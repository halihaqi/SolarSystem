using System.Globalization;
using Game.SolarSystem;
using HFramework;
using TMPro;
using UnityEngine.UI;

namespace Game.UI.Controls
{
    public class UI_planet_info : ControlBase
    {
        private TextMeshProUGUI _txtName;
        private TextMeshProUGUI _txtMass;
        private TextMeshProUGUI _txtRadius;
        private TextMeshProUGUI _txtDistance;
        private CelestialBody _body;
        private Button _btnFocus;

        protected internal override void OnInit()
        {
            base.OnInit();
            _txtName = GetControl<TextMeshProUGUI>("txt_name");
            _txtMass = GetControl<TextMeshProUGUI>("txt_mass");
            _txtRadius = GetControl<TextMeshProUGUI>("txt_radius");
            _txtDistance = GetControl<TextMeshProUGUI>("txt_distance");
            _btnFocus = GetControl<Button>("btn_focus");
            _btnFocus.onClick.AddListener(OnDetail);
        }

        public void SetData(CelestialBody body)
        {
            _body = body;
            _txtName.text = body.bodyName;
            _txtMass.text = body.Mass.ToString("0.##E+0") + " xEarth";
            _txtRadius.text = body.radius.ToString("0.##E+0") + " km";
            _txtDistance.text = body.distance.ToString("0.##E+0") + " mkm";
        }

        private void OnDetail()
        {
            if(_body == null) return;
            HEntry.UIMgr.ShowPanel<PlanetDetailPop>(GameConst.UIGROUP_POP, userData: _body);
        }
    }
}