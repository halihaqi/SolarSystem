using System;
using System.Collections.Generic;
using HFramework;

namespace Game.Model.CustomGalaxy
{
    [Serializable]
    public class CustomGalaxyData
    {
        public Dictionary<int, CustomGalaxyInfo> infos;

        public CustomGalaxyData()
        {
            infos = new Dictionary<int, CustomGalaxyInfo>(GameConst.CUSTOM_GALAXY_NUM);
            for (int i = 0; i < GameConst.CUSTOM_GALAXY_NUM; i++)
            {
                infos.Add(i, null);
            }
        }
    }
}