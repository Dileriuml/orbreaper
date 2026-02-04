using Characters;
using UnityEngine;
using Zenject;

namespace OrbReaper.Player
{
    public class PlayerMoveHandler : IFixedTickable
    {
        private readonly PlayerInput playerInput; 
        private readonly PlayerFacade playerFacade;
        private readonly Camera mainCamera;
        
        public PlayerMoveHandler(
            PlayerFacade playerFacade,
            PlayerInput playerInput,
            Camera mainCamera)
        {
            this.playerInput = playerInput;
            this.playerFacade = playerFacade;
            this.mainCamera = mainCamera;
        }

        public void FixedTick()
        {
            var cameraTranform = mainCamera.transform;
            var moveVector = playerInput.Player.Move.ReadValue<Vector2>();
            var forceDirection = cameraTranform.forward * moveVector.y + cameraTranform.right * moveVector.x;
            forceDirection.y = 0;
            var forceToApply = forceDirection * (playerFacade.PlayerModel.Character.MovePower * Time.deltaTime);
            playerFacade.UnityModel.Rigidbody.AddForce(forceToApply, ForceMode.Impulse);
        }
    }
}