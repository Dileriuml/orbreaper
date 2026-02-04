using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace OrbReaper.Installers
{
    [CreateAssetMenu(fileName = "LevelConfiguration", menuName = "Installers/LevelConfiguration")]
    public class LevelConfiguration : ScriptableObjectInstaller<LevelConfiguration>
    {
        [SerializeField]
        private LevelSettings levelSettings;  
            
        public override void InstallBindings()
        {
            Container.BindInstance(levelSettings).AsSingle();
        }
        
        [Serializable]
        public class LevelSettings
        {
            public List<WaveInfo> waves;
            
            public float startDelaySeconds = 2f;
            public float spawnDelaySeconds = 0.3f;
            
            public float spawnRadius = 5f;
            public Vector3 spawnPositionCenter = new (75f, 100f, 113f);
        }
    }
}