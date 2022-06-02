using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Components.GoBased
{
    public class SpawnComponent : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private GameObject _prefab;

        public void Spawn()
        {
            Instantiate(_prefab, _target.position, Quaternion.identity);
        }

        public void SpawnPosition(Vector3 position)
        {
            Instantiate(_prefab, position, Quaternion.identity);
        }

        public GameObject InstanseSpawn()
        {
            var instanse = Instantiate(_prefab, _target.position, Quaternion.identity);
            instanse.transform.SetParent(transform.parent);

            return instanse;
        }  
    }
}