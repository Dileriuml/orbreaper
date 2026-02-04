using System.Collections;
using System.Linq;
using OrbReaper.Installers;
using UnityEngine;

namespace WavesAndLevels
{
    public class LevelManager : ILevelManager
    {
        private readonly IEnemyRegistry enemyRegistry;
        private readonly IEnemySpawner enemySpawner;
        private readonly LevelConfiguration.LevelSettings levelSettings;

        public LevelManager(
            IEnemyRegistry enemyRegistry, 
            IEnemySpawner enemySpawner, 
            LevelConfiguration.LevelSettings levelSettings)
        {
            this.enemyRegistry = enemyRegistry;
            this.enemySpawner = enemySpawner;
            this.levelSettings = levelSettings;
        }
        
        public IEnumerator StartLevelCoroutine(int level)
        {
            if (levelSettings.waves.ElementAtOrDefault(level) is {} wave)
            {
                yield return enemySpawner.SpawnWaveCoroutine(wave);
            }
            
            yield return new WaitUntil(() => !enemyRegistry.IsAnyEnemyAlive);
        }
        
        public int LevelCount => levelSettings.waves.Count;
    }
}