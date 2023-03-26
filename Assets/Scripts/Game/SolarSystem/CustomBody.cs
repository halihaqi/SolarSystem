using Game.Model.CustomGalaxy;
using HFramework;
using UnityEngine;

namespace Game.SolarSystem
{
    public class CustomBody : CelestialBody
    {
        private MeshRenderer _mr;
        private LineRenderer _lr;
        private TrailRenderer _tr;
        public Color bodyColor;
        
        protected override void Awake()
        {
            base.Awake();
            _mr = GetComponent<MeshRenderer>();
            _tr = GetComponent<TrailRenderer>();
            _lr = GetComponentInChildren<LineRenderer>();
        }

        public void SetData(CustomBodyInfo info)
        {
            bodyName = info.bodyName;
            mass = info.mass;
            radius = info.radius;
            distance = info.distance;
            var color = info.bodyColor;
            bodyColor = new Color(color[0], color[1], color[2], color[3]);
            UpdateBody();
            SolarSystem.Instance.RegisterBody(this);
        }

        public void UpdateBody()
        {
            gameObject.name = bodyName;
            rb.mass = mass;
            transform.position = rb.position = Vector3.right * distance;
            transform.localScale = radius * GameConst.CELESTIAL_ZOOM * Vector3.one;
            _lr.enabled = SolarSystem.Instance.OrbitEnabled;
            var newMat = Instantiate(_mr.sharedMaterial);
            newMat.color = bodyColor;
            _mr.material = newMat;
            _tr.startColor = _lr.startColor = bodyColor;
            _tr.endColor = _lr.endColor = bodyColor;
        }
        
        protected override void OnValidate()
        {
        }
    }
}