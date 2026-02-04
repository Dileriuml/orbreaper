using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Unity;
using MvvmCross.Platforms.Unity.TargetBinding;
using OrbReaper.UI.ViewModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OrbReaper.UI.View
{
    public class ToggleBehavior : MvxMonoBehavior<ToggleBehavior, BoolValueViewModel>
    {
        [SerializeField] 
        private Toggle toggle;

        [SerializeField]
        private TMP_Text text;
        
        protected override void DoBinding(MvxFluentBindingDescriptionSet<MvxMonoBehavior<ToggleBehavior, BoolValueViewModel>, BoolValueViewModel> bindingDescriptionSet)
        {
            bindingDescriptionSet.Bind(toggle).To(vm => vm.IsToggled);
            bindingDescriptionSet.Bind(text).To(vm => vm.Title);
        }
    }
}