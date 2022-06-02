using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Creaters.Weapon
{
    public class BulletTimeParticle : BulletParticle
    {
        [SerializeField] private float _time;

        private Collider2D _collider;
        private float _startTime;

        protected override void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _startTime = Time.time;
            base.Awake();
        }

        protected override void FixedUpdate()
        {
            if (_startTime - Time.time > _time)
                _collider.enabled = true;
            base.FixedUpdate();
        }
    }
}