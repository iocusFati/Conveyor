using System;
using GamePlay.FoodFolder;

namespace GamePlay.QuestFolder
{
    public class Quest
    {
        private FoodType Type { get; }
        public int TargetNum { get; }
        public int CurrentProgress { get; private set; }

        public event Action<int> OnProgressUpdate;

        public Quest(FoodType type, int targetNum)
        {
            Type = type;
            TargetNum = targetNum;
        }

        public void TryUpdateProgress(FoodType type, out bool reachedTarget)
        {
            if (type == Type)
            {
                CurrentProgress++;
                OnProgressUpdate.Invoke(CurrentProgress);
                
                if (CurrentProgress >= TargetNum)
                {
                    reachedTarget = true;
                    return;
                }
            }

            reachedTarget = false;
        }
        
        public override string ToString() =>
            TargetNum > 1 
                ? new string($"Collect {TargetNum} {Type.ToString()}s") 
                : new string($"Collect {TargetNum} {Type.ToString()}");
    }
}