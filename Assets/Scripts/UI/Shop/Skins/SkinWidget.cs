using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Model;
using Assets.Scripts.Model.Definition;
using Assets.Scripts.Model.Definition.Repositories;
using Assets.Scripts.UI.DataGroup;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Shop.Skins
{
    public class SkinWidget : MonoBehaviour, IItemRenderer<SkinDef>
    {
        [SerializeField] private Image _skinPreview;
        [SerializeField] private Text _price;
        [SerializeField] private Button _buy;
        [SerializeField] private Button _use;
        [SerializeField] private GameObject _active;

        private GameSession _session;
        private SkinDef _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(SkinDef data, int index)
        {
            _data = data;

            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            _skinPreview.sprite = _data.SkinPreview;
            _price.text = _data.Price.ToString();
            _active.gameObject.SetActive(_session.Data.Skins.Used.Value == _data.Id);
            _use.gameObject.SetActive(_session.Data.Skins.IsUnlocked(_data.Id));
            _buy.gameObject.SetActive(!_session.Data.Skins.IsUnlocked(_data.Id));
        }

        public void OnBuy()
        {
            if (_session.SkinModel.CanBuy(_data.Id))
            {
                _session.SkinModel.Unlock(_data.Id);
            }
        }

        public void OnUse()
        {
            _session.SkinModel.SelectSkin(_data.Id);
        }
    }
}
