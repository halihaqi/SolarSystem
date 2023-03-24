using HFramework;
using UnityEngine;

namespace Game
{
    public class CameraMaster : SingletonMono<CameraMaster>
    {
        [SerializeField] private RomaCamera romaCamera;
        [SerializeField] private FocusCamera focusCamera;

        public RomaCamera RomaCamera => romaCamera;
        public FocusCamera FocusCamera => focusCamera;

        public void SwitchCamera(bool isRoma)
        {
            romaCamera.gameObject.SetActive(isRoma);
            focusCamera.gameObject.SetActive(!isRoma);
        }
    }
}