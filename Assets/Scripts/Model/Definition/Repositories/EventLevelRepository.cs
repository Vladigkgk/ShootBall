using System;
using UnityEngine;

namespace Assets.Scripts.Model.Definition.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Repository/EventLevel", fileName = "EventLevel")]
    public class EventLevelRepository : DefRepository<EventDef>
    {
        
    }

    [Serializable]
    public class EventDef : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _iconLevel;
        [SerializeField] private int _needSeeAd;
        [SerializeField] private string _nameFirstLevel;
        [SerializeField] private string _nameLastLevel;
        [SerializeField] private string _idSkin;

        public string Id => _id;
        public Sprite IconLevel => _iconLevel;
        public int NeedSeeAd => _needSeeAd;
        public string NameFirstLevel => _nameFirstLevel;
        public string NameLastLevel => _nameLastLevel;
        public string IdSkin => _idSkin;
    }
}