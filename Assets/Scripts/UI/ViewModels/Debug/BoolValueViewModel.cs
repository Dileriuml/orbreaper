using UI.ViewModels.Buttons;

namespace OrbReaper.UI.ViewModels
{
    public abstract class BoolValueViewModel : TitledViewModel, IToggleViewModel
    {
        private bool isToggled;

        public bool IsToggled
        {
            get => isToggled;
            set => SetProperty(ref isToggled, value, OnToggleChanged); 
        }

        protected abstract void OnToggleChanged();
    }
}