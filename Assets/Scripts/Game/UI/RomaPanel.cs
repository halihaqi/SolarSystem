using System;
using Game.UI.Controls;
using HFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class RomaPanel : PanelBase
    {
        private Toggle _togLine;
        private UI_move_control _moveControl;
        
        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            _togLine = GetControl<Toggle>("tog_line");
            _moveControl = GetControl<UI_move_control>("move_control");
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            _togLine.isOn = SolarSystem.SolarSystem.Instance.OrbitEnabled;
            _moveControl.SetData(CameraMaster.Instance.RomaCamera);
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            if (btnName == "btn_back")
            {
                CameraMaster.Instance.SwitchCamera(false);
                HideMe();
                HEntry.UIMgr.ShowPanel<RomaMainPanel>();
            }
        }
        
        protected override void OnToggleValueChanged(string togName, bool isToggle)
        {
            base.OnToggleValueChanged(togName, isToggle);
            if (togName == "tog_line")
            {
                SolarSystem.SolarSystem.Instance.OrbitEnabled = isToggle;
            }
        }
    }
}