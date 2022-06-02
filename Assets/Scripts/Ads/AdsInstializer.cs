using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Ads
{
    public class AdsInstializer : MonoBehaviour, IUnityAdsInitializationListener
    {
        private readonly string _androidGameId = "4743428";
        private readonly string _iOSGameId = "4743429";
        private readonly bool _testMode = true;
        private string _gameId;

        private InterstitialAd _ad;

        private void Awake()
        {
            InitializeAds();
        }

        public void InitializeAds()
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
                ? _iOSGameId
                : _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Unity Ads initialization complete.");
            _ad?.LoadAd();

        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        }
    }
}