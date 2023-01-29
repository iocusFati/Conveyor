using System.Collections.Generic;
using GamePlay.FoodFolder;

namespace Infrastructure.Assets
{
    public abstract class AssetPaths
    {
        public const string FoodData = "StaticData/FoodData";
        public const string QuestData = "StaticData/QuestData";
        public const string PlayerData = "StaticData/PlayerData";
        public const string CameraData = "StaticData/CameraData";
        public const string UIData = "StaticData/UIData";

        public const string UIHUD = "Prefabs/UI/HUD";
        public const string UIRoot = "Prefabs/UI/UIRoot";
        public const string Curtain = "Prefabs/UI/Curtain";

        public const string PlayerPath = "Prefabs/Player/Player";
        public const string Location = "Prefabs/Environment/Location";

        public static readonly Dictionary<string, FoodType> FoodPaths = new(){
            { "Prefabs/Food/Beer", FoodType.Beer },
            {"Prefabs/Food/Ham", FoodType.Ham},
            {"Prefabs/Food/Watermelon", FoodType.Watermelon}
        };
    }
}