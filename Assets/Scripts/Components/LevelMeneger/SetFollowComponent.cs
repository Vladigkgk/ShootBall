using System.Collections;
using UnityEngine;
using Cinemachine;
using Assets.Scripts.Creaters.Hero;

namespace Assets.Scripts.Components.LevelMeneger
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class SetFollowComponent : MonoBehaviour
    {
        private void Start()
        {
            SetFollow();
        }

        public void SetFollow()
        {
            var camera = GetComponent<CinemachineVirtualCamera>();
            var hero = FindObjectOfType<Hero>();
            camera.Follow = hero.transform;
        }
    }
}