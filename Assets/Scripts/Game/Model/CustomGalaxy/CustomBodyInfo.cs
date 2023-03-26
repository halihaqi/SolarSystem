using System;
using UnityEngine;

namespace Game.Model.CustomGalaxy
{
    [Serializable]
    public class CustomBodyInfo
    {
        public string bodyName = "Undefined";
        public float mass = 1;//质量
        public float radius = 1;//半径
        public float rotateSpeed = 3;//自转速度
        public float distance;//距太阳距离
        public float[] bodyColor;//天体颜色
        
        public CustomBodyInfo(){}

        public CustomBodyInfo(string bodyName, float mass, float radius, float rotateSpeed, float distance, Color bodyColor)
        {
            this.bodyName = bodyName;
            this.mass = mass;
            this.radius = radius;
            this.rotateSpeed = rotateSpeed;
            this.distance = distance;
            this.bodyColor = new[] { bodyColor.r, bodyColor.g, bodyColor.b, bodyColor.a };
        }
    }
}