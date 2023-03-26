using System.Collections.Generic;
using HFramework;
using UnityEngine;

namespace Game.SolarSystem
{
    public class SolarSystem : SingletonAutoMono<SolarSystem>
    {
        [SerializeField] private CelestialBody centerBody;
        public bool isCenterStatic = false;
        
        private List<CelestialBody> _bodies;
        
        //计算速度的参数
        private Vector3 _calcVector;
        private float _calcDst;
        private Vector3 _calcDir;
        private Vector3 _calcAcceleration;
        private Vector3 _calcVelocity;

        private bool _orbitEnabled;

        public CelestialBody CenterBody
        {
            get => centerBody;
            set
            {
                centerBody = value;
                if(value != null)
                    centerBody.calcVelocity = Vector3.zero;
            }
        }
        
        private void Awake()
        {
            _bodies = new List<CelestialBody>();
            Time.fixedDeltaTime = GameConst.TIME_STEP;
        }

        private void FixedUpdate()
        {
            //遍历计算天体理论位移
            for (int i = 0; i < _bodies.Count; i++)
            {
                CalculateVelocity(_bodies[i], _bodies, GameConst.TIME_STEP);
            }
            
            //实行天体位移
            for (int i = 0; i < _bodies.Count; i++)
            {
                if(isCenterStatic && centerBody.bodyName == _bodies[i].bodyName) continue;
                _bodies[i].UpdatePosition(GameConst.TIME_STEP);
            }
        }

        public void Init()
        {
            ClearBody();
            var objs = GameObject.FindGameObjectsWithTag(GameConst.CELESTIAL_TAG);
            foreach (var obj in objs)
            {
                _bodies.Add(obj.GetComponent<CelestialBody>());
            }

            foreach (var body in _bodies)
            {
                if(centerBody != null && centerBody.name == body.name) return;
                InitVelocity(body);
            }
        }

        public void RegisterBody(CelestialBody body)
        {
            if(_bodies.Contains(body)) return;
            _bodies.Add(body);
            if(centerBody != null && centerBody.name == body.name) return;
            InitVelocity(body);
        }

        public void RemoveBody(CelestialBody body)
        {
            if(!_bodies.Contains(body)) return;
            _bodies.Remove(body);
        }

        public void ClearBody()
        {
            _bodies.Clear();
        }

        /// <summary>
        /// 计算时间间隔后其他天体对该天体施加力后，该天体的理论位置
        /// </summary>
        /// <param name="calcBody">计算速度的天体</param>
        /// <param name="allBodies">所有天体</param>
        /// <param name="timeStep">时间间隔</param>
        public void CalculateVelocity(CelestialBody calcBody, List<CelestialBody> allBodies, float timeStep)
        {
            //重置参数
            _calcVelocity = calcBody.calcVelocity;
            
            foreach (var body in allBodies)
            {
                if(body == calcBody) continue;
                if(body.rb.position == calcBody.rb.position) continue;
                _calcVector = body.rb.position - calcBody.rb.position;
                _calcDir = _calcVector.normalized;
                _calcDst = _calcVector.sqrMagnitude;
                //加速度计算公式
                //F = GMm / r² = ma
                //a = GM / r²
                _calcAcceleration = _calcDir * (GameConst.GRAVITATIONAL_CONSTANT * body.rb.mass) / _calcDst;
                
                //V = v0 + at
                _calcVelocity += _calcAcceleration * timeStep;
            }

            calcBody.calcVelocity = _calcVelocity;
        }

        /// <summary>
        /// 根据圆周运动公式初始化天体速度
        /// v = √(G * M / r)
        /// </summary>
        /// <param name="body">要初始化的天体</param>
        public void InitVelocity(CelestialBody body)
        {
            var initVelocity = Vector3.zero;
            foreach (var cb in _bodies)
            {
                if(cb == body) continue;
                body.transform.LookAt(cb.rb.transform);
                float r = Vector3.Distance(body.rb.position, cb.rb.position);
                initVelocity += body.rb.transform.right *
                                Mathf.Sqrt(GameConst.GRAVITATIONAL_CONSTANT * cb.rb.mass / r);
            }

            body.calcVelocity = initVelocity;
        }

        public CelestialBody GetBodyByName(string bodyName)
        {
            return _bodies.Find(o => o.name == bodyName);
        }

        public bool OrbitEnabled
        {
            get => _orbitEnabled;
            set
            {
                if(_orbitEnabled == value) return;
                _orbitEnabled = value;
                foreach (var body in _bodies)
                {
                    body.OrbitEnabled = value;
                }
            }
        }
    }
}