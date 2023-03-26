using Game.Procedure;
using HFramework;

namespace Game.UI
{
    public class ChooseModePop : PopBase
    {
        protected internal override void OnInit(object userData)
        {
            isModal = true;
            base.OnInit(userData);
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            if(btnName == "btn_roma")
                HEntry.ProcedureMgr.ChangeState<RomaProcedure>();
            else if (btnName == "btn_explore")
            {
                //探索模式
            }
        }
    }
}