using OrbReaper;
using OrbReaper.Player;
using OrbReaper.Utils;
using Player;
using UnityEngine;
using Zenject;

namespace Characters
{
    public class PlayerFacade : MonoBehaviour
    {
        public PlayerModel PlayerModel { get; private set; }

        public UnityModel UnityModel { get; private set; }

        public PlayerHealthController HealthController { get; private set; }
        
        public GameSettings.PlayerSettings PlayerSettings { get; private set; }

        [Inject]
        private void Construct(
            PlayerModel playerModel, 
            UnityModel unityModel,
            PlayerHealthController playerHealthController,
            GameSettings.PlayerSettings playerSettings)
        {
            UnityModel = unityModel;
            PlayerModel = playerModel;
            HealthController = playerHealthController;
            PlayerSettings = playerSettings;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<EnemyFacade>(out var enemyFacade))
            {
                HealthController.GetDamage(PlayerSettings.CollisionDamageMultiplier * collision.GetKineticEnergy());
            }
        }
    }
}