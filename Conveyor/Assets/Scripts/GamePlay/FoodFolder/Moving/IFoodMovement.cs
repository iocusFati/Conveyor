using UnityEngine;

namespace GamePlay.FoodFolder.Moving
{
    public interface IFoodMovement
    {
        void StartMoving(Food food);
        Transform Conveyor { set; }
    }
}