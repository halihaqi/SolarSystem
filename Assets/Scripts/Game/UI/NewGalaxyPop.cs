using Game.Model.CustomGalaxy;
using Game.Procedure;
using HFramework;
using UnityEngine.UI;

namespace Game.UI
{
    public class NewGalaxyPop : PopBase
    {
        private InputField _ifName;
        private int _index;

        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            _ifName = GetControl<InputField>("if_name");
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            if (userData is int i)
                _index = i;
            _ifName.text = "Undefined";
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            if (btnName == "btn_sure")
            {
                CustomGalaxyModel.Instance.SetCustomGalaxy(_index, new CustomGalaxyInfo(_index, _ifName.text));
                CustomGalaxyModel.Instance.ChooseGalaxy(_index);
                HEntry.ProcedureMgr.ChangeState<CreateProcedure>();
            }
            else if(btnName == "btn_cancel")
                HideMe();
        }
    }
}