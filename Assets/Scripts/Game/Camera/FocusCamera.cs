using System;
using Game.SolarSystem;
using HFramework;
using UnityEngine;

namespace Game
{
    public class FocusCamera : MonoBehaviour
    {
        private const float SCREEN_RATIO = 1.414f;
        private const float PADDING_RATIO = -0.17f;
        
        private Camera _cam;
        private CelestialBody _target;
        
        public Vector3 focusPadding = Vector3.zero;
        public Vector3 lookPadding = Vector3.zero;
        public float scrollSpeed = 10;
        public float rotateSpeed = 30;
        public float padding = 5;
        private Vector2 _mouseInput;
        private Vector3 _startRot;
        private Vector3 _starPos;

        public Camera Camera => _cam;

        private void Awake()
        {
            _cam = GetComponent<Camera>();
            HEntry.EventMgr.AddListener<Vector2>(ClientEvent.GET_LOOK, OnLookInput);
            HEntry.EventMgr.AddListener<float>(ClientEvent.GET_MOUSE_SCROLL, OnScroll);
            _startRot = transform.eulerAngles;
            _starPos = transform.position;
        }

        private void OnDestroy()
        {
            HEntry.EventMgr.RemoveListener<Vector2>(ClientEvent.GET_LOOK, OnLookInput);
            HEntry.EventMgr.RemoveListener<float>(ClientEvent.GET_MOUSE_SCROLL, OnScroll);

        }

        private void OnLookInput(Vector2 vector)
        {
            _mouseInput = vector;
        }

        private void OnScroll(float delta)
        {
            var val = _cam.fieldOfView - delta * scrollSpeed;
            val = Mathf.Clamp(val, 50, 100);
            _cam.fieldOfView = val;
        }

        private void LateUpdate()
        {
            if (_target != null)
            {
                var camRad = _cam.fieldOfView * Mathf.Deg2Rad;
                var logicDis = _target.transform.localScale.x * SCREEN_RATIO / Mathf.Tan(camRad / 2);
                focusPadding.y = logicDis * Mathf.Sqrt(2) / 2;
                focusPadding.z = -logicDis * Mathf.Sqrt(2) / 2;
                lookPadding.x = -logicDis * PADDING_RATIO;
                Vector3 targetPos = _target.transform.position + focusPadding;
                transform.position = targetPos;
                transform.rotation =
                    Quaternion.LookRotation(_target.transform.position + lookPadding - transform.position,
                        transform.up);
                return;
            }
            Quaternion targetRot = Quaternion.AngleAxis(_mouseInput.x, Vector3.up) *
                                   Quaternion.AngleAxis(_mouseInput.y, Vector3.forward) * transform.rotation;
            //控制z轴偏移
            //限制旋转角度
            var fixedY =
                TransformUtils.ClampAngle(targetRot.eulerAngles.y, _startRot.y - padding, _startRot.y + padding);
            var fixedX = 
                TransformUtils.ClampAngle(targetRot.eulerAngles.x, _startRot.x - padding, _startRot.x + padding);

            targetRot = Quaternion.Euler(fixedX, fixedY, _startRot.z);
            
            //旋转
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
        }

        public void SetTarget(CelestialBody body)
        {
            _target = body;
            if (body == null)
            {
                transform.position = _starPos;
                transform.rotation = Quaternion.Euler(_startRot);
            }
        }
    }
}