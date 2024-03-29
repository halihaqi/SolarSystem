﻿using UnityEngine;
using UnityEngine.EventSystems;

namespace HFramework
{
    public abstract class PopBase : PanelBase
    {
        private const string MASK_PATH = "UI/Controls/mask";

        private MaskEntity _mask;
        protected bool isModal = false;
        protected bool interactable = true;

        public bool IsModal => isModal;

        public bool Interactable => interactable;

        protected MaskEntity Mask => _mask;

        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            //如果Pop面板没有mask，自动添加
            var mask = transform.GetChild(0);
            if (isModal && !mask.TryGetComponent(out _mask))
            {
                var obj = HEntry.ResMgr.Load<GameObject>(MASK_PATH);
                obj.transform.SetParent(this.transform, false);
                obj.transform.SetAsFirstSibling();
                _mask = obj.GetComponent<MaskEntity>();
                _mask.SetActive(false);
            }
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);

            if (isModal)
                _mask.SetActive(true);

            if (isModal && interactable)
                UIManager.AddCustomEventListener(_mask.Mask, EventTriggerType.PointerClick, OnClickBk);
        }

        protected internal override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            if(_mask != null)
                UIManager.RemoveAllCustomEvents(_mask.Mask);
        }

        protected internal override void OnRecycle()
        {
            base.OnRecycle();
            if(_mask != null)
                _mask.SetActive(false);
        }

        private void OnClickBk(BaseEventData data)
            => HideMe();
    }
}