using System;
using Characters;
using OrbReaper.Signals;
using OrbReaper.Utils;
using Zenject;

namespace OrbReaper.UI
{
    public class UIHandler : IDisposable, IInitializable
    {
        private readonly PlayerFacade playerFacade;
        private readonly SignalBus signalBus;
        private readonly ViewContainer viewContainer;
        
        public UIHandler( 
            PlayerFacade playerFacade,
            SignalBus signalBus,
            ViewContainer viewContainer)
        {
            this.playerFacade = playerFacade;
            this.signalBus = signalBus;
            this.viewContainer = viewContainer;
            
            viewContainer.HealthBar.SetFormatter(NumberFormatters.FormatFloatTwoPoints);
            viewContainer.PowerBar.SetFormatter(NumberFormatters.FormatFloatTwoPoints);
        }

        public void Dispose()
        {
            signalBus.Unsubscribe<GameEvents.PlayerEvents.HealthChangedSignal>(OnHealthChanged);
            signalBus.Unsubscribe<GameEvents.PlayerEvents.MaxHealthChangedSignal>(OnMaxHealthChanged);
            signalBus.Unsubscribe<GameEvents.PlayerEvents.PowerChangedSignal>(OnPowerChanged);
            signalBus.Unsubscribe<GameEvents.PlayerEvents.MaxPowerChangedSignal>(OnMaxPowerChanged);
        }

        public void Initialize()
        {
            signalBus.Subscribe<GameEvents.PlayerEvents.HealthChangedSignal>(OnHealthChanged);
            signalBus.Subscribe<GameEvents.PlayerEvents.MaxHealthChangedSignal>(OnMaxHealthChanged);
            signalBus.Subscribe<GameEvents.PlayerEvents.PowerChangedSignal>(OnPowerChanged);
            signalBus.Subscribe<GameEvents.PlayerEvents.MaxPowerChangedSignal>(OnMaxPowerChanged);
            OnHealthChanged();
            OnMaxHealthChanged();
            OnMaxPowerChanged();
            OnPowerChanged();
        }

        private void OnPowerChanged()
        {
            viewContainer.PowerBar.Value = playerFacade.PlayerModel.Character.SoulPower;
        }

        private void OnMaxPowerChanged()
        {
            viewContainer.PowerBar.MaxValue = playerFacade.PlayerModel.Character.MaxSoulPower;
        }

        private void OnHealthChanged()
        {
            viewContainer.HealthBar.Value = playerFacade.PlayerModel.LivingModel.Health;
        }
        
        private void OnMaxHealthChanged()
        {
            viewContainer.HealthBar.MaxValue = playerFacade.PlayerModel.LivingModel.MaxHealth;
        }
    }
}