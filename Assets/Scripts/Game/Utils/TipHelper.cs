using System;
using Game.UI;
using HFramework;

namespace Game.Utils
{
    public static class TipHelper
    {
        public static void ShowTip(string str, Action callback)
        {
            HEntry.UIMgr.ShowPanel<TipPop>(GameConst.UIGROUP_POP, 
                new TipParam(str, callback));
        }
    }
}