using System.Collections;
using GamePlay.FoodFolder.FoodSpawnerFolder.Pool;
using GamePlay.FoodFolder.Moving;
using Infrastructure.Bootstrapper;
using Infrastructure.States;
using UnityEngine;

namespace GamePlay.FoodFolder.FoodSpawnerFolder
{
    public class FoodSpawner : MonoBehaviour, IFoodSpawner
    {
        [SerializeField] private Transform _initialPoint;
        
        private IFoodPool _foodPool;
        private ICoroutineRunner _coroutineRunner;
        private IFoodMovement _movement;
        private IGameStateMachine _stateMachine;

        private float _minTimeToNextSpawn;
        private float _maxTimeToNextSpawn;

        private Coroutine _spawnFood;

        public void Construct(ICoroutineRunner coroutineRunner, IGameStateMachine stateMachine,
            FoodStaticData foodStaticData)
        {
            _coroutineRunner = coroutineRunner;
            _stateMachine = stateMachine; 

            _minTimeToNextSpawn = foodStaticData.MinTimeToNextSpawn;
            _maxTimeToNextSpawn = foodStaticData.MaxTimeToNextSpawn;
        }
        
        public void Initialize(IFoodPool foodPool, IFoodMovement movement)
        {
            _foodPool = foodPool;
            _movement = movement;
        }

        public void StartSpawning()
        {
            _spawnFood = _coroutineRunner.StartCoroutine(SpawnFood());
            _movement.Conveyor = gameObject.transform;
        }

        public void StopSpawning() => 
            _coroutineRunner.StopCoroutine(_spawnFood);

        private IEnumerator SpawnFood()
        {
            while (true)
            {
                if (_stateMachine.IsInGameLoop)
                {
                    _foodPool.GetFood(out var food);
                    food.transform.position = _initialPoint.position;

                    yield return new WaitForSecondsRealtime(GapBetweenSpawns());
                }
                
                else
                    yield return null;
            }
        }

        private float GapBetweenSpawns()
        {
            return Random.Range(_minTimeToNextSpawn, _maxTimeToNextSpawn);
        }
    }
}