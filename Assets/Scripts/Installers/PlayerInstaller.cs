using System;
using OrbReaper.Player;
using Player;
using UnityEngine;
using Zenject;

namespace OrbReaper.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField]
        private Settings settings = null;

        public override void InstallBindings()
        {
            Container.BindInstance(settings.LivingBeingModel).AsSingle();
            Container.BindInstance(settings.CharacterModel).AsSingle();
            
            Container.Bind<PlayerHealthController>().AsSingle();
            Container.Bind<PlayerModel>().AsSingle();
            
            Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            //Container.BindInterfacesTo<PlayerMoveHandler>().AsSingle();
            //Container.BindInterfacesAndSelfTo<PlayerDamageHandler>().AsSingle();
            //Container.BindInterfacesTo<PlayerDirectionHandler>().AsSingle();
            //Container.BindInterfacesTo<PlayerShootHandler>().AsSingle();

            //Container.Bind<PlayerInputState>().AsSingle();

            //Container.BindInterfacesTo<PlayerHealthWatcher>().AsSingle();
        }

        [Serializable]
        public class Settings
        {
            public LivingBeingModel LivingBeingModel;
            public CharacterModel CharacterModel;
        }
    }
}