using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Model;
using UnityEngine.InputSystem;

namespace Assets.Scripts.UI.Windows
{
    public class MainMenuWindow : MonoBehaviour
    {
        [SerializeField] private Text _levelNumder;
        [SerializeField] private Text _coinsValue;
        [SerializeField] private GameObject _buttons;

        private Animator _animator;
        private GameSession _session;
        private static readonly int HideKey = Animator.StringToHash("Hide");

        private int _coins;

        private void Awake()
        {
            var input = FindObjectOfType<PlayerInput>();
            input.enabled = false;
            _animator = GetComponent<Animator>();
            _session = FindObjectOfType<GameSession>();
            _session.Data.Coins.OnChanged += OnChanged;
            var coins = _session.Data.Coins.Value;
            OnChanged(coins, coins);
        }

        private void OnChanged(int newValue, int oldValue)
        {
            _coins = newValue;
            _coinsValue.text = _coins.ToString();
        }

        private void Start()
        {
            var scene = SceneManager.GetActiveScene();
            var sceneIndex = scene.buildIndex;
            _levelNumder.text = sceneIndex.ToString();
            ChangeForEventLevel();
        }

        private void ChangeForEventLevel()
        {
            var sceneHud = SceneManager.GetSceneByName("HUD").buildIndex;
            var activeScene = SceneManager.GetActiveScene();
            if (activeScene.buildIndex >= sceneHud)
            {
                _buttons.SetActive(false);
            }
        }


        public void OnHideWindow()
        {
            _animator.SetBool(HideKey, true);
            var input = FindObjectOfType<PlayerInput>();
            input.enabled = true;
        }

        public void OnPauseMenuWindow()
        {
            var finalLevel = Resources.Load<GameObject>("UI/PauseMenu");
            var convas = GameObject.FindGameObjectWithTag("MainConvas");
            Instantiate(finalLevel, convas.transform);
        }

        public void OnShopWindow()
        {
            var shopWindow = Resources.Load<GameObject>("UI/ShopWindow");
            var convas = GameObject.FindGameObjectWithTag("MainConvas");
            Instantiate(shopWindow, convas.transform);
        }

        public void OnEventLevelWindow()
        {
            var eventLevelWindow = Resources.Load<GameObject>("UI/EventWindow");
            var convas = GameObject.FindGameObjectWithTag("MainConvas");
            Instantiate(eventLevelWindow, convas.transform);
        }

        private void OnDestroy()
        {
            _session.Data.Coins.OnChanged -= OnChanged;
        }

    }
}