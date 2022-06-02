using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class RotateComponent : MonoBehaviour
    {
        [SerializeField] private int _updateRotation;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            _rigidbody.rotation += _updateRotation;
        }
    }
}