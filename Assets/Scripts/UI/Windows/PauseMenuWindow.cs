using System.Collections;
using Assets.Scripts.Creaters.Weapon;
using Assets.Scripts.Model;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Windows
{
    public class PauseMenuWindow : MonoBehaviour
    {
        private static readonly int HideKey = Animator.StringToHash("Hide");

        private Animator _animator;
        private float _localTime;

        private void Awake()
        {
            var input = FindObjectOfType<PlayerInput>();
            input.enabled = false;
            var final = FindObjectOfType<FinalLevelWindow>();
            var lose = FindObjectOfType<LoseLevelWindow>();
            if (final != null || lose != null) 
            {
                Destroy(gameObject);
            }
            _localTime = Time.timeScale;
            Time.timeScale = 0;
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Reload()
        {
            var session = FindObjectOfType<GameSession>();
            session.LoadLastLevel();

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        public void Countinue()
        {
            _animator.SetTrigger(HideKey);
        }

        public void Exit()
        {
            Application.Quit();
            
        }

        public void OnCloseAnimationClip()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            Time.timeScale = _localTime;
            var input = FindObjectOfType<PlayerInput>();
            input.enabled = true;
        }
    }
}