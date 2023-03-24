using HFramework;
using UnityEngine.UI;

namespace Game.UI
{
    public class LoadingPanel : PanelBase
    {
        private Slider _sldLoad;

        protected internal override void OnInit(object userData)
        {
            base.OnInit(userData);
            _sldLoad = GetControl<Slider>("sld_load");
            _sldLoad.maxValue = 100; 
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            HEntry.EventMgr.AddListener<int>(ClientEvent.SCENE_LOADING, OnLoading);
        }

        protected internal override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            HEntry.EventMgr.RemoveListener<int>(ClientEvent.SCENE_LOADING, OnLoading);
        }

        private void OnLoading(int val)
        {
            _sldLoad.value = val;
        }
    }
}