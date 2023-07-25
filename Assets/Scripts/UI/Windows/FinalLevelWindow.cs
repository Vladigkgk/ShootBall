using System.Collections;
using Assets.Scripts.Components.LevelMeneger;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Windows
{
    public class FinalLevelWindow : MonoBehaviour
    {
        [SerializeField] protected Text _levelCount;
        [SerializeField] protected Text _enemyCount;
        [SerializeField] protected Text _coinsCount;

        private GameSession _session;

        protected virtual void Awake()
        {
            if (FindObjectsOfType<FinalLevelWindow>().Length > 1)
                Destroy(gameObject);

            _session = FindObjectOfType<GameSession>();
            
            _levelCount.text = SetActiveLevel();
            SetEnemyAndCoins();
        }

        protected virtual void SetEnemyAndCoins()
        {
            var levelProgress = FindObjectOfType<LevelProgress>();
            var enemy = levelProgress.CountKill;
            _enemyCount.text = enemy.ToString();
            var coins = enemy * 10;
            _session.Data.Coins.Value += coins;
            _coinsCount.text = coins.ToString();


        }

        protected string SetActiveLevel()
        {
            var scene = SceneManager.GetActiveScene();
            var sceneIndex = scene.buildIndex;
            return sceneIndex.ToString();
        }

        public void LoadNextLevel()
        {
            var scene = SceneManager.GetActiveScene();
            Debug.Log(scene.buildIndex + 1);
            var nextScene = SceneManager.GetSceneByBuildIndex(scene.buildIndex + 1);
            var index = scene.buildIndex + 1;
            Debug.Log(index);
            if (nextScene.name == "Hud")
            {
                index = 1;
            }
            _session.Data.CurrentLevel.Value = index;
            _session.Save();
            SceneManager.LoadScene(index);
        }
    }
}