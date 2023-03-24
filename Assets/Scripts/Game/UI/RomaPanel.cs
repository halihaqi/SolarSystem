using System;
using HFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class RomaPanel : PanelBase
    {
        [SerializeField] private CanvasGroup control;
        private Image _imgArrow;
        private TextMeshProUGUI _txtSpeed;
        private Toggle _togLine;


        private bool _useMove = false;
        private RomaCamera _rc;
        
        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            _imgArrow = GetControl<Image>("img_arrow");
            _txtSpeed = GetControl<TextMeshProUGUI>("txt_speed");
            _togLine = GetControl<Toggle>("tog_line");
            control.alpha = 0;
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            HEntry.EventMgr.AddListener<int>(ClientEvent.GET_MOUSE_BUTTON_DOWN, OnUnLock);
            HEntry.EventMgr.AddListener<int>(ClientEvent.GET_MOUSE_BUTTON_UP, OnLock);
            HEntry.EventMgr.AddListener<Vector2>(ClientEvent.GET_MOVE, OnMove);
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY_DOWN, OnSprint);
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY_UP, OnUnSprint);

            _rc = CameraMaster.Instance.RomaCamera;
            _togLine.isOn = SolarSystem.SolarSystem.Instance.OrbitEnabled;
        }

        private void Update()
        {
            _txtSpeed.text = ((int)_rc.CurSpeed).ToString();
        }

        protected internal override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            HEntry.EventMgr.RemoveListener<int>(ClientEvent.GET_MOUSE_BUTTON_DOWN, OnUnLock);
            HEntry.EventMgr.RemoveListener<int>(ClientEvent.GET_MOUSE_BUTTON_UP, OnLock);
            HEntry.EventMgr.RemoveListener<Vector2>(ClientEvent.GET_MOVE, OnMove);
            HEntry.EventMgr.RemoveListener<KeyCode>(ClientEvent.GET_KEY_DOWN, OnSprint);
            HEntry.EventMgr.RemoveListener<KeyCode>(ClientEvent.GET_KEY_UP, OnUnSprint);
        }

        private void OnUnLock(int key)
        {
            if(key != 1) return;
            _useMove = true;
            control.alpha = 1;
        }

        private void OnLock(int key)
        {
            if(key != 1) return;
            _useMove = false;
            control.alpha = 0;
        }

        private void OnSprint(KeyCode key)
        {
            if(key != KeyCode.LeftShift) return;
            var size = _imgArrow.rectTransform.sizeDelta;
            _imgArrow.rectTransform.sizeDelta = new Vector2(size.x + 20, size.y);
        }
        
        private void OnUnSprint(KeyCode key)
        {
            if(key != KeyCode.LeftShift) return;
            var size = _imgArrow.rectTransform.sizeDelta;
            _imgArrow.rectTransform.sizeDelta = new Vector2(size.x - 20, size.y);
        }

        private void OnMove(Vector2 input)
        {
            if(!_useMove) return;
            
            if(input == Vector2.zero)
                _imgArrow.gameObject.SetActive(false);
            else
            {
                _imgArrow.gameObject.SetActive(true);
                float angle = Mathf.Atan2(-input.x, input.y) * Mathf.Rad2Deg;
                if (angle < 0)
                    angle += 360f;

                control.transform.localRotation = Quaternion.Euler(0, 0, angle);
            }
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