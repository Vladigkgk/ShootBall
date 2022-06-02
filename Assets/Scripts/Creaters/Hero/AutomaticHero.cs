using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Creaters.Hero
{
    public class AutomaticHero : PressHero
    {
        [SerializeField] private float _cooldownShoot;

        private IEnumerator QueueShoot()
        {
            while (enabled)
            {
                Shoot();
                yield return new WaitForSeconds(_cooldownShoot);
            }
            yield return null;
        }

        public override void StartTouch()
        {
            StartCoroutine(QueueShoot());
        }

        public override void EndTouch()
        {
            StopAllCoroutines();
        }
    }
}