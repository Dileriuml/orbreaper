using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace OrbReaper.FocalCamera
{
    public class FocalCameraZoomController : ITickable, IInitializable
    {
        private readonly PlayerInput playerInput;
        private readonly GameSettings.CameraSettings cameraSettings;
        private readonly Camera camera;

        private float targetAngle;
        private float targetHeight;
        private int zoomItemIndex;
        
        public FocalCameraZoomController(PlayerInput playerInput, GameSettings.CameraSettings cameraSettings, Camera camera)
        {
            this.playerInput = playerInput;
            this.cameraSettings = cameraSettings;
            this.camera = camera;
        }
        
        private int ZoomItemIndex
        {
            get => zoomItemIndex;
            set
            {
                if (!IsZoomIndexExists(value))
                {
                    return;
                }

                zoomItemIndex = value;
                
                UpdateAngleAndHeight();
            }
        }

        private void UpdateAngleAndHeight()
        {
            targetAngle = cameraSettings.ZoomItems[zoomItemIndex].IsFirstPerson
                ? cameraSettings.ZoomItems[zoomItemIndex].CustomRotation
                : cameraSettings.DefaultCameraRotation;
            targetHeight = cameraSettings.ZoomItems[zoomItemIndex].IsFirstPerson
                ? cameraSettings.FirstPersonCameraHeight
                : cameraSettings.DefaultCameraHeight;
        }

        public void Tick()
        {
            var targetZoom = cameraSettings.ZoomItems[zoomItemIndex].Value;
            var rotationX = Mathf.MoveTowards(camera.transform.localEulerAngles.x, targetAngle, cameraSettings.ZoomCameraRotateSpeed * Time.deltaTime);
            var pos = camera.transform.localPosition;
            pos.y = Mathf.MoveTowards(
                pos.y, 
                targetHeight, 
                cameraSettings.ZoomSpeed * Time.deltaTime);
            camera.transform.localPosition = pos;
            camera.transform.localEulerAngles = Vector3.right * rotationX;
            camera.fieldOfView = Mathf.MoveTowards(camera.fieldOfView, targetZoom, cameraSettings.ZoomSpeed * Time.deltaTime);
        }

        public void Initialize()
        {
            ZoomItemIndex = cameraSettings.DefaultZoomItem;
            playerInput.Player.ZoomControls.performed += ZoomChanged;
        }

        private bool IsZoomIndexExists(int newIndex) => newIndex >= 0 && newIndex < cameraSettings.ZoomItems.Length;

        private void ZoomChanged(InputAction.CallbackContext obj)
        {
            var valueChange = obj.ReadValue<Vector2>();

            if (valueChange.y > 0f)
            {
                ZoomItemIndex--;
            }
            else
            {
                ZoomItemIndex++;
            }
        }
    }
}