using System;
using System.Collections.Generic;
using Hali_Framework;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// 天体类
    /// </summary>
    [ExecuteInEditMode]
    public class CelestialBody : GravityObject
    {
        //自定义天体参数
        public string bodyName = "Undefined";
        public float radius = 1;//半径
        public float gravity = 1;//重力
        public Vector3 initialVelocity = Vector3.zero;//初速度

        //计算速度的参数
        private Vector3 _calcVector;
        private float _calcDst;
        private Vector3 _calcDir;
        private Vector3 _calcAcceleration;
        private Vector3 _calcVelocity;

        protected override void Awake()
        {
            base.Awake();
            rb.velocity = initialVelocity;
            SolarSystem.Instance.RegisterBody(this);
        }

        private void OnDisable()
        {
            SolarSystem.Instance.RemoveBody(this);
        }

        //当参数改变时
        private void OnValidate()
        {
            this.gameObject.name = bodyName;
            //质量计算公式
            //M = gR² / G
            rb.mass = gravity * radius * radius / GameConst.GRAVITATIONAL_CONSTANT;
        }

        /// <summary>
        /// 计算时间间隔后其他天体对该天体施加力后，该天体的理论位置
        /// </summary>
        /// <param name="allBodies">所有天体</param>
        /// <param name="timeStep">时间间隔</param>
        public void CalculateVelocity(List<CelestialBody> allBodies, float timeStep)
        {
            //重置参数
            _calcVelocity = Vector3.zero;
            
            foreach (var body in allBodies)
            {
                if(body == this) continue;
                _calcVector = body.rb.position - this.rb.position;
                _calcDir = _calcVector.normalized;
                _calcDst = _calcVector.sqrMagnitude;
                //加速度计算公式
                //F = GMm / r² = ma
                //a = GM / r²
                _calcAcceleration = _calcDir * (GameConst.GRAVITATIONAL_CONSTANT * body.rb.mass) / _calcDst;
                
                //V = v0 + at
                _calcVelocity += _calcAcceleration * timeStep;
            }
        }

        /// <summary>
        /// 计算理论位置后更新天体位置，
        /// 将计算与位移分离使移动更稳定
        /// </summary>
        public void UpdatePosition(float timeStep)
        {
            this.rb.MovePosition(this.rb.position + _calcVelocity * timeStep);
        }
    }
}
