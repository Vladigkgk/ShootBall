using Assets.Scripts.Model.Data;
using Assets.Scripts.Model.Definition;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Model.Models
{
    public class BullletModel
    {
        private PlayerData _data;

        public event Action OnChanged;
        public event Action OnSkinUpdate;

        public string Used => _data.Bullets.Used.Value;

        public BullletModel(PlayerData data)
        {
            _data = data;
        }

        public void Unlock(string skinId)
        {
            var def = DefsFacade.I.Bullets.Get(skinId);
            var isEnoghtResourses = _data.IsEnought(def.Price);
            if (isEnoghtResourses)
            {
                var coins = _data.Coins.Value - def.Price;
                _data.Coins.Value = coins;
                _data.Bullets.AddSkin(skinId);
                OnChanged?.Invoke();
            }
        }

        public void SelectSkin(string skinId)
        {
            _data.Bullets.Used.Value = skinId;
            OnChanged?.Invoke();
            OnSkinUpdate?.Invoke();
        }

        public bool IsUsed(string skinId)
        {
            return _data.Bullets.Used.Value == skinId;
        }

        public bool IsUnlocked(string skinId)
        {
            return _data.Bullets.IsUnlocked(skinId);
        }

        public bool CanBuy(string skinId)
        {
            var skinDef = DefsFacade.I.Bullets.Get(skinId);
            return _data.IsEnought(skinDef.Price);
        }
    }
}