using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model.Data;
using Assets.Scripts.Model.Definition;
using Assets.Scripts.Model.Propertis;
using UnityEngine;

namespace Assets.Scripts.Model.Models
{
    public class SkinModel
    {
        private PlayerData _data;

        public event Action OnChanged;
        public event Action OnSkinUpdate;

        public string Used => _data.Skins.Used.Value;

        public SkinModel(PlayerData data)
        {
            _data = data;
        }

        public void Unlock(string skinId)
        {
            var def = DefsFacade.I.Skin.Get(skinId);
            var isEnoghtResourses = _data.IsEnought(def.Price);
            if (isEnoghtResourses)
            {
                var coins = _data.Coins.Value - def.Price;
                _data.Coins.Value = coins;
                _data.Skins.AddSkin(skinId);
                OnChanged?.Invoke();
            }
        }

        public void SelectSkin(string skinId)
        {
            _data.Skins.Used.Value = skinId;
            OnChanged?.Invoke();
            OnSkinUpdate?.Invoke();
        }

        public bool IsUsed(string skinId)
        {
            return _data.Skins.Used.Value == skinId;
        }

        public bool IsUnlocked(string skinId)
        {
            return _data.Skins.IsUnlocked(skinId);
        }

        public bool CanBuy(string skinId)
        {
            var skinDef = DefsFacade.I.Skin.Get(skinId);
            return _data.IsEnought(skinDef.Price);
        }
    }
}
