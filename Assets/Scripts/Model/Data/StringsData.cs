using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model.Data
{
    [Serializable]
    public class StringsData
    {
        [SerializeField] private List<string> _useds = new List<string>();

        public void Add(string id)
        {
            if (!_useds.Contains(id))
            {
                _useds.Add(id);
            }
        }

        public bool Contains(string id)
        {
            return _useds.Contains(id);
        }


    }
}