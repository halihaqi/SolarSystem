using System.Globalization;
using HFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI.Controls
{
    public class UI_color_ring : ControlBase
    {
        private Image _ringPop;
        private Image _imgOutColor;
        private Image _imgInnerColor;
        private Image _imgOld;
        private Image _imgNew;

        private Slider _sldR;
        private Slider _sldG;
        private Slider _sldB;

        private InputField _ifR;
        private InputField _ifG;
        private InputField _ifB;

        private Button _btnBack;

        private Color _curColor;
        public Color CurColor => _curColor;
        private bool _isPop = false;

        protected internal override void OnInit()
        {
            base.OnInit();
            _ringPop = GetControl<Image>("ring_pop");
            _imgOutColor = GetControl<Image>("img_out_color");
            _imgInnerColor = GetControl<Image>("img_inner_color");
            _imgOld = GetControl<Image>("img_old");
            _imgNew = GetControl<Image>("img_new");

            _sldR = GetControl<Slider>("sld_r");
            _sldG = GetControl<Slider>("sld_g");
            _sldB = GetControl<Slider>("sld_b");

            _ifR = GetControl<InputField>("if_r");
            _ifG = GetControl<InputField>("if_g");
            _ifB = GetControl<InputField>("if_b");

            _btnBack = GetControl<Button>("btn_back");
            
            UIManager.AddCustomEventListener(_imgOutColor, EventTriggerType.PointerClick, OnClickOutColor);
            _btnBack.onClick.AddListener(OnBack);
            (_sldR.minValue, _sldR.maxValue) = (0, 1);
            (_sldG.minValue, _sldG.maxValue) = (0, 1);
            (_sldB.minValue, _sldB.maxValue) = (0, 1);

            _sldR.onValueChanged.AddListener(OnSldRChanged);
            _sldG.onValueChanged.AddListener(OnSldGChanged);
            _sldB.onValueChanged.AddListener(OnSldBChanged);
            
            _ifR.onValueChanged.AddListener(OnIfRChanged);
            _ifG.onValueChanged.AddListener(OnIfGChanged);
            _ifB.onValueChanged.AddListener(OnIfBChanged);
        }

        public void SetData(Color color)
        {
            _curColor = color;
            _imgOutColor.color = color;
            _ringPop.gameObject.SetActive(false);
        }

        private void OnClickOutColor(BaseEventData data)
        {
            _isPop = !_isPop;
            _ringPop.gameObject.SetActive(_isPop);
            if (_isPop)
            {
                _imgInnerColor.color = _curColor;
                _imgOld.color = _curColor;
                _imgNew.color = _curColor;
                _sldR.value = _curColor.r;
                _sldG.value = _curColor.g;
                _sldB.value = _curColor.b;
                _ifR.text = _curColor.r.ToString(CultureInfo.InvariantCulture);
                _ifG.text = _curColor.g.ToString(CultureInfo.InvariantCulture);
                _ifB.text = _curColor.b.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void OnBack()
        {
            _isPop = false;
            _ringPop.gameObject.SetActive(false);
        }

        private void OnSldRChanged(float val)
        {
            _ifR.text = val.ToString(CultureInfo.InvariantCulture);
            _curColor.r = val;
            _imgNew.color = _curColor;
            _imgInnerColor.color = _curColor;
            _imgOutColor.color = _curColor;
        }
        
        private void OnSldGChanged(float val)
        {
            _ifG.text = val.ToString(CultureInfo.InvariantCulture);
            _curColor.g = val;
            _imgNew.color = _curColor;
            _imgInnerColor.color = _curColor;
            _imgOutColor.color = _curColor;
        }
        
        private void OnSldBChanged(float val)
        {
            _ifB.text = val.ToString(CultureInfo.InvariantCulture);
            _curColor.b = val;
            _imgNew.color = _curColor;
            _imgInnerColor.color = _curColor;
            _imgOutColor.color = _curColor;
        }

        private void OnIfRChanged(string str)
        {
            if (string.IsNullOrEmpty(str))
                str = "0";
            var val = float.Parse(str);
            _sldR.value = val;
            _curColor.r = val;
            _imgNew.color = _curColor;
            _imgInnerColor.color = _curColor;
            _imgOutColor.color = _curColor;
        }
        
        private void OnIfGChanged(string str)
        {
            if (string.IsNullOrEmpty(str))
                str = "0";
            var val = float.Parse(str);
            _sldG.value = val;
            _curColor.g = val;
            _imgNew.color = _curColor;
            _imgInnerColor.color = _curColor;
            _imgOutColor.color = _curColor;
        }
        
        private void OnIfBChanged(string str)
        {
            if (string.IsNullOrEmpty(str))
                str = "0";
            var val = float.Parse(str);
            _sldB.value = val;
            _curColor.b = val;
            _imgNew.color = _curColor;
            _imgInnerColor.color = _curColor;
            _imgOutColor.color = _curColor;
        }
    }
}