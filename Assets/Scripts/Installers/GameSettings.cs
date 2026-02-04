using System;
using UnityEngine;
using Zenject;

//[CreateAssetMenu(fileName = "GameSettings", menuName = "Installers/GameSettings")]
public class GameSettings : ScriptableObjectInstaller<GameSettings>
{
    [SerializeField]
    private EnemySettings enemyConfiguration;
    
    [SerializeField]
    private CameraSettings cameraSettings;

    [SerializeField]
    private PlayerSettings playerSettings;
    
    public override void InstallBindings()
    {
        Container.BindInstance(enemyConfiguration).AsSingle();
        Container.BindInstance(cameraSettings).AsSingle();
        Container.BindInstance(playerSettings).AsSingle();
    }
    
    [Serializable]
    public class PlayerSettings
    {
        public float CollisionDamageMultiplier = 0.03f;
        
        public Vector3 DefaultLocation = Vector3.zero;
    }
    
    [Serializable]
    public class EnemySettings
    {
        public float EnemyDeathTreshold = 75f;
        public float MoveSpeed = 40f;
        public GroundCheck.Settings GroundCheckConfig;
    }
    
    [Serializable]
    public class CameraSettings
    {
        [Range(0.1f, 1f)]
        public float KeyboardSensetivity = 0.5f;
        
        [Range(0.1f, 2f)]
        public float MouseSensetivity = 1f;
        
        [Range(0.1f, 100f)]
        public float RotationDecaySpeed = 50f;
        
        [Range(0.1f, 0.5f)]
        public float LateDecayFrom = 0.5f;
        
        [Range(0.1f, 1000f)]
        public float SmoothSpeed = 100f;

        [Min(1)]
        public float ZoomSpeed = 100;
        
        public float ZoomCameraRotateSpeed = 200;

        public float DefaultCameraRotation = 52f;

        public float FirstPersonCameraHeight = -2.5f;
        
        public float DefaultCameraHeight = 8.5f;
        
        [Min(0)]
        public int DefaultZoomItem;
        
        public ZoomModeItem[] ZoomItems;

        public Vector3 OffsetFromPlayer = Vector3.zero;
    }
    
    [Serializable]
    public class ZoomModeItem
    {
        public string Name;
        public float Value;
        public bool IsFirstPerson = false;
        public float CustomRotation;
    }
}