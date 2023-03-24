using Game.Procedure;
using HFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class RomaMainPanel : PanelBase
    {
        private Slider _sldZoom;
        private TextMeshProUGUI _txtZoom;
        private Toggle _togLine;

        private Camera _cam;

        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            _sldZoom = GetControl<Slider>("sld_zoom");
            _txtZoom = GetControl<TextMeshProUGUI>("txt_zoom");
            _togLine = GetControl<Toggle>("tog_line");
            _sldZoom.minValue = 0;
            _sldZoom.maxValue = 50;
        }

        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            float fov = _cam.fieldOfView;
            _txtZoom.text = "x" + ((100 - fov) / 50 + 1).ToString("0.0");
            _sldZoom.value = 100 - fov;
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            _cam = CameraMaster.Instance.FocusCamera.Camera;
            HEntry.InputMgr.Enabled = true;
            _cam.fieldOfView = 100;
            _togLine.isOn = SolarSystem.SolarSystem.Instance.OrbitEnabled;
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "btn_back":
                    HEntry.ProcedureMgr.ChangeState<BeginProcedure>();
                    break;
                case "btn_option":
                    break;
                case "btn_focus":
                    HideMe();
                    HEntry.UIMgr.ShowPanel<RomaFocusPanel>(userData: _cam);
                    break;
                case "btn_roma":
                    CameraMaster.Instance.SwitchCamera(true);
                    HideMe();
                    HEntry.UIMgr.ShowPanel<RomaPanel>();
                    break;
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