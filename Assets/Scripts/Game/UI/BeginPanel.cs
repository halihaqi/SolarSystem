using DG.Tweening;
using Game.Procedure;
using HFramework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.UI
{
    public class BeginPanel : PanelBase
    {
        private Button _btnPlay;
        private Button _btnExplore;
        private Button _btnOption;
        private Button _btnExit;
        private Image _imgBtns;
        private float _oriX;

        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            _btnPlay = GetControl<Button>("btn_play");
            _btnExplore = GetControl<Button>("btn_explore");
            _btnOption = GetControl<Button>("btn_option");
            _btnExit = GetControl<Button>("btn_exit");
            _imgBtns = GetControl<Image>("img_btns");
            UIManager.AddCustomEventListener(_imgBtns, EventTriggerType.PointerEnter, OnEnterBtns);
            UIManager.AddCustomEventListener(_imgBtns, EventTriggerType.PointerExit, OnExitBtns);
            _oriX = _imgBtns.rectTransform.anchoredPosition.x;
        }

        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            switch (btnName)
            {
                case "btn_explore":
                    HEntry.ProcedureMgr.ChangeState<RomaProcedure>();
                    break;
                case "btn_exit":
                    Application.Quit();
                    break;
            }
        }

        private void OnEnterBtns(BaseEventData data)
        {
            _imgBtns.rectTransform.DOAnchorPosX(0, 0.5f);
        }
        
        private void OnExitBtns(BaseEventData data)
        {
            _imgBtns.rectTransform.DOAnchorPosX(_oriX, 0.5f);
        }
    }
}