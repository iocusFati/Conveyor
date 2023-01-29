using TMPro;
using UnityEngine;

namespace GamePlay.PlayerFolder
{
    public class Player : MonoBehaviour
    {
        public TextMeshPro FoodPickText;
        public Transform CameraLookAtPoint;

        public IPlayerAnimation Animation { get; set; }

        public void Construct(IPlayerAnimation playerAnimation)
        {
            Animation = playerAnimation;
        }
    }
}