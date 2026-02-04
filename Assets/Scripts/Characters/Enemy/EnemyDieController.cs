using OrbReaper;
using Zenject;

namespace Characters.Enemy
{
    public class EnemyDieController : ITickable
    {
        private readonly EnemyFacade enemyFacade;
        private readonly GameSettings.EnemySettings enemySettings;

        public EnemyDieController(EnemyFacade enemyFacade, GameSettings.EnemySettings enemySettings)
        {
            this.enemyFacade = enemyFacade;
            this.enemySettings = enemySettings;
        }
        
        public void Tick()
        {
            if (enemyFacade.UnityModel.Transform.position.y < enemySettings.EnemyDeathTreshold
                && enemyFacade.isActiveAndEnabled)
            {
                Die();
            }
        }
        
        private void Die() => enemyFacade.Die();
    }
}