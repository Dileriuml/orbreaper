using System.Collections;
using OrbReaper;

namespace WavesAndLevels
{
    public interface IEnemySpawner
    {
        IEnumerator SpawnWaveCoroutine(WaveInfo wave);
    }
}