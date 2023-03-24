using System.Collections.Generic;
using Game.UI.Controls;
using HFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class RomaFocusPanel : PanelBase
    {
        [SerializeField] private Transform togGroup;
        private Dictionary<string, Toggle> _togs;
        private UI_planet_info _planetInfo;
        private FocusCamera _fc;

        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            
            _planetInfo = GetControl<UI_planet_info>("planet_info");

            _togs = new Dictionary<string, Toggle>(togGroup.childCount);
            for (int i = 0; i < togGroup.childCount; i++)
            {
                var tog = togGroup.GetChild(i).GetComponent<Toggle>();
                tog.onValueChanged.AddListener(OnToggle);
                _togs.Add(tog.name, tog);
            }
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            _fc = CameraMaster.Instance.FocusCamera;
            _fc.Camera.fieldOfView = 50;
            foreach (var kv in _togs)
            {
                if (kv.Value.isOn)
                {
                    var body = SolarSystem.SolarSystem.Instance.GetBodyByName(kv.Key);
                    _fc.SetTarget(body);
                    _planetInfo.SetData(body);
                    break;
                }
            }
            SolarSystem.SolarSystem.Instance.OrbitEnabled = false;
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            if (btnName == "btn_back")
            {
                HideMe();
                _fc.SetTarget(null);
                HEntry.UIMgr.ShowPanel<RomaMainPanel>();
            }
        }

        private void OnToggle(bool isOn)
        {
            if (isOn && _fc != null)
            {
                foreach (var kv in _togs)
                {
                    if (kv.Value.isOn)
                    {
                        var body = SolarSystem.SolarSystem.Instance.GetBodyByName(kv.Key);
                        _fc.SetTarget(body);
                        _planetInfo.SetData(body);
                        return;
                    }
                }
            }
        }
    }
}