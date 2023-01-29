using System;
using Infrastructure.Services;

namespace GamePlay.QuestFolder
{
    public interface IQuestFactory : IService
    {
        Quest Create();
        event Action<Quest> OnQuestCreated;
    }
}