using UnityEngine;
using Zenject;

namespace Characters.Enemy
{
    public class EnemyAIMoveController : ITickable
    {
        private readonly PlayerFacade playerFacade;
        private readonly GroundCheck groundCheck;
        private readonly UnityModel unityModel;
        private readonly GameSettings.EnemySettings enemySettings;

        public EnemyAIMoveController(
            PlayerFacade playerFacade, 
            GroundCheck groundCheck, 
            UnityModel unityModel, 
            GameSettings.EnemySettings enemySettings)
        {
            this.playerFacade = playerFacade;
            this.groundCheck = groundCheck;
            this.unityModel = unityModel;
            this.enemySettings = enemySettings;
        }

        private Transform EnemyTransform => unityModel.Transform;

        private Rigidbody EnemyRigidbody => unityModel.Rigidbody;

        private Vector3 PlayerLocation => playerFacade.UnityModel.Transform.position;

        private Vector3 LookDirection => PlayerLocation - EnemyTransform.position;
        
        public void Tick()
        {
            if (groundCheck == null || groundCheck.IsGrounded)
            {
                EnemyRigidbody.AddForce(LookDirection * enemySettings.MoveSpeed * Time.deltaTime, ForceMode.Acceleration);
            }
        }
    }
}