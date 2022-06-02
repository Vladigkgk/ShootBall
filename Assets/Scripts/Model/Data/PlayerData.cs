using System;
using Assets.Scripts.Model.Propertis;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Model.Data
{
    [Serializable]
    public class PlayerData
    {
        public IntProperty CurrentLevel = new IntProperty();
        public IntProperty NormalLevel = new IntProperty();
        public IntProperty ShowAds = new IntProperty();
        public IntProperty Coins = new IntProperty();
        public StringProperty LastEventLevel = new StringProperty();
        public ShopData Skins = new ShopData();
        public ShopData Bullets = new ShopData();
        public StringsData UsedEvents = new StringsData();

        public bool IsEnought(int count)
        {
            return count <= Coins.Value;
        }
        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}