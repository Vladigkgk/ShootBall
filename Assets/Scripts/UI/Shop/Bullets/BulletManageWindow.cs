using Assets.Scripts.Model;
using Assets.Scripts.Model.Definition;
using Assets.Scripts.Model.Definition.Repositories;
using Assets.Scripts.UI.DataGroup;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI.Shop.Bullets
{
    public class BulletManageWindow : MonoBehaviour
    {
        [SerializeField] private Transform _bulletContainer;

        private PredefinedDataGroup<BulletDef, BulletWidget> _dataGroup;

        private GameSession _session;

        private void Start()
        {
            _dataGroup = new PredefinedDataGroup<BulletDef, BulletWidget>(_bulletContainer);
            _session = FindObjectOfType<GameSession>();
            _session.BullletModel.OnChanged += OnSkinChanged;

            OnSkinChanged();
        }

        private void OnSkinChanged()
        {
            _dataGroup.SetData(DefsFacade.I.Bullets.All);
        }

        private void OnDestroy()
        {
            _session.BullletModel.OnChanged -= OnSkinChanged;
        }
    }
}