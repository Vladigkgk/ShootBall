using System.Collections;
using Assets.Scripts.Model;
using UnityEngine;

namespace Assets.Scripts.Components.LevelMeneger
{
    public class LoadNextLevelComponent : MonoBehaviour
    {
        [SerializeField] private string _sceneName;

        public void Exit()
        {
            var session = FindObjectOfType<GameSession>();
        }
    }
}