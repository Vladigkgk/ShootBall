using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif

namespace Assets.Scripts.Creaters
{
    public class KillSlender : KillMonster
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] List<Vector3> _positionVector;

        private Vector3 _defaultPosition;

        public override void ChangeHealth(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Kill();
                return;
            }
            FindNextPosition();
            /*_defaultPosition = transform.position;
            transform.position = _nextPosition.position;
            _nextPosition.position = _defaultPosition;*/


        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            foreach (var vector in _positionVector)
            {
                Handles.DrawSolidDisc(transform.position + vector * 3, Vector3.forward, 0.1f);
            }
            
        }
#endif

        private void FindNextPosition()
        {
            foreach (var vector in _positionVector)
            {
                var hit = Physics2D.OverlapCircle(transform.position + vector * 3, 0.1f, _layer);
                if (!hit)
                {
                    transform.position += vector * 3;
                    return;
                }
                   
            }
        }
    }
}