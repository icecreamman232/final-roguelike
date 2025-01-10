
using SGGames.Scripts.Core;

namespace SGGames.Scripts.StatusEffects
{
    public class PlayerStatusEffectHandler : StatusEffectHandler, IPlayerStatusEffectService
    {
        protected override void Start()
        {
            base.Start();
            ServiceLocator.RegisterService<PlayerStatusEffectHandler>(this);
        }
    }
}

