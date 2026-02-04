using OrbReaper.Lighting;
using UI.ViewModels.Buttons;

namespace OrbReaper.UI.ViewModels
{
    public class SunTriggerButtonViewModel : ButtonViewModel, IDebugMenuItem
    {
        private readonly ISunController sunController;
        
        public SunTriggerButtonViewModel(ISunController sunController)
        {
            this.sunController = sunController;
        }

        public override string Title => "Sun Trigger";
        
        protected override void OnClick() => sunController.Trigger();
    }
}