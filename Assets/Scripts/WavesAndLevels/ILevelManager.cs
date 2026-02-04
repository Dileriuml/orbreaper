using System.Collections;

namespace WavesAndLevels
{
    public interface ILevelManager
    {
        IEnumerator StartLevelCoroutine(int level);

        int LevelCount { get; }
    }
}