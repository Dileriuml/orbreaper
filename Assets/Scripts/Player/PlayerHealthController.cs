using Characters;
using OrbReaper;
using OrbReaper.Signals;
using Zenject;

namespace Player
{
    public class PlayerHealthController : LivingBeingController
    {
        private readonly SignalBus signalBus;

        public PlayerHealthController(SignalBus signalBus, LivingBeingModel livingBeingModel) 
            : base(livingBeingModel)
        {
            this.signalBus = signalBus;
        }
        
        protected override void OnDamageReceived()
        {
            base.OnDamageReceived();
            signalBus.Fire<GameEvents.PlayerEvents.HealthChangedSignal>();
        }

        protected override void OnHealingReceived()
        {
            base.OnHealingReceived();
            signalBus.Fire<GameEvents.PlayerEvents.HealthChangedSignal>();
        }
    }
}