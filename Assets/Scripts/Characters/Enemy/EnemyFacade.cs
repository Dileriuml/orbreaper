using Characters;
using OrbReaper.Signals;
using OrbReaper.Utils;
using Zenject;

namespace OrbReaper
{
    public class EnemyFacade : SpawnableBehaviour
    {
        private UnityModel unityModel;
        private SignalBus signalBus;
        private GroundCheck groundCheck;
        
        public UnityModel UnityModel => unityModel;
        
        [Inject]
        private void Construct(SignalBus signalBus, UnityModel unityModel)
        {
            this.signalBus = signalBus;
            this.unityModel = unityModel;
        }

        protected override void OnSpawn()
        {
            base.OnSpawn();
            unityModel.Rigidbody.ResetVelocity();
            signalBus.Fire(CreateSignalWithThis<GameEvents.EnemyEvents.EnemySpawnedSignal>());
        }
        
        public void Die()
        {
            Dispose();
            signalBus.Fire(CreateSignalWithThis<GameEvents.EnemyEvents.EnemyDiedSignal>());
        }

        private T CreateSignalWithThis<T>()
            where T : GameEvents.EnemyEvents.EnemySignal, new()
        {
            return new T
            {
                enemyFacade = this
            };
        }
    }
}