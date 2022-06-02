using System.Collections;
using Assets.Scripts.Ads;
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
        private InterstitialAd _ads;

        protected virtual void Awake()
        {
            if (FindObjectsOfType<FinalLevelWindow>().Length > 1)
                Destroy(gameObject);

            _session = FindObjectOfType<GameSession>();
            
            _levelCount.text = SetActiveLevel();
            SetEnemyAndCoins();
        }

        private void Start()
        {
            _ads = FindObjectOfType<InterstitialAd>();
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
            var testscene = SceneManager.GetActiveScene();
            if (!string.IsNullOrEmpty(_session.Data.LastEventLevel.Value))
            {
                var finalSceneEvent = SceneManager.GetSceneByName(_session.Data.LastEventLevel.Value);
                Debug.Log(finalSceneEvent.buildIndex);
                if (finalSceneEvent.buildIndex == testscene.buildIndex)
                {
                    _session.Data.CurrentLevel.Value = _session.Data.NormalLevel.Value;
                    _session.Save();
                    SceneManager.LoadSceneAsync(_session.Data.NormalLevel.Value);
                    return;

                }
            }
            var index = testscene.buildIndex + 1;
            var scene = SceneManager.GetSceneByBuildIndex(index);
            if (scene.name == "Hud")
            {
                index = 1;
            }
            _session.Data.CurrentLevel.Value = index;
            _session.Save();
            if (index % 7 == 0)
            {
                _ads.SetIndex(index);
                _ads.LoadAd();
                return;
            }
            SceneManager.LoadSceneAsync(index);
        }
    }
}