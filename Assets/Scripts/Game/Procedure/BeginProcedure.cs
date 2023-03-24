using Game.UI;
using HFramework;

namespace Game.Procedure
{
    public class BeginProcedure : ProcedureBase
    {
        protected internal override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            if (!HEntry.SceneMgr.IsCurScene(GameConst.BEGIN_SCENE))
            {
                HEntry.SceneMgr.LoadSceneWithPanel<LoadingPanel>(GameConst.BEGIN_SCENE, OnEnterScene, false);
                DelayUtils.Instance.Delay(1, 1, obj =>
                {
                    HEntry.SceneMgr.ManualCompleteLoad();
                });
            }
            else
                InitBeginScene(null);
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
            DelayUtils.Instance.Delay(0.1f,1, InitBeginScene);
        }

        private void InitBeginScene(object obj)
        {
            var ss = SolarSystem.SolarSystem.Instance;
            ss.Init();
            ss.isCenterStatic = false;
            ss.CenterBody = ss.GetBodyByName("TweenRed");
            HEntry.UIMgr.ShowPanel<BeginPanel>();
        }
    }
}