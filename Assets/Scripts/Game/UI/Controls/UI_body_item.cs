using Game.Model.CustomGalaxy;
using HFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Controls
{
    public class UI_body_item : ControlBase
    {
        private Toggle _tog;
        private Text _txtName;
        private Image _imgColor;
        private CustomBodyInfo _body;
        
        protected internal override void OnInit()
        {
            base.OnInit();
            _tog = GetComponent<Toggle>();
            _txtName = GetControl<Text>("txt_name");
            _imgColor = GetControl<Image>("img_color");
        }

        public void SetData(CustomBodyInfo body)
        {
            _body = body;
            _txtName.text = body.bodyName;
            var color = body.bodyColor;
            _imgColor.color = new Color(color[0], color[1], color[2], color[3]);
        }
    }
}