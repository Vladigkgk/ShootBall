using System;
using System.Collections;
using Assets.Scripts.Components.GoBased;
using Assets.Scripts.Components.LevelMeneger;
using UnityEngine;

namespace Assets.Scripts.Creaters
{
    public class KillMonster : MonoBehaviour
    {
        [SerializeField] protected int _health = 1;
        [SerializeField] private SpawnComponent _hatSpawn;
        [SerializeField] private GameObject _hat;
        [SerializeField] protected SpawnListComponent _spawners;
        [SerializeField] private SpawnComponent _bombSpawn;

        public Action OnKill;
        private bool _isDead = false;

        public bool IsDead => _isDead;

        public virtual void ChangeHealth(int damage)
        {
            _health -= damage;
            if (_health > 0)
            {
                _hatSpawn.Spawn();
                Destroy(_hat);
            }
            if (_health <= 0)
            {
                Kill();
            }
        }

        protected void Kill()
        {
            _spawners?.SpawnAll();
            _bombSpawn?.Spawn();

            _isDead = true;
            OnKill?.Invoke();
            gameObject.SetActive(false);
        }
    }
}