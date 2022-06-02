using Assets.Scripts.Model;
using Assets.Scripts.Model.Definition.Repositories;
using Assets.Scripts.UI.DataGroup;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Shop.Bullets
{
    public class BulletWidget : MonoBehaviour, IItemRenderer<BulletDef>, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private Image _skinPreview;
        [SerializeField] private Button _showAdButton;
        [SerializeField] private Button _use;
        [SerializeField] private GameObject _active;

        private GameSession _session;
        private BulletDef _data;

        private readonly string _androidAdUnitId = "Rewarded_Android";
        private readonly string _iOSAdUnitId = "Rewarded_iOS";
        private string _adUnitId = null;

        private void Awake()
        {
            // Get the Ad Unit ID for the current platform:
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
         _adUnitId = _androidAdUnitId;
#endif

            //Disable the button until the ad is ready to show:
        }

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
                LoadAd();
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

        public void LoadAd()
        {
            // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
            Advertisement.Load(_adUnitId, this);
        }

        // If the ad successfully loads, add a listener to the button and enable it:
        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);

            if (adUnitId.Equals(_adUnitId))
            {
                // Configure the button to call the ShowAd() method when clicked:
                _showAdButton.onClick.AddListener(ShowAd);
                // Enable the button for users to click:
                _showAdButton.interactable = true;
            }
        }

        // Implement a method to execute when the user clicks the button:
        public void ShowAd()
        {
            // Disable the button:
            _showAdButton.interactable = false;
            // Then show the ad:
            Advertisement.Show(_adUnitId, this);
        }

        // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                // Grant a reward.
                OnBuy();
                // Load another ad:
                Advertisement.Load(_adUnitId, this);
            }
        }

        // Implement Load and Show Listener error callbacks:
        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
            // Use the error details to determine whether to try to load another ad.
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }

        void OnDestroy()
        {
            // Clean up the button listeners:
            _showAdButton.onClick.RemoveAllListeners();
        }

    }
}