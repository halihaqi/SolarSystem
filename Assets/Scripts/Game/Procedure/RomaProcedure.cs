using Game.UI;
using HFramework;

namespace Game.Procedure
{
    public class RomaProcedure : ProcedureBase
    {
        protected internal override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            HEntry.SceneMgr.LoadSceneWithPanel<LoadingPanel>(GameConst.ROMA_SCENE, OnEnterScene, false);
            DelayUtils.Instance.Delay(1, 1, obj =>
            {
                HEntry.SceneMgr.ManualCompleteLoad();
            });
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
            DelayUtils.Instance.Delay(0.1f,1, obj =>
            {
                var ss = SolarSystem.SolarSystem.Instance;
                ss.Init();
                ss.isCenterStatic = true;
                ss.CenterBody = ss.GetBodyByName("Sun");
                HEntry.UIMgr.ShowPanel<RomaMainPanel>();
            });
        }
    }
}