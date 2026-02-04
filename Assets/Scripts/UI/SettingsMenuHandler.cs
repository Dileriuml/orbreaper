using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Views;
using OrbReaper.Installers;
using UI.ViewModels.Buttons;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace OrbReaper.UI
{
    public class SettingsMenuHandler : IInitializable
    {
        private readonly UIInstaller.MenuSettings menuSettings;
        private readonly Dictionary<Type, Func<GameObject>> prefabMappings;
        private readonly List<IDebugMenuItem> buttons;
        
        public SettingsMenuHandler(UIInstaller.MenuSettings menuSettings, IDebugMenuItem[] debugItems)
        {
            this.menuSettings = menuSettings;

            buttons = debugItems.ToList();
            prefabMappings = new Dictionary<Type, Func<GameObject>>
            {
                [typeof(IButtonViewModel)] = () => this.menuSettings.ButtonPrefab,
                [typeof(IToggleViewModel)] = () => this.menuSettings.CheckboxPrefab,
            };
        }

        public void Initialize()
        {
            for (var i = 0; i < menuSettings.MenuGroup.transform.childCount; i++)
            {
                Object.Destroy(menuSettings.MenuGroup.transform.GetChild(i));
            }
            
            buttons.ForEach(bt =>
            {
                if (!TryGetViewPrefab(bt, out var menuItem))
                {
                    return;
                }

                if (menuItem.GetComponent(typeof(IMvxView)) is IMvxView mvxView)
                {
                    if (mvxView is MonoBehaviour mb)
                    {
                        mb.enabled = true;
                    }
                    
                    mvxView.DataContext = bt;
                }
            });
        }

        private bool TryGetViewPrefab(IDebugMenuItem fromItemType, out GameObject viewPrefab)
        {
            var intersect = prefabMappings.Keys.Intersect(fromItemType.GetType().GetInterfaces());
            if (intersect.FirstOrDefault() is { } keyType)
            {
                var parent = prefabMappings[keyType].Invoke();
                viewPrefab = Object.Instantiate(parent, menuSettings.MenuGroup.transform);
                viewPrefab.SetActive(true);
                return true;
            }

            viewPrefab = null;
            return false;
        }
    }
}