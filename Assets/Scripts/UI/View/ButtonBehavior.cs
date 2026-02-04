using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Unity;
using OrbReaper.UI.ViewModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OrbReaper.UI.View
{
    public class ButtonBehavior : MvxMonoBehavior<ButtonBehavior, ButtonViewModel>
    {
        [SerializeField]
        private TMP_Text text;
        
        [SerializeField]
        private Button button;

        public void OnClick()
        {
            ViewModel.ClickCommand.Execute(this);
        }

        protected override void DoBinding(MvxFluentBindingDescriptionSet<MvxMonoBehavior<ButtonBehavior, ButtonViewModel>, ButtonViewModel> bindingDescriptionSet)
        {
            bindingDescriptionSet.Bind(text).To(v => v.Title);
            bindingDescriptionSet.Bind(button).To(v => v.ClickCommand);
        }
    }
}