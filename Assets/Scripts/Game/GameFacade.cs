using System.Collections;
using UnityEngine;
using WavesAndLevels;
using Zenject;

namespace OrbReaper.Game
{
    public class GameFacade : MonoBehaviour
    {
        [Inject] 
        private IGameState gameState;

        [Inject] 
        private ILevelManager levelManager;

        private void Start()
        {
            gameState.CurrentLevel = -1;
            
            // Update when menu implemented
            StartCoroutine( StartGame());
        }

        public IEnumerator StartGame()
        {
            gameState.CurrentLevel = 0;
            yield return StartLevelsCycle();
        }

        public void SpawnPlayer()
        {
            // TODO
        }

        public void ExitGame()
        {
            // TODO save state?
            
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
        
        private IEnumerator StartLevelsCycle()
        {
            for (gameState.CurrentLevel = 0; gameState.CurrentLevel < levelManager.LevelCount; gameState.CurrentLevel++)
            {
                yield return levelManager.StartLevelCoroutine(gameState.CurrentLevel);
            }
        }
    }
}