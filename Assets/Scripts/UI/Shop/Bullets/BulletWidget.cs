using Assets.Scripts.Model;
using Assets.Scripts.Model.Definition.Repositories;
using Assets.Scripts.UI.DataGroup;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Shop.Bullets
{
    public class BulletWidget : MonoBehaviour, IItemRenderer<BulletDef>
    {
        [SerializeField] private Image _skinPreview;
        [SerializeField] private Button _showAdButton;
        [SerializeField] private Button _use;
        [SerializeField] private GameObject _active;

        private GameSession _session;
        private BulletDef _data;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(BulletDef data, int index)
        {
            _data = data;

            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            _skinPreview.sprite = _data.ImageBullet;
            _active.gameObject.SetActive(_session.Data.Bullets.Used.Value == _data.Id);
            _use.gameObject.SetActive(_session.Data.Bullets.IsUnlocked(_data.Id));
            _showAdButton.gameObject.SetActive(!_session.Data.Bullets.IsUnlocked(_data.Id));
            if (!_session.Data.Bullets.IsUnlocked(_data.Id))
            {
                _showAdButton.interactable = false;
            }
            
        }

        public void OnBuy()
        {

            _session.BullletModel.Unlock(_data.Id);

        }

        public void OnUse()
        {
            _session.BullletModel.SelectSkin(_data.Id);
        }       

        void OnDestroy()
        {
            // Clean up the button listeners:
            _showAdButton.onClick.RemoveAllListeners();
        }

    }
}