using System.Collections.Generic;
using System.Linq;
using OrbReaper;
using OrbReaper.Signals;
using OrbReaper.Utils;
using Zenject;

namespace WavesAndLevels
{
    public class EnemyRegistry : SignalSubscriber, IEnemyRegistry
    {
        private List<EnemyFacade> enemies = new ();

        public EnemyRegistry(SignalBus signalBus) : base(signalBus)
        {
        }
        
        private void OnSpawned(GameEvents.EnemyEvents.EnemySpawnedSignal enemySpawnedSignal)
        {
            enemies.Add(enemySpawnedSignal.enemyFacade);
        }
        
        private void OnDespawnedSignal(GameEvents.EnemyEvents.EnemyDiedSignal enemyDiedSignal)
        {
            enemies.Remove(enemyDiedSignal.enemyFacade);
        }

        protected override void OnSubscribeSignals()
        {
            Subscribe<GameEvents.EnemyEvents.EnemySpawnedSignal>(OnSpawned);
            Subscribe<GameEvents.EnemyEvents.EnemyDiedSignal>(OnDespawnedSignal);
        }

        public bool IsAnyEnemyAlive => enemies.Any();
    }
}