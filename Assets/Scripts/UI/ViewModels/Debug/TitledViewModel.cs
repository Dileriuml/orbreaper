using MvvmCross.ViewModels;
using UI.ViewModels.Buttons;

namespace OrbReaper.UI.ViewModels
{
    public abstract class TitledViewModel : MvxViewModel, ITitledViewModel
    {
        public abstract string Title { get; }
    }
}