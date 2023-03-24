using System.Collections.Generic;
using HFramework;

namespace Game.Model
{
    public class CelestialBodyModel : Singleton<CelestialBodyModel>
    {
        private Dictionary<int, CelestialBodyInfo> _dic;

        public CelestialBodyModel()
        {
            _dic = HEntry.DataMgr.GetTable<CelestialBodyInfoContainer>().dataDic;
        }

        public CelestialBodyInfo GetCelestialBodyInfo(string name)
        {
            foreach (var info in _dic.Values)
            {
                if (info.name == name)
                    return info;
            }

            return null;
        }
    }
}