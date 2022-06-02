using System;
using UnityEngine;

namespace Assets.Scripts.Model.Definition.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Repository/Bullets", fileName = "Bullets")]
    public class BulletRepository : DefRepository<BulletDef>
    {
        
    }

    [Serializable]
    public class BulletDef : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _imageBullet;
        [SerializeField] private int _price;

        public string Id => _id;
        public Sprite ImageBullet => _imageBullet;
        public int Price => _price;
    }
}