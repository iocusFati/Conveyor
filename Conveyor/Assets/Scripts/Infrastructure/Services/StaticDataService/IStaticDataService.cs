using CameraBehaviour;
using GamePlay.FoodFolder;
using GamePlay.PlayerFolder;
using GamePlay.QuestFolder;
using UI.Data;

namespace Infrastructure.Services.StaticDataService
{
    public interface IStaticDataService : IService
    {
        FoodStaticData FoodData { get; }
        PlayerStaticData PlayerData { get; }
        CameraStaticData CameraData { get; }
        QuestStaticData QuestData { get; }
        UIStaticData UIData { get; }
    }
}