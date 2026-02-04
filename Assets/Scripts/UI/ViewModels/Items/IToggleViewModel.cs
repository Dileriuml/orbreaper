namespace UI.ViewModels.Buttons
{
    public interface IToggleViewModel : ITitledViewModel
    {
        bool IsToggled { get; set; }
    }
}