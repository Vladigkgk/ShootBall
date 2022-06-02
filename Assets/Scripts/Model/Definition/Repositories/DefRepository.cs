using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model.Definition.Repositories
{
    public class DefRepository<TDetType> : ScriptableObject where TDetType : IHaveId
    {
        [SerializeField] protected TDetType[] _collection;

        public TDetType Get(string id)
        {
            if (string.IsNullOrEmpty(id)) return default;

            foreach(var itemDef in _collection)
            {
                if (itemDef.Id == id)
                {
                    return itemDef;
                }
            }

            return default;
        }

        public TDetType[] All => new List<TDetType>(_collection).ToArray();

    }
}