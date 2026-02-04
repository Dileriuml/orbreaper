using Characters;
using UnityEngine;
using Zenject;

namespace OrbReaper.FocalCamera
{
    public class FocalCameraController : ILateTickable, ITickable
    {
        private readonly PlayerFacade playerFacade;
        private readonly PlayerInput playerInput;
        private readonly GameSettings.CameraSettings cameraSettings;
        private readonly FocalCamera focalCamera;

        private float rotationDistance;
        
        public FocalCameraController(
            PlayerFacade playerFacade,
            PlayerInput playerInput,
            GameSettings.CameraSettings cameraSettings,
            FocalCamera focalCamera)
        {
            this.playerFacade = playerFacade;
            this.playerInput = playerInput;
            this.cameraSettings = cameraSettings;
            this.focalCamera = focalCamera;
        }

        public void Tick()
        {
            HandleInput();

            if (Mathf.Abs(rotationDistance) <= Mathf.Epsilon)
            {
                return;
            }
            
            // Calculate the mouse movement since the last frame
            focalCamera.transform.Rotate(Vector3.up, cameraSettings.SmoothSpeed * rotationDistance);
        }

        private void HandleInput()
        {
            if (playerInput.Player.RotateAnalog is { triggered : true } ra)
            {
                rotationDistance = cameraSettings.MouseSensetivity * ra.ReadValue<Vector2>().x;
            }
            else if (playerInput.Player.RotateButtons is { inProgress : true } rb)
            {
                rotationDistance = cameraSettings.KeyboardSensetivity * rb.ReadValue<float>();
            }
            else
            {
                if (Mathf.Abs(rotationDistance) > cameraSettings.LateDecayFrom)
                {
                    rotationDistance = Mathf.Max(-cameraSettings.LateDecayFrom, Mathf.Min(cameraSettings.LateDecayFrom, rotationDistance));
                }

                // Note: This one here exists as the decay, so it smooths whenever user releases rotation
                rotationDistance = Mathf.MoveTowards(rotationDistance, 0f, cameraSettings.RotationDecaySpeed * Time.deltaTime);
            }
        }

        // Update is called once per frame
        public void LateTick()
        {
            // Get the current mouse position
            focalCamera.transform.position = playerFacade.UnityModel.Transform.position + cameraSettings.OffsetFromPlayer;
        }
    }
}