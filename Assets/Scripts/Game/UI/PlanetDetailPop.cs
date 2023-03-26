using Game.Model;
using Game.SolarSystem;
using HFramework;
using UnityEngine.UI;

namespace Game.UI
{
    public class PlanetDetailPop : PopBase
    {
        private Text _txtDetail;

        protected internal override void OnInit(object userData)
        {
            isModal = true;
            base.OnInit(userData);
            _txtDetail = GetControl<Text>("txt_detail");
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            if (userData is CelestialBody body)
            {
                _txtDetail.text = CelestialBodyModel.Instance.
                    GetCelestialBodyInfo(body.bodyName).detail;
            }
        }
    }
}