using Assets.Scripts.Model;
using Assets.Scripts.Model.Definition.Repositories;
using Assets.Scripts.UI.DataGroup;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.UI.Event
{
    public class EventLevelWidget : MonoBehaviour, IItemRenderer<EventDef>, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        [SerializeField] private Image _iconLevel;
        [SerializeField] private Image _iconSkin;
        [SerializeField] private Button _firstPlay;
        [SerializeField] private Button _replay;
        [SerializeField] private Button _showAdButton;
        [SerializeField] private Text _seen;
        [SerializeField] private Text _needSee;


        private EventDef _data;
        private GameSession _session;

        private readonly string _androidAdUnitId = "Rewarded_Android";
        private readonly string _iOSAdUnitId = "Rewarded_iOS";
        private string _adUnitId = null;

        private void Awake()
        {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
            _adUnitId = _androidAdUnitId;
#endif

        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            UpdateView();
        }

        public void SetData(EventDef data, int index)
        {
            _data = data;

            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            _iconLevel.sprite = _data.IconLevel;
            if (_session.Data.ShowAds.Value >= _data.NeedSeeAd)
            {
                _firstPlay.gameObject.SetActive(!_session.Data.UsedEvents.Contains(_data.Id));
                _replay.gameObject.SetActive(_session.Data.UsedEvents.Contains(_data.Id));
                _showAdButton.gameObject.SetActive(false);
            }
            else
            {
                _firstPlay.gameObject.SetActive(false);
                _replay.gameObject.SetActive(false);
                _seen.text = _session.Data.ShowAds.Value.ToString();
                _needSee.text = _data.NeedSeeAd.ToString();
                _showAdButton.gameObject.SetActive(true);
                _showAdButton.interactable = false;
                LoadAd();

            }
        }

        private void AddShowAdsCount()
        {
            _session.Data.ShowAds.Value += 1;
        }

        public void LoadEvent()
        {
            _session.Data.NormalLevel.Value = SceneManager.GetActiveScene().buildIndex;
            _session.Data.UsedEvents.Add(_data.Id);
            _session.SkinModel.Unlock(_data.IdSkin);
            _session.SkinModel.SelectSkin(_data.IdSkin);
            _session.Data.LastEventLevel.Value = _data.NameLastLevel;
            _session.Save();
            SceneManager.LoadScene(_data.NameFirstLevel);
        }

        public void Replay()
        {
            _session.Data.NormalLevel.Value = SceneManager.GetActiveScene().buildIndex;
            _session.SkinModel.SelectSkin(_data.IdSkin);
            _session.Data.LastEventLevel.Value = _data.NameLastLevel;
            _session.Save();
            SceneManager.LoadScene(_data.NameFirstLevel);
        }

        public void LoadAd()
        {
            Advertisement.Load(_adUnitId, this);
        }

        public void OnUnityAdsAdLoaded(string adUnitId)
        {
            Debug.Log("Ad Loaded: " + adUnitId);

            if (adUnitId.Equals(_adUnitId))
            {
                _showAdButton.onClick.AddListener(ShowAd);
                _showAdButton.interactable = true;
            }
        }

        public void ShowAd()
        {
            _showAdButton.interactable = false;
            Advertisement.Show(_adUnitId, this);
        }

        public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
        {
            if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            {
                Debug.Log("Unity Ads Rewarded Ad Completed");
                AddShowAdsCount();
                Advertisement.Load(_adUnitId, this);
            }
        }

        public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
        {
            Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        }

        public void OnUnityAdsShowStart(string adUnitId) { }
        public void OnUnityAdsShowClick(string adUnitId) { }

        void OnDestroy()
        {
            _showAdButton.onClick.RemoveAllListeners();
        }
    }
}