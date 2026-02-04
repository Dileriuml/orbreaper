using MvvmCross.ViewModels;

namespace UI.ViewModels.Buttons
{
    public interface ITitledViewModel : IMvxViewModel
    {
        string Title { get; }
    }
}