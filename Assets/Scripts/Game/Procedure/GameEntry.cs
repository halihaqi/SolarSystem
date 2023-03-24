using System;
using HFramework;
using UnityEngine;

namespace Game.Procedure
{
    public class GameEntry : MonoBehaviour
    {
        private void Awake()
        {
            var gm = GameMaster.Instance;
        }
    }
}