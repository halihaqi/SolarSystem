using Hali_Framework;
using UnityEngine;

namespace Game.UI
{
    public class StartPanel : PanelBase
    {
        protected override void OnClick(string btnName)
        {
            base.OnClick(btnName);
            if (btnName == "StartBtn")
            {
                //进入选择游戏模式面板
            }

            if (btnName == "ConfigBtn")
            {
                //进入设置面板
            }

            if (btnName == "ExitBtn")
            {
                Application.Quit();
            }
        }
    }
}