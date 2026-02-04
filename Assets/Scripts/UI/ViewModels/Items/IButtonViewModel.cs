using MvvmCross.Commands;

namespace UI.ViewModels.Buttons
{
    public interface IButtonViewModel : ITitledViewModel
    {
        IMvxCommand ClickCommand { get; }
    }
}