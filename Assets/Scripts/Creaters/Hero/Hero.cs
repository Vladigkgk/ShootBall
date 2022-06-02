using System.Collections;
using Assets.Scripts.Components.ColliderBased;
using Assets.Scripts.Components.GoBased;
using Assets.Scripts.Utils;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Creaters.Hero
{
    public class Hero : MonoBehaviour
    {
        [SerializeField] private float _force = 1;
        [SerializeField] private float _downVelocity;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private SpawnListComponent _particles;
        [SerializeField] private SpawnListComponent _trash;
        [SerializeField] private GameObject _dawnParticle;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private ParticleSystem _fireParticle;

        private Rigidbody2D _rigibody;
        private Vector2 _direction;
        protected UnityArmatureComponent _armature;

        private void Start()
        {
            _armature = GetComponent<UnityArmatureComponent>();
            _armature.animation.Play("Stay");
            _rigibody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _rigibody.freezeRotation = !_groundCheck.IsTouchingLayer;
            if (_armature.animation.isCompleted)
            {
                _armature.animation.Play("Stay");
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_layer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.x >= _downVelocity || contact.relativeVelocity.y >= _downVelocity)
                {
                    var position = contact.point;
                    Instantiate(_dawnParticle, position, Quaternion.identity);
                }
            }
        }

        public void Shoot()
        {
            _armature.animation.Play("Shoot", 1);
            FireSpawn();
            _direction = NewUpdateDirection();
            _rigibody.AddForce(new Vector2(-_direction.x * _force, -_direction.y * _force), ForceMode2D.Impulse);
        }

        private void FireSpawn()
        {
            _particles.SpawnAll();
            _fireParticle?.gameObject.SetActive(true);

        }

        private Vector2 NewUpdateDirection()
        {
            var rotation = _rigibody.rotation;
            return new Vector2(Mathf.Cos(rotation * Mathf.Deg2Rad), Mathf.Sin(rotation * Mathf.Deg2Rad));
        }

        public void Die()
        {
            _trash.SpawnAll();
            var loseWindow = Resources.Load<GameObject>("UI/LoseLevel");
            var convas = GameObject.FindGameObjectWithTag("MainConvas");
            Instantiate(loseWindow, convas.transform);
            Destroy(gameObject);
        }
    }
}