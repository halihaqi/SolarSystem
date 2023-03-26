using Game.Model.CustomGalaxy;
using Game.SolarSystem;
using Game.UI.Controls;
using Game.Utils;
using HFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class CreateBodyPop : PopBase
    {
        private InputField _ifName;
        private UI_input_arrow _inputMass;
        private UI_input_arrow _inputRadius;
        private UI_input_arrow _inputDistance;
        private UI_input_arrow _inputSpeed;
        private UI_color_ring _colorRing;

        protected internal override void OnInit(object userData)
        {
            isModal = true;
            base.OnInit(userData);
            _ifName = GetControl<InputField>("if_name");
            _inputMass = GetControl<UI_input_arrow>("input_mass");
            _inputRadius = GetControl<UI_input_arrow>("input_radius");
            _inputDistance = GetControl<UI_input_arrow>("input_distance");
            _inputSpeed = GetControl<UI_input_arrow>("input_speed");
            _colorRing = GetControl<UI_color_ring>("color_ring");
            _inputMass.SetData(0, 99999999);
            _inputRadius.SetData(0, 99999);
            _inputDistance.SetData(0, 99999);
            _inputSpeed.SetData(-9999, 9999);
            _colorRing.SetData(Color.white);
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            if (btnName == "btn_sure")
            {
                TipHelper.ShowTip("是否创建天体？", OnSure);
            }
        }

        private void OnSure()
        {
            HideMe();
            var info = new CustomBodyInfo(_ifName.text, _inputMass.CurNum, _inputRadius.CurNum, _inputSpeed.CurNum,
                _inputDistance.CurNum, _colorRing.CurColor);
            CustomGalaxyModel.Instance.AddBody(info);

            HEntry.ResMgr.LoadAsync<GameObject>(GameConst.BODY_PATH, obj =>
            {
                var cb = obj.AddComponent<CustomBody>();
                cb.SetData(info);
                HEntry.EventMgr.TriggerEvent(ClientEvent.BODY_UPDATE);
            });
        }
    }
}