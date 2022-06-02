using Assets.Scripts.Model;
using Assets.Scripts.Model.Definition;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Creaters.Weapon
{
    public class BulletParticle : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private Rigidbody2D _rigidbody;
        private Transform _target;
        private Vector2 _point;

        protected virtual void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _target = GameObject.FindWithTag("Cursor").transform;
            _point = _rigidbody.GetPoint(_target.position).normalized;
            var agle = Mathf.Acos(_point.x) * 60;
            _rigidbody.rotation = _point.y > 0 ? agle : -agle;
        }

        protected void Start()
        {
            var session = FindObjectOfType<GameSession>();
            var sprite = GetComponent<SpriteRenderer>();
            sprite.sprite = DefsFacade.I.Bullets.Get(session.Data.Bullets.Used.Value).ImageBullet;
        }

        protected virtual void FixedUpdate()
        {
            var position = _rigidbody.position;
            position += _point * _speed;
            _rigidbody.MovePosition(position);
        }
    }
}