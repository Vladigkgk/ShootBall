using System;
using UnityEngine;

namespace Assets.Scripts.Model.Definition.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Repository/Skins", fileName = "Skins")]
    public class SkinRepository : DefRepository<SkinDef>
    {
    }

    [Serializable]
    public struct SkinDef : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _skinPreview;
        [SerializeField] private GameObject _skin;
        [SerializeField] private int _price;

        public string Id => _id;
        public Sprite SkinPreview => _skinPreview;
        public GameObject Skin => _skin;
        public int Price => _price;

    }
}