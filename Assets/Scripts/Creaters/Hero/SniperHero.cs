using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Creaters.Hero
{
    public class SniperHero : PressHero
    {
        [SerializeField] private GameObject _line;
        [SerializeField] private float _modifyTime;
        private float _defaultTime;
        private bool _slowmo;

        public override void StartTouch()
        {
            _slowmo = true;
            _defaultTime = Time.timeScale;
            _armature.animation.Play("Shoot");
            _armature.animation.Stop();
        }

        private void Update()
        {
            if (_slowmo)
            {
                _line.SetActive(true);
                Time.timeScale = _modifyTime;
            }

        }

        public override void EndTouch()
        {
            _slowmo = false;
            _line.SetActive(false);
            Time.timeScale = _defaultTime;
            Shoot();
        }
    }
}