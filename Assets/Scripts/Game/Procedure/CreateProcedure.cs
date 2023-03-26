using System;
using System.Collections;
using Game.Model.CustomGalaxy;
using Game.SolarSystem;
using Game.UI;
using HFramework;
using UnityEngine;

namespace Game.Procedure
{
    public class CreateProcedure : ProcedureBase
    {
        protected internal override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            HEntry.SceneMgr.LoadSceneWithPanel<LoadingPanel>(GameConst.CREATE_SCENE, OnEnterScene, false);
        }

        protected internal override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            SolarSystem.SolarSystem.Instance.ClearBody();
            HEntry.UIMgr.HideAllLoadedPanels();
            HEntry.UIMgr.HideAllLoadingPanels();
        }

        private void OnEnterScene()
        {
            //生成星系
            HEntry.MonoMgr.StartCoroutine(GenerateGalaxy());
        }

        private IEnumerator GenerateGalaxy()
        {
            var info = CustomGalaxyModel.Instance.CurGalaxy;
            if (info == null)
                throw new Exception("Has no custom galaxy info.");

            CustomBody centerBody = null;
            //逐个生成天体
            foreach (var body in info.bodies)
            {
                bool isOk = false;
                HEntry.ResMgr.LoadAsync<GameObject>(GameConst.BODY_PATH, obj =>
                {
                    var cb = obj.AddComponent<CustomBody>();
                    cb.SetData(body);
                    if (body == info.centerBody)
                        centerBody = cb;
                    isOk = true;
                });

                while (!isOk)
                    yield return null;
            }
            
            //全部生成结束，初始化天体系统
            SolarSystem.SolarSystem.Instance.Init();
            SolarSystem.SolarSystem.Instance.CenterBody = centerBody;
            SolarSystem.SolarSystem.Instance.isCenterStatic = info.isCenterStatic;
            yield return new WaitForSeconds(0.1f);
            
            //显示面板
            HEntry.UIMgr.ShowPanel<CreateMainPanel>();
            HEntry.SceneMgr.ManualCompleteLoad();
        }
    }
}