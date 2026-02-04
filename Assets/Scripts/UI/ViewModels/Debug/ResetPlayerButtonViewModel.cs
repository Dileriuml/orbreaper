using Characters;
using OrbReaper.Utils;
using UI.ViewModels.Buttons;

namespace OrbReaper.UI.ViewModels
{
    public class ResetPlayerButtonViewModel : ButtonViewModel, IDebugMenuItem
    {
        private readonly PlayerFacade playerFacade;
        private readonly GameSettings.PlayerSettings playerSettings;
        
        public ResetPlayerButtonViewModel(PlayerFacade playerFacade, GameSettings.PlayerSettings playerSettings)
        {
            this.playerFacade = playerFacade;
            this.playerSettings = playerSettings;
        }
        
        public override string Title => "Reset Player";
        
        protected override void OnClick()
        {
            playerFacade.UnityModel.Rigidbody.ResetVelocity();
            playerFacade.UnityModel.Transform.position = playerSettings.DefaultLocation;
        }
    }
}