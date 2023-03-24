using System;
using HFramework;
using UnityEngine;

namespace Game.SolarSystem
{
    /// <summary>
    /// 天体类
    /// </summary>
    public class CelestialBody : GravityObject
    {
        //自定义天体参数
        public string bodyName = "Undefined";
        public float mass = 1;
        public float radius = 1;//半径
        public float rotateSpeed = 3;
        public float distance;//距太阳距离

        public Vector3 calcVelocity;
        private TrailRenderer _orbit;

        public bool OrbitEnabled
        {
            get => _orbit.enabled;
            set
            {
                _orbit.Clear();
                _orbit.enabled = value;
            }
        }

        protected override void Awake()
        {
            if(!Application.isPlaying) return;
            base.Awake();
            rb.useGravity = false;
            //质量计算公式
            //M = gR² / G
            //rb.mass = gravity * radius * radius / GameConst.GRAVITATIONAL_CONSTANT;
            rb.mass = mass;
            gameObject.tag = nameof(CelestialBody);
            transform.GetComponentInChildren<LineRenderer>().enabled = false;
            _orbit = GetComponent<TrailRenderer>();
        }

        private void FixedUpdate()
        {
            transform.RotateAround(transform.position, Vector3.up, Time.fixedDeltaTime * rotateSpeed);
        }

        private void OnValidate()
        {
            gameObject.name = bodyName;
            gameObject.GetComponent<Rigidbody>().mass = mass;
            transform.localScale = radius * GameConst.CELESTIAL_ZOOM * Vector3.one;
            transform.position = Vector3.right * distance;
            if (!gameObject.GetComponentInChildren<LineRenderer>())
            {
                var obj = new GameObject("LineRenderer");
                obj.transform.SetParent(transform, false);
                obj.AddComponent<LineRenderer>().enabled = false;
            }
        }
        
        /// <summary>
        /// 计算理论位置后更新天体位置，
        /// 将计算与位移分离使移动更稳定
        /// </summary>
        /// <param name="timeStep"></param>
        public void UpdatePosition(float timeStep)
        {
            rb.MovePosition(Position + calcVelocity * timeStep);
        }
    }
}
