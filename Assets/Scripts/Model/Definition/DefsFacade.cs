using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model.Definition.Repositories;
using UnityEngine;

namespace Assets.Scripts.Model.Definition
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private SkinRepository _skins;
        [SerializeField] private BulletRepository _bullets;
        [SerializeField] private EventLevelRepository _event;

        public SkinRepository Skin => _skins;
        public BulletRepository Bullets => _bullets;
        public EventLevelRepository Event => _event;

        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}
