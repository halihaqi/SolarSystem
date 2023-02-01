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
            SolarSystem.SolarSystem.Instance.Init();
            //开启天体系统计算
            MonoMgr.Instance.AddUpdateListener(SolarSystem.SolarSystem.Instance.Run);
        }
    }
}