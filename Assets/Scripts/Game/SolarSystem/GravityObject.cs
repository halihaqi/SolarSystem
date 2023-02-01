using UnityEngine;

namespace Game.SolarSystem
{
    /// <summary>
    /// 受重力影响的物体类，必须带有RigidBody
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class GravityObject : MonoBehaviour
    {
        public Rigidbody rb { get; private set; }
        
        //供外部获取参数
        public float Mass => rb.mass;//物体质量
        public Vector3 Position => rb.position;//当前坐标
        public Vector3 Velocity => rb.velocity;//当前速度

        protected virtual void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
    }
}