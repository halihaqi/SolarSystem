using Game.SolarSystem;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(DebugOrbit))]
    public class OrbitEditor : UnityEditor.Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var b = ((DebugOrbit)target).useDebug;
            if (GUILayout.Button(b ? "关闭轨迹预测" : "开启预测轨迹"))
            {
                ((DebugOrbit)target).ClearOrbit();
                ((DebugOrbit)target).useDebug = !b;
            }
        }
    }
}