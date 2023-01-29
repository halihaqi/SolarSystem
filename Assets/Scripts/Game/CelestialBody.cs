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

        private float _calcDst;//计算的天体距离
        private Vector3 _calcDir;//计算的施加力的方向
        private Vector3 _calcAcceleration;//计算的速度

        protected override void Awake()
        {
            base.Awake();
            rb.velocity = initialVelocity;
        }

        //当参数改变时
        private void OnValidate()
        {
            this.gameObject.name = bodyName;
            //质量计算公式
            //M = g * R² / G
            rb.mass = gravity * radius * radius / GameConst.GRAVITATIONAL_CONSTANT;
        }

        /// <summary>
        /// 计算其他天体对该天体施加力后，该天体的理论速度
        /// </summary>
        /// <param name="allBodies"></param>
        /// <param name="timeStep"></param>
        public void UpdateVelocity(CelestialBody[] allBodies, float timeStep)
        {
            //速度计算公式
            //
            foreach (var body in allBodies)
            {
                if(body == this) continue;
                _calcDir = (body.rb.position - this.rb.position).normalized;
                _calcDst = (body.rb.position - this.rb.position).sqrMagnitude;
                _calcAcceleration = _calcDir * GameConst.GRAVITATIONAL_CONSTANT * body.rb.mass / _calcDst;
                rb.velocity += _calcAcceleration * timeStep;
            }
        }
    }
}
