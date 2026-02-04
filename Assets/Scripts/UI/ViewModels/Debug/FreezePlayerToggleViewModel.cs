using Characters;
using OrbReaper.Utils;
using UI.ViewModels.Buttons;
using UnityEngine;

namespace OrbReaper.UI.ViewModels
{
    public class FreezePlayerToggleViewModel : BoolValueViewModel, IDebugMenuItem
    {
        private readonly PlayerFacade playerFacade;

        public FreezePlayerToggleViewModel(PlayerFacade playerFacade)
        {
            this.playerFacade = playerFacade;
        }
        
        public override string Title => "Freeze player";
        
        protected override void OnToggleChanged()
        {
            playerFacade.UnityModel.Rigidbody.constraints = !IsToggled ? RigidbodyConstraints.None : RigidbodyConstraints.FreezePosition;
        }
    }
}