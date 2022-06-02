using System;
using System.Collections.Generic;
using Assets.Scripts.Model.Propertis;
using UnityEngine;

namespace Assets.Scripts.Model.Data
{
    [Serializable]
    public class ShopData
    {
        [SerializeField] private StringProperty _used = new StringProperty();
        [SerializeField] private List<string> _unlocked = new List<string>();

        public StringProperty Used => _used;

        public void AddSkin(string id)
        {
            if (!_unlocked.Contains(id))
                    _unlocked.Add(id);
        }

        public bool IsUnlocked(string id)
        {
            return _unlocked.Contains(id);
        }
    }
}