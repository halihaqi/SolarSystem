using HFramework;

namespace Game.Procedure
{
    public class InitProcedure : ProcedureBase
    {
        protected internal override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            //初始化逻辑
            ChangeState<BeginProcedure>(procedureOwner);
        }
    }
}