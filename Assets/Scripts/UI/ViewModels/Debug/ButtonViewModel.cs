using MvvmCross.Commands;
using UI.ViewModels.Buttons;

namespace OrbReaper.UI.ViewModels
{
    public abstract class ButtonViewModel : TitledViewModel, IButtonViewModel
    {
        protected ButtonViewModel()
        {
            ClickCommand = new MvxCommand(OnClick);
        }
        
        public IMvxCommand ClickCommand { get; }

        protected abstract void OnClick();
    }
}