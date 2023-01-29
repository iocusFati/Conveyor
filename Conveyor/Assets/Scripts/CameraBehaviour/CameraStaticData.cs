using UnityEngine;

namespace CameraBehaviour
{
    [CreateAssetMenu(fileName = "CameraData", menuName = "StaticData/CameraData")]
    public class CameraStaticData : ScriptableObject
    {
        public float MoveInterpolant;
        public float RotateInterpolant;
        public float PercentageBeforeStop;
    }
}