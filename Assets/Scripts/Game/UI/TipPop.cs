using System;
using HFramework;
using UnityEngine.UI;

namespace Game.UI
{
    public class TipPop : PopBase
    {
        private Text _txtTip;
        private Action _callback;
        
        protected internal override void OnInit(object userData)
        {
            isModal = true;
            base.OnInit(userData);
            _txtTip = GetControl<Text>("txt_tip");
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            _txtTip.text = "";
            if (userData is TipParam p)
            {
                _txtTip.text = p.tip;
                _callback = p.callback;
            }
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            if (btnName == "btn_sure")
            {
                _callback?.Invoke();
                HideMe();
            }
            else if(btnName == "btn_cancel")
                HideMe();
        }
    }

    public class TipParam
    {
        public string tip;
        public Action callback;

        public TipParam(string tip, Action callback)
        {
            this.tip = tip;
            this.callback = callback;
        }
    }
}