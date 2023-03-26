using System;
using System.Collections.Generic;
using HFramework;
using UnityEngine;

namespace Game.SolarSystem
{
    [ExecuteInEditMode]
    public class DebugOrbit : MonoBehaviour
    {
        private class VirtualCelestial
        {
            public string name;
            public Vector3 position;
            public Vector3 velocity;
            public float mass;
            public Vector3[] renderPos;
        }
        
        [SerializeField] private int perStep = 10000;//预览的步数
        [SerializeField] private float perStepDelta = 0.1f;//预览的步长
        [SerializeField] private CelestialBody centerBody;
        [SerializeField] private bool isCenterStatic = false;
        [HideInInspector] public bool useDebug = false;

        private List<CelestialBody> _bodies;
        private List<VirtualCelestial> _virtualBodies;
        
        //计算速度的参数
        private Vector3 _calcVector;
        private float _calcDst;
        private Vector3 _calcDir;
        private Vector3 _calcAcceleration;

        private void OnEnable()
        {
            this.enabled = !Application.isPlaying;
        }

        private void OnDisable()
        {
            if(!Application.isPlaying) return;
            ClearOrbit();
        }

        private void Update()
        {
            if(!useDebug) return;
            GenerateOrbit();
        }

        public void GenerateOrbit()
        {
            //重置参数
            _bodies = new List<CelestialBody>();
            _virtualBodies = new List<VirtualCelestial>();
            var objs = GameObject.FindGameObjectsWithTag(GameConst.CELESTIAL_TAG);
            foreach (var obj in objs)
            {
                if (obj.TryGetComponent<CelestialBody>(out var celestialBody))
                {
                    VirtualCelestial vc = new VirtualCelestial();
                    vc.position = celestialBody.transform.position;
                    vc.mass = celestialBody.mass;
                    vc.name = celestialBody.bodyName;
                    vc.renderPos = new Vector3[perStep];
                    _bodies.Add(celestialBody);
                    _virtualBodies.Add(vc);
                }
            }
            
            //初速度
            foreach (var body in _virtualBodies)
            {
                if(body.name == (centerBody != null ? centerBody.bodyName : null)) continue;
                InitVelocity(body);
            }

            //计算需要渲染的点
            for (int i = 0; i < perStep; i++)
            {
                for (int j = 0; j < _virtualBodies.Count; j++)
                {
                    if(isCenterStatic && centerBody == _bodies[j]) continue;
                    foreach (var body in _virtualBodies)
                    {
                        if(body == _virtualBodies[j]) continue;
                        _calcVector = body.position - _virtualBodies[j].position;
                        _calcDir = _calcVector.normalized;
                        _calcDst = _calcVector.sqrMagnitude;
                        //加速度计算公式
                        //F = GMm / r² = ma
                        //a = GM / r²
                        _calcAcceleration = _calcDir * (GameConst.GRAVITATIONAL_CONSTANT * body.mass) / _calcDst;
                        _virtualBodies[j].velocity += _calcAcceleration * perStepDelta;
                    }

                    _virtualBodies[j].position += _virtualBodies[j].velocity * perStepDelta;
                    _virtualBodies[j].renderPos[i] = _virtualBodies[j].position;
                }
            }

            //渲染点位
            for (int i = 0; i < _bodies.Count; i++)
            {
                var body = _bodies[i];
                var virtualBody = _virtualBodies[i];
                var lineRenderer = body.GetComponentInChildren<LineRenderer>();
                if(lineRenderer == null) continue;
                lineRenderer.enabled = true;
                lineRenderer.positionCount = virtualBody.renderPos.Length;
                lineRenderer.SetPositions(virtualBody.renderPos);
            }
            Debug.Log("Orbit Render Complete!");
        }

        public void ClearOrbit()
        {
            if(_bodies != null)
                foreach (var body in _bodies)
                {
                    var lineRenderer = body.GetComponentInChildren<LineRenderer>();
                    if(lineRenderer == null) continue;
                    lineRenderer.positionCount = 0;
                    lineRenderer.enabled = false;
                }
            _bodies?.Clear();
            _virtualBodies?.Clear();
        }


        private void InitVelocity(VirtualCelestial body)
        {
            foreach (var c in _virtualBodies)
            {
                if(c == body) continue;
                
                float r = Vector3.Distance(body.position, c.position);
                Vector3 dir = (c.position - body.position).normalized;
                body.velocity += Vector3.Cross(dir, Vector3.up) *
                                     Mathf.Sqrt(GameConst.GRAVITATIONAL_CONSTANT * c.mass / r);
            }
        }
    }
}