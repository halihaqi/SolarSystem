using System;
using System.Collections.Generic;
using HFramework;

namespace Game.Model.CustomGalaxy
{
    public class CustomGalaxyModel : Singleton<CustomGalaxyModel>
    {
        private CustomGalaxyData _galaxyData;
        private CustomGalaxyInfo _curGalaxy;

        public CustomGalaxyInfo CurGalaxy => _curGalaxy;

        public Dictionary<int, CustomGalaxyInfo> GalaxyDic => _galaxyData.infos;

        public int CurBodyNum => _curGalaxy.bodies.Count;

        public CustomGalaxyModel()
        {
            UpdateData();
        }
        
        public void SaveData()
        {
            HEntry.DataMgr.Save(GameConst.CUSTOM_GALAXY_FILE, 
                GameConst.CUSTOM_GALAXY_DATA, _galaxyData);
        }

        public void UpdateData()
        {
            _galaxyData = HEntry.DataMgr.Load<CustomGalaxyData>(GameConst.CUSTOM_GALAXY_FILE,
                GameConst.CUSTOM_GALAXY_DATA);
        }

        public CustomGalaxyInfo GetCustomGalaxy(int id)
        {
            if (_galaxyData.infos.ContainsKey(id))
                return _galaxyData.infos[id];
            return null;
        }

        public void ChooseGalaxy(int id)
        {
            if (_galaxyData.infos.ContainsKey(id))
                _curGalaxy = _galaxyData.infos[id];
        }

        public void SetCustomGalaxy(int id, CustomGalaxyInfo info)
        {
            if (_galaxyData.infos.ContainsKey(id))
                _galaxyData.infos[id] = info;
            else
                _galaxyData.infos.Add(id, info);
        }

        public void AddBody(CustomBodyInfo info)
        {
            if (_curGalaxy == null)
                throw new Exception("Need choose galaxy.");
            _curGalaxy.bodies.Add(info);
        }

        public void RemoveBody(CustomBodyInfo info)
        {
            if (_curGalaxy == null)
                throw new Exception("Need choose galaxy.");
            _curGalaxy.bodies.Remove(info);
        }
        

        public bool RemoveCustomGalaxy(int id)
        {
            if (_galaxyData.infos.ContainsKey(id))
            {
                _galaxyData.infos.Remove(id);
                return true;
            }

            return false;
        }
    }
}