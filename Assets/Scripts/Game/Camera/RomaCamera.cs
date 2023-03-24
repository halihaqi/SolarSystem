using System;
using Game.SolarSystem;
using HFramework;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Camera))]
    public class RomaCamera : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3;
        [SerializeField] private float lookSensitive = 3;
        [SerializeField] private float sprintSpeed = 10;
        [SerializeField] private float sprintMultSpeed = 10;
        [SerializeField] private float sprintThreshold = 80;

        private Camera _cam;
        private bool _useMove = false;
        private bool _isSprint = false;
        private float _threshold = 0.01f;//输入最低门槛
        private float _camTargetYaw;
        private float _camTargetPitch;
        private float _realSpeed;

        public float CurSpeed => _realSpeed;
        public Camera Camera => _cam;

        private void Awake()
        {
            _cam = GetComponent<Camera>();
            
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY_DOWN, OnSprint);
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY, OnSprinting);
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY_UP, OnUnSprint);

            HEntry.EventMgr.AddListener<int>(ClientEvent.GET_MOUSE_BUTTON_DOWN, OnUseMove);
            HEntry.EventMgr.AddListener<int>(ClientEvent.GET_MOUSE_BUTTON_UP, OnUnUseMove);
            HEntry.EventMgr.AddListener<Vector2>(ClientEvent.GET_LOOK, OnLook);
            HEntry.EventMgr.AddListener<Vector2>(ClientEvent.GET_MOVE, OnMove);
            HEntry.InputMgr.Enabled = true;

            var oriAngle = transform.eulerAngles;
            _camTargetPitch = oriAngle.x;
            _camTargetYaw = oriAngle.y;
            _realSpeed = moveSpeed;
        }

        private void OnEnable()
        {
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY_DOWN, OnSprint);
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY, OnSprinting);
            HEntry.EventMgr.AddListener<KeyCode>(ClientEvent.GET_KEY_UP, OnUnSprint);

            HEntry.EventMgr.AddListener<int>(ClientEvent.GET_MOUSE_BUTTON_DOWN, OnUseMove);
            HEntry.EventMgr.AddListener<int>(ClientEvent.GET_MOUSE_BUTTON_UP, OnUnUseMove);
            HEntry.EventMgr.AddListener<Vector2>(ClientEvent.GET_LOOK, OnLook);
            HEntry.EventMgr.AddListener<Vector2>(ClientEvent.GET_MOVE, OnMove);
            HEntry.InputMgr.Enabled = true;
        }

        private void OnDisable()
        {
            HEntry.EventMgr.RemoveListener<KeyCode>(ClientEvent.GET_KEY_DOWN, OnSprint);
            HEntry.EventMgr.RemoveListener<KeyCode>(ClientEvent.GET_KEY, OnSprinting);
            HEntry.EventMgr.RemoveListener<KeyCode>(ClientEvent.GET_KEY_UP, OnUnSprint);
            
            HEntry.EventMgr.RemoveListener<int>(ClientEvent.GET_MOUSE_BUTTON_DOWN, OnUseMove);
            HEntry.EventMgr.RemoveListener<int>(ClientEvent.GET_MOUSE_BUTTON_UP, OnUnUseMove);
            HEntry.EventMgr.RemoveListener<Vector2>(ClientEvent.GET_LOOK, OnLook);
            HEntry.EventMgr.RemoveListener<Vector2>(ClientEvent.GET_MOVE, OnMove);
        }
        
        private void OnSprint(KeyCode key)
        {
            if (key == KeyCode.LeftShift)
            {
                _isSprint = true;
                _realSpeed = sprintSpeed;
            }
        }
        
        private void OnSprinting(KeyCode key)
        {
            if (key == KeyCode.LeftShift)
            {
                _realSpeed += Time.deltaTime * sprintMultSpeed;
                _realSpeed = Mathf.Min(sprintThreshold, _realSpeed);
            }
        }
        
        private void OnUnSprint(KeyCode key)
        {
            if (key == KeyCode.LeftShift)
            {
                _isSprint = false;
                _realSpeed = moveSpeed;
            }
        }

        private void OnUseMove(int key)
        {
            if (key == 1)
            {
                _useMove = true;
                HEntry.InputMgr.CursorMode = CursorLockMode.Locked;
            }
        }

        private void OnUnUseMove(int key)
        {
            if (key == 1)
            {
                _useMove = false;
                HEntry.InputMgr.CursorMode = CursorLockMode.None;
            }
        }

        private void OnLook(Vector2 look)
        {
            if(!_useMove) return;
            if (look.sqrMagnitude < _threshold) return;
            
            _camTargetYaw += look.x * lookSensitive * Time.deltaTime;
            _camTargetPitch += look.y * -lookSensitive * Time.deltaTime;
            //旋转相机目标点
            transform.rotation = Quaternion.Euler(_camTargetPitch, _camTargetYaw, 0);
        }

        private void OnMove(Vector2 move)
        {
            if(!_useMove) return;
            if(move.sqrMagnitude < _threshold) return;

            Vector3 targetPos = transform.forward * (move.y * Time.deltaTime * _realSpeed);
            targetPos += transform.right * (move.x * Time.deltaTime * _realSpeed);
            transform.position += targetPos;
        }

        public void Leap(CelestialBody body)
        {
            var transform1 = body.transform;
            transform.position = transform1.position - body.radius * GameConst.CELESTIAL_ZOOM * transform1.forward;
            transform.LookAt(transform1);
        }
    }
}