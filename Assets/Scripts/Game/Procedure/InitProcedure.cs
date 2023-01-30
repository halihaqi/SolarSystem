using Game.UI;
using Hali_Framework;

namespace Game.Procedure
{
    public class InitProcedure : ProcedureBase
    {
        protected internal override void OnEnter(IFsm<ProcedureMgr> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            UIMgr.Instance.ShowPanel<StartPanel>("StartPanel");
            //初始化天体系统
            SolarSystem.Instance.Init();
        }
    }
}