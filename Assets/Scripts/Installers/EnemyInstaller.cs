using Characters.Enemy;
using Zenject;

namespace OrbReaper.Installers
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        [Inject] 
        private GameSettings.EnemySettings enemySettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(enemySettings.GroundCheckConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<GroundCheck>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyDieController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyAIMoveController>().AsSingle();
        }
    }
}