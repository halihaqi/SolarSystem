using System.Collections.Generic;
using Hali_Framework;
using UnityEngine;

namespace Game.SolarSystem
{
    public class SolarSystem : Singleton<SolarSystem>
    {
        private List<CelestialBody> _bodies = new ();

        public void Init()
        {
            Time.fixedDeltaTime = GameConst.TIME_STEP;
        }
        
        public void Run()
        {
            //遍历计算天体理论位移
            for (int i = 0; i < _bodies.Count; i++)
            {
                _bodies[i].CalculateVelocity(_bodies, GameConst.TIME_STEP);
            }
            
            //实行天体位移
            for (int i = 0; i < _bodies.Count; i++)
            {
                _bodies[i].UpdatePosition(GameConst.TIME_STEP);
            }
        }

        public void RegisterBody(CelestialBody body)
        {
            if(_bodies.Contains(body)) return;
            _bodies.Add(body);
        }

        public void RemoveBody(CelestialBody body)
        {
            if(!_bodies.Contains(body)) return;
            _bodies.Remove(body);
        }
    }
}