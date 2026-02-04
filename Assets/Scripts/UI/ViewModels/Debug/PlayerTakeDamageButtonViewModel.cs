using Characters;
using UI.ViewModels.Buttons;
using Random = UnityEngine.Random;

namespace OrbReaper.UI.ViewModels
{
    public class PlayerTakeDamageButtonViewModel : ButtonViewModel, IDebugMenuItem
    {
        private readonly PlayerFacade playerFacade;
        
        public PlayerTakeDamageButtonViewModel(PlayerFacade playerFacade)
        {
            this.playerFacade = playerFacade;
        }
        
        public override string Title => "Random hp";
        
        protected override void OnClick()
        {
            var damage = Random.Range(-1f, 1f) * 200f;
            if (damage > 0)
            {
                playerFacade.HealthController.GetDamage(damage);
            }
            else
            {
                playerFacade.HealthController.ReceiveHealing(-damage);   
            }
        }
    }
}