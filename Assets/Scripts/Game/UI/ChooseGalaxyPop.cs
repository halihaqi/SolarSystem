using System.Collections.Generic;
using Game.Model.CustomGalaxy;
using Game.Procedure;
using Game.UI.Controls;
using Game.Utils;
using HFramework;
using UnityEngine;

namespace Game.UI
{
    public class ChooseGalaxyPop : PopBase
    {
        private HList _svGalaxy;
        private Dictionary<int, CustomGalaxyInfo> _infos;

        protected internal override void OnInit(object userData)
        {
            isModal = true;
            base.OnInit(userData);
            _svGalaxy = GetControl<HList>("sv_galaxy");
            _svGalaxy.itemRenderer = OnItemRenderer;
            _svGalaxy.onClickItem = OnItemClick;
        }

        protected internal override void OnShow(object userData)
        {
            base.OnShow(userData);
            _infos = CustomGalaxyModel.Instance.GalaxyDic;
            _svGalaxy.numItems = _infos.Count;
        }

        private void OnItemRenderer(int index, GameObject obj)
        {
            var item = obj.GetComponent<UI_galaxy_item>();
            item.SetData(index, _infos[index]);
        }

        private void OnItemClick(int index, ControlBase cb)
        {
            var item = cb as UI_galaxy_item;
            if (item.isNull)
            {
                TipHelper.ShowTip("是否新建星系？", () =>
                {
                    HEntry.UIMgr.ShowPanel<NewGalaxyPop>(GameConst.UIGROUP_POP, userData: index);
                });
            }
            else
            {
                TipHelper.ShowTip("是否进入该星系？", () =>
                {
                    CustomGalaxyModel.Instance.ChooseGalaxy(index);
                    HEntry.ProcedureMgr.ChangeState<CreateProcedure>();
                });
            }
        }
    }
}