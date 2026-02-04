using System.Collections;
using OrbReaper.Installers;
using UnityEngine;
using WavesAndLevels;

namespace OrbReaper.WavesAndLevels
{
    public class EnemySpawner : IEnemySpawner
    {
        private readonly LevelConfiguration.LevelSettings levelSettings;
        private readonly EnemyFactory enemyFactory;

        public EnemySpawner(
            LevelConfiguration.LevelSettings levelSettings,
            EnemyFactory enemyFactory)
        {
            this.levelSettings = levelSettings;
            this.enemyFactory = enemyFactory;
        }

        public IEnumerator SpawnWaveCoroutine(WaveInfo wave)
        {
            foreach (var spawnEntry in wave.SpawnEntries)
            {
                for (var i = 0; i < spawnEntry.Count - 1; i++)
                {
                    SpawnEnemy();
                    
                    yield return new WaitForSeconds(levelSettings.spawnDelaySeconds);
                }
                
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            var spawnedEnemy = enemyFactory.Create();
            spawnedEnemy.transform.position = GetRandomPosition();
        }

        private Vector3 GetRandomPosition()
        {
            var generatedPosition = Random.insideUnitCircle * levelSettings.spawnRadius;
            return new Vector3(generatedPosition.x, 0, generatedPosition.y) + levelSettings.spawnPositionCenter;
        }
    }
}