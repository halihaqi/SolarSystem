using HFramework;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Controls
{
    public class UI_move_control : ControlBase
    {
        [SerializeField] private CanvasGroup control;
        private Image _imgArrow;
        private TextMeshProUGUI _txtSpeed;
        private RomaCamera _rc;
        
        private bool _useMove = false;

        protected internal override void OnInit()
        {
            base.OnInit();
            _imgArrow = GetControl<Image>("img_arrow");
            _txtSpeed = GetControl<TextMeshProUGUI>("txt_speed");
            control.alpha = 0;
        }

        protected internal override void OnRecycle()
        {
            base.OnRecycle();
            HEntry.EventMgr.RemoveListener<int>(ClientEvent.GET_MOUSE_BUTTON_DOWN, OnUnLock);
            HEntry.EventMgr.RemoveListener<int>(ClientEvent.GET_MOUSE_BUTTON_UP, OnLock);
            HEntry.EventMgr.RemoveListener<Vector2>(ClientEvent.GET_MOVE, OnMove);
            HEntry.EventMgr.RemoveListener<KeyCode>(ClientEvent.GET_KEY_DOWN, OnSprint);
            HEntry.EventMgr.RemoveListener<KeyCode>(ClientEvent.GET_KEY_UP, OnUnSprint);
        }

        public void SetData(RomaCamera rc)
        {
            _rc = rc;
            HEntry.EventMgr.AddListener<int>(ClientEvent.GET_MOUSE_BUTTON_DOWN, OnUnLock);
            HEntry.EventMgr.AddListener<int>(ClientEvent.GET_MOUSE_BUTTON_UP, OnLock);
            HEntry.EventMgr.AddListener<Vector2>(ClientEvent.GET_MOVE, OnMove);
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY_DOWN, OnSprint);
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY_UP, OnUnSprint);
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
            _txtSpeed.text = ((int)_rc.CurSpeed).ToString();
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
    }
}