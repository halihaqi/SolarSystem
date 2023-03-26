using Game.Model.CustomGalaxy;
using Game.Procedure;
using Game.UI.Controls;
using Game.Utils;
using HFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class CreateMainPanel : PanelBase
    {
        private HList _svBody;
        private UI_move_control _moveControl;
        private Text _txtGalaxyName;

        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            _svBody = GetControl<HList>("sv_body");
            _moveControl = GetControl<UI_move_control>("move_control");
            _txtGalaxyName = GetControl<Text>("txt_galaxy_name");
            _svBody.itemRenderer = OnItemRenderer;
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            var rc = Camera.main.GetComponent<RomaCamera>();
            _moveControl.SetData(rc);
            _svBody.numItems = CustomGalaxyModel.Instance.CurBodyNum;
            _txtGalaxyName.text = CustomGalaxyModel.Instance.CurGalaxy.galaxyName;
            HEntry.InputMgr.Enabled = true;
            HEntry.EventMgr.AddListener(ClientEvent.BODY_UPDATE, OnBodyUpdate);
        }

        protected internal override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            HEntry.EventMgr.RemoveListener(ClientEvent.BODY_UPDATE, OnBodyUpdate);
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "btn_back":
                    HEntry.ProcedureMgr.ChangeState<BeginProcedure>();
                    break;
                case "btn_create":
                    HEntry.UIMgr.ShowPanel<CreateBodyPop>(GameConst.UIGROUP_POP);
                    break;
                case "btn_save":
                    TipHelper.ShowTip("是否保存修改？", () =>
                    {
                        CustomGalaxyModel.Instance.SaveData();
                    });
                    break;
            }
        }

        private void OnItemRenderer(int index, GameObject obj)
        {
            var item = obj.GetComponent<UI_body_item>();
            item.SetData(CustomGalaxyModel.Instance.CurGalaxy.bodies[index]);
        }

        protected override void OnToggleValueChanged(string togName, bool isToggle)
        {
            base.OnToggleValueChanged(togName, isToggle);
            if (togName == "tog_line")
            {
                SolarSystem.SolarSystem.Instance.OrbitEnabled = isToggle;
            }
        }

        private void OnBodyUpdate()
        {
            _svBody.numItems = CustomGalaxyModel.Instance.CurBodyNum;
        }
    }
}