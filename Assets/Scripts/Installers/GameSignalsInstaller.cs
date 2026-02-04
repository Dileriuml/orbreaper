using OrbReaper.Signals;
using Zenject;

namespace OrbReaper.Installers
{
    public class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<GameEvents.PlayerEvents.HealthChangedSignal>();
            Container.DeclareSignal<GameEvents.PlayerEvents.MaxHealthChangedSignal>();
            Container.DeclareSignal<GameEvents.PlayerEvents.PowerChangedSignal>();
            Container.DeclareSignal<GameEvents.PlayerEvents.MaxPowerChangedSignal>();
            Container.DeclareSignal<GameEvents.PlayerEvents.PlayerDiedSignal>();
            Container.DeclareSignal<GameEvents.EnemyEvents.EnemyDiedSignal>();
            Container.DeclareSignal<GameEvents.EnemyEvents.EnemySpawnedSignal>();
        }
    }
}