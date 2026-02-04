using System;
using OrbReaper.UI;
using OrbReaper.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace OrbReaper.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] 
        private Settings settings = null;

        [SerializeField]
        private MenuSettings menuSettings;
        
        public override void InstallBindings()
        {
            Container.BindInstance(settings.ViewContainer).AsSingle();
            Container.BindInstance(menuSettings).AsSingle();
            
            Container.BindInterfacesAndSelfTo<UIHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<SettingsMenuHandler>().AsSingle();
            RegisterUIDebug();
        }

        private static Type[] DebugMenuItems => new[]
        {
            typeof(ResetPlayerButtonViewModel),
            typeof(FreezePlayerToggleViewModel),
            typeof(PlayerTakeDamageButtonViewModel),
            typeof(SunTriggerButtonViewModel)
        }; 
        
        private void RegisterUIDebug()
        {
            Array.ForEach(DebugMenuItems, t => Container.BindInterfacesAndSelfTo(t).AsSingle());
        }

        [Serializable]
        public class Settings
        {
            public ViewContainer ViewContainer; 
        }
        
        [Serializable]
        public class MenuSettings
        {
            public LayoutGroup MenuGroup;
            public GameObject ButtonPrefab;
            public GameObject CheckboxPrefab;
        }
    }
}