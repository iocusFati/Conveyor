using CameraBehaviour;
using GamePlay.FoodFolder;
using GamePlay.PlayerFolder;
using GamePlay.QuestFolder;
using Infrastructure.Assets;
using UI.Data;
using UnityEngine;

namespace Infrastructure.Services.StaticDataService
{
    public class StaticDataService : IStaticDataService
    {
        public FoodStaticData FoodData { get; private set; }
        public PlayerStaticData PlayerData { get; private set; }
        public QuestStaticData QuestData { get; private set; }
        public CameraStaticData CameraData { get; private set; }
        public UIStaticData UIData { get; private set; }


        public void Initialize()
        {
            LoadFoodData();
            LoadPlayerData();
            LoadQuestData();
            LoadCameraData();
            LoadUIData();
        }

        private void LoadFoodData() => 
            FoodData = Resources.Load<FoodStaticData>(AssetPaths.FoodData);

        private void LoadPlayerData() => 
            PlayerData = Resources.Load<PlayerStaticData>(AssetPaths.PlayerData);

        private void LoadQuestData() => 
            QuestData = Resources.Load<QuestStaticData>(AssetPaths.QuestData);

        private void LoadCameraData() => 
            CameraData = Resources.Load<CameraStaticData>(AssetPaths.CameraData);

        private void LoadUIData() => 
            UIData = Resources.Load<UIStaticData>(AssetPaths.UIData);
    }
}