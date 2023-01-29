using System.Collections.Generic;
using System.Linq;
using GamePlay.FoodFolder;
using UnityEngine;

namespace GamePlay.LocationFolder.BasketFolder
{
    public class Basket : MonoBehaviour
    {
        private int _currentPosition;
        private int _maxProductsInBasket;

        [SerializeField] private Animator _animator;  
        [SerializeField] private List<ProductInBasket> _products;

        private BasketAnimation _animation;
        private readonly List<Food> _activeProducts = new();

        public void Construct(FoodStaticData foodStaticData)
        {
            _animation = new BasketAnimation(_animator);
            
            _maxProductsInBasket = foodStaticData.MaxProductsInBasket;
        }
        
        public void PutInTheBasket(FoodType type)
        {
            _animation.Shake();
            if (_currentPosition >= _maxProductsInBasket) return;
            
            Food product = _products[_currentPosition].Products.FirstOrDefault(product => product.Type == type);
            _activeProducts.Add(product);
            product.gameObject.SetActive(true);
            _currentPosition++;
        }

        public void GoToDefault()
        {
            foreach (var product in _activeProducts) 
                product.gameObject.SetActive(false);
            
            _activeProducts.Clear();
            _currentPosition = 0;
        }    
    }
}