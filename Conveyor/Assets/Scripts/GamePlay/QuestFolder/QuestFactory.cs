using System;
using GamePlay.FoodFolder;
using Random = UnityEngine.Random;

namespace GamePlay.QuestFolder
{
    public class QuestFactory : IQuestFactory
    {
        private readonly int _maxProductNum;
        
        private int _foodTargetNum;
        private FoodType _foodType;

        public event Action<Quest> OnQuestCreated;

        public QuestFactory(QuestStaticData questStaticData)
        {
            _maxProductNum = questStaticData.MaxTargetProductsNum;
        }

        public Quest Create()
        {
            int foodTypesLength = Enum.GetNames(typeof(FoodType)).Length;
            _foodTargetNum = Random.Range(1, _maxProductNum + 1);
            _foodType = (FoodType)Random.Range(0, foodTypesLength);
            
            var quest = new Quest(_foodType, _foodTargetNum);
            OnQuestCreated.Invoke(quest);

            return quest;
        }
    }
}