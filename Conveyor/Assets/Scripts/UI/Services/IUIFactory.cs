using System;
using Infrastructure.Services;
using UI.HUD;

namespace UI.Services
{
    public interface IUIFactory : IService
    {
        HUD_Display CreateHUD();
        event Action<HUD_Display> OnHUDCreated;
        Curtain CreateCurtain();
    }
}