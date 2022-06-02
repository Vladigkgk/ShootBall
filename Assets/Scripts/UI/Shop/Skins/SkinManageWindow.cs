using System;
using System.Collections;
using Assets.Scripts.Model;
using Assets.Scripts.Model.Definition;
using Assets.Scripts.Model.Definition.Repositories;
using Assets.Scripts.UI.DataGroup;
using UnityEngine;

namespace Assets.Scripts.UI.Shop.Skins
{
    public class SkinManageWindow : MonoBehaviour
    {
        [SerializeField] private Transform _skinContainer;

        private PredefinedDataGroup<SkinDef, SkinWidget> _dataGroup;

        private GameSession _session;

        private void Start()
        {
            _dataGroup = new PredefinedDataGroup<SkinDef, SkinWidget>(_skinContainer);
            _session = FindObjectOfType<GameSession>();
            _session.SkinModel.OnChanged += OnSkinChanged;

            OnSkinChanged();
        }

        private void OnSkinChanged()
        {
            _dataGroup.SetData(DefsFacade.I.Skin.All);
        }

        private void OnDestroy()
        {
            _session.SkinModel.OnChanged -= OnSkinChanged;
        }
    }
}