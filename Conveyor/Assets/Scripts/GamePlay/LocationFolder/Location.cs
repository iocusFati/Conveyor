using System.Collections.Generic;
using GamePlay.FoodFolder.FoodSpawnerFolder;
using GamePlay.LocationFolder.BasketFolder;
using UnityEngine;

namespace GamePlay.LocationFolder
{
    public class Location : MonoBehaviour
    {
        public List<GameObject> DeactivateObjects;
        public Basket Basket;

        public IFoodSpawner FoodSpawner { get; set; }
    }
}