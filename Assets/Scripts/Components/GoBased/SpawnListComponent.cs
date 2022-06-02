using System;
using UnityEngine;

namespace Assets.Scripts.Components.GoBased
{
    public class SpawnListComponent : MonoBehaviour
    {
        [SerializeField] private SpawnData[] _spawners;

        public void SpawnId(string id)
        {
            foreach (var spawner in _spawners)
            {
                if (spawner.Id == id)
                {
                    spawner.Spawner.Spawn();
                }
            }
        }

        public void SpawnAll()
        {
            foreach (var spawner in _spawners)
            {
                spawner.Spawner.Spawn();
            }
        }
    }
    [Serializable]
    public class SpawnData
    {
        [SerializeField] private string _id;
        [SerializeField] private SpawnComponent _spawner;

        public string Id => _id;
        public SpawnComponent Spawner => _spawner;
    }
}