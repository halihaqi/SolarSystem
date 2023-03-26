using System;
using System.Collections.Generic;
using Game.SolarSystem;

namespace Game.Model.CustomGalaxy
{
    [Serializable]
    public class CustomGalaxyInfo
    {
        public int id;
        public List<CustomBodyInfo> bodies;
        public CustomBodyInfo centerBody;
        public bool isCenterStatic;
        public string galaxyName;
        
        public CustomGalaxyInfo(){}

        public CustomGalaxyInfo(int id)
        {
            this.id = id;
            bodies = new List<CustomBodyInfo>();
            galaxyName = "Undefined";
        }
        
        public CustomGalaxyInfo(int id, string name)
        {
            this.id = id;
            bodies = new List<CustomBodyInfo>();
            galaxyName = name;
        }
    }
}