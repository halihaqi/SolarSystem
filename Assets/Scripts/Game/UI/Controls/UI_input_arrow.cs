using HFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Controls
{
    public class UI_input_arrow : ControlBase
    {
        private InputField _inputField;
        private Button _btnAdd;
        private Button _btnSub;

        private int _minNum = int.MinValue;
        private int _maxNum = int.MaxValue;
        private int _curNum;

        public int CurNum => _curNum;

        protected internal override void OnInit()
        {
            base.OnInit();
            _inputField = GetControl<InputField>("input_field");
            _btnAdd = GetControl<Button>("btn_add");
            _btnSub = GetControl<Button>("btn_sub");
            _inputField.onValueChanged.AddListener(OnInput);
            _btnAdd.onClick.AddListener(OnAdd);
            _btnSub.onClick.AddListener(OnSub);
        }

        public void SetData(int minNum, int maxNum)
        {
            _minNum = minNum;
            _maxNum = maxNum;
        }

        private void OnInput(string str)
        {
            if (string.IsNullOrEmpty(str))
                str = "0";
            _curNum = int.Parse(str);
            _curNum = Mathf.Clamp(_curNum, _minNum, _maxNum);
            _inputField.text = _curNum.ToString();
            
            _btnAdd.enabled = _curNum < _maxNum;
            _btnSub.enabled = _curNum > _minNum;
        }

        private void OnAdd()
        {
            ++_curNum;
            _inputField.text = _curNum.ToString();
        }

        private void OnSub()
        {
            --_curNum;
            _inputField.text = _curNum.ToString();
        }
    }
}