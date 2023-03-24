using UnityEngine;

namespace Game
{
    public class BeginCamera : MonoBehaviour
    {
        public Transform target;
        public float moveSpeed = 3;
        public float rotateSpeed = 10;
        public float padding = 5;
        private Vector2 _mouseInput;
        private Vector3 _startRot;
        private Vector3 _frontRot;

        private void Start()
        {
            _startRot = transform.localEulerAngles;
        }

        void FixedUpdate()
        {
            //上一帧欧拉角，用于限制旋转角度
            _frontRot = transform.localEulerAngles;
            _mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            Quaternion targetRot = Quaternion.AngleAxis(_mouseInput.x, Vector3.up) *
                                   Quaternion.AngleAxis(-_mouseInput.y, Vector3.forward) * transform.rotation;
            //控制z轴偏移
            targetRot = Quaternion.Euler(targetRot.eulerAngles.x, _startRot.y, targetRot.eulerAngles.z);
            //旋转
            
            //限制旋转角度
            // if (transform.localEulerAngles.z > _startRot.z + padding
            //     || transform.localEulerAngles.z < _startRot.z - padding)
            // {
            //     targetRot.z = _frontRot.z;
            // }
            //
            // if (transform.localEulerAngles.x > _startRot.x + padding
            //     || transform.localEulerAngles.x < _startRot.x - padding)
            // {
            //     targetRot.x = _frontRot.x;
            // }

            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
            //跟随目标
            var targetPos = transform.position;
            targetPos.z = target.position.z;
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}