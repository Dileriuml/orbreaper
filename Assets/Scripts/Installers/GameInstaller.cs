using System;
using MvvmCross.Platforms.Unity;
using OrbReaper.FocalCamera;
using OrbReaper.Game;
using OrbReaper.WavesAndLevels;
using UnityEngine;
using WavesAndLevels;
using Zenject;

namespace OrbReaper.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public const string EnemiesObjectName = "Enemies";
        
        [SerializeField]
        private EnemySettings enemySettings;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MvxUnitySetup>().AsSingle();
            
            Container.BindFactory<EnemyFacade, EnemyFactory>()
                // We could just use FromMonoPoolableMemoryPool here instead, but
                // for IL2CPP to work we need our pool class to be used explicitly here
                .FromPoolableMemoryPool<EnemyFacade, EnemyPool>(poolBinder => poolBinder
                    // Spawn 5 enemies right off the bat so that we don't incur spikes at runtime
                    .WithInitialSize(20)
                    .FromSubContainerResolve()
                    .ByNewPrefabInstaller<EnemyInstaller>(enemySettings.EnemyPrefab)
                    // Place each enemy under an Enemies game object at the root of scene hierarchy
                    .UnderTransformGroup(EnemiesObjectName));
            
            Container.BindInterfacesTo<EnemyRegistry>().AsSingle();
            Container.BindInterfacesTo<EnemySpawner>().AsSingle();
            
            Container.BindInterfacesTo<LevelManager>().AsSingle();
            Container.BindInterfacesTo<GameState>().AsSingle();
            
            Container.BindInterfacesTo<FocalCameraController>().AsSingle();
            Container.BindInterfacesTo<FocalCameraZoomController>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerInput>()
                     .AsSingle()
                     .OnInstantiated<PlayerInput>((c, obj) => obj.Player.Enable());
            
            Container.BindInstance(enemySettings).AsSingle();

            GameSignalsInstaller.Install(Container);
        }
        
        [Serializable]
        public class EnemySettings
        {
            public GameObject EnemyPrefab;
        }
        
        public class EnemyPool : MonoPoolableMemoryPool<IMemoryPool, EnemyFacade>
        {
        }
    }
}