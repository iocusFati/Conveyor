using UnityEngine;

namespace GamePlay.PlayerFolder
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/PlayerData")]
    public class PlayerStaticData : ScriptableObject
    {
        [Header("PickFoodText")] 
        public float RaiseBy;
        public float Duration;
        public Color DefaultColor;

        [Header("Spawn")] 
        public Vector3 OriginRotation;

        [Header("Animation")]
        public float WeightInterpolant;
        public int WinAnimationNumber;
    }
}