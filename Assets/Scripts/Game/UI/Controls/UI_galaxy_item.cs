using Game.Model.CustomGalaxy;
using HFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Controls
{
    public class UI_galaxy_item : ControlBase
    {
        [SerializeField] private GameObject infoGroup;
        [SerializeField] private GameObject nullSign;
        private Text _txtId;
        private Text _txtName;
        private Text _txtNum;
        private Text _txtCenter;
        private bool _isNull;

        public bool isNull => _isNull;

        protected internal override void OnInit()
        {
            base.OnInit();
            _txtId = GetControl<Text>("txt_id");
            _txtName = GetControl<Text>("txt_name");
            _txtNum = GetControl<Text>("txt_num");
            _txtCenter = GetControl<Text>("txt_center");
        }

        public void SetData(int index, CustomGalaxyInfo info)
        {
            _isNull = info == null;
            infoGroup.SetActive(info != null);
            nullSign.SetActive(info == null);
            _txtId.text = $"No.{index + 1}";
            if (info == null)
                return;
            
            _txtName.text = info.galaxyName;
            _txtNum.text = info.bodies.Count.ToString();
            _txtCenter.text = info.centerBody == null ? "null" : info.centerBody.bodyName;
        }
    }
}