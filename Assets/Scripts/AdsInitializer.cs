using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener {
    [SerializeField] Button showAdButton;
    [SerializeField] string androidGameId = "Android Game Id";
    [SerializeField] string IOSGameId = "IOS Game ID";
    [SerializeField] string androidAdUnitId = "Interstitial_Android";
    [SerializeField] string iOsAdUnitId = "Interstitial_iOS";
    [SerializeField] string androidRewardedAdUnitId = "Rewarded_Android";
    [SerializeField] string iOsRewardedAdUnitId = "Rewarded_iOS";
    [SerializeField] string androidBannerAdUnitId = "Banner_Android";
    [SerializeField] string iOsBannerAdUnitId = "Banner_iOS";
    string gameId;
    string adUnitId;
    [SerializeField] bool testMode = false;

    private void Awake() {
        if(Application.internetReachability == NetworkReachability.NotReachable) {
            FindObjectOfType<GameManager>().canvas.transform.GetChild(12).gameObject.SetActive(false);
            GameObject.Find("Ads").gameObject.SetActive(false);
        }
        else {
            InitializeAds();
            showAdButton.onClick.AddListener(LoadRewardedAd);
        }
        //LoadInterstitialAd();
        //if (Advertisement.isInitialized) {
        //    Debug.Log("Advertisement is initialized");
        //    LoadInterstitialAd();
        //}
        //else {
        //    InitializeAds();
        //    showAdButton.onClick.AddListener(LoadRewardedAd);
        //}
    }

    public void InitializeAds() {
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSGameId : androidGameId;
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void OnInitializationComplete() {
        Debug.Log("Unity Ads initialization complete.");
        //oyun ilk acildiginda reklam goster.
        //LoadInterstitialAd();
        //LoadBannerAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) {
        Debug.Log($"Unity Ads initialization failed : {error.ToString()} - {message}");
    }

    public void LoadInterstitialAd() {
        // ad yuklenmeden oyunu baslatmayi engellemek icin.
        Debug.Log("interstitial ad showing");
        Time.timeScale = 0;
        FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>().mute = true;
        FindObjectOfType<GameManager>().canvas.transform.GetChild(13).gameObject.SetActive(true);
        Advertisement.Load((Application.platform == RuntimePlatform.IPhonePlayer) ? iOsAdUnitId : androidAdUnitId, this);
    }

    public void LoadRewardedAd() {
        Debug.Log("rewarded ad showing");
        //button a tikladiktan sonra oyunu hemen baslatmayi engellemek icin.
        Time.timeScale = 0;
        FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>().mute = true;
        FindObjectOfType<GameManager>().canvas.transform.GetChild(13).gameObject.SetActive(true);
        Advertisement.Load((Application.platform == RuntimePlatform.IPhonePlayer) ? iOsRewardedAdUnitId : androidRewardedAdUnitId, this);
    }

    public void LoadBannerAd() {
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load((Application.platform == RuntimePlatform.IPhonePlayer) ? iOsBannerAdUnitId : androidBannerAdUnitId, new BannerLoadOptions {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        }); ;
    }

    void OnBannerLoaded() {
        // new BannerOptions la ekstra callback ler kullanilabilir.
        Advertisement.Banner.Show((Application.platform == RuntimePlatform.IPhonePlayer) ? iOsBannerAdUnitId : androidBannerAdUnitId);
    }

    void OnBannerError(string message) {

    }

    public void OnUnityAdsAdLoaded(string placementId) {
        Debug.Log("OnUnityAdsAdLoaded");
        Advertisement.Show(placementId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message) {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>().mute = false;
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message) {
        Debug.Log("OnUnityAdsShowFailure");
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>().mute = false;
    }

    public void OnUnityAdsShowStart(string placementId) {
        Debug.Log("OnUnityShowStart");
        //Time.timeScale = 0;
        //FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>().mute = true;

        //Advertisement.Banner.Hide();
    }

    public void OnUnityAdsShowClick(string placementId) {
        Debug.Log("OnUnityAdsShowClick");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState) {
        //izlenen reklamin rewarded ad olup olmadigi + reklami bitirilip bitirilmedigi kontrol edilir.
        Debug.Log(placementId);
        if (placementId.Equals(androidRewardedAdUnitId) && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState)) {
            Debug.Log("give player money");
            PlayerPrefs.SetInt("TotalCoins", GameManager.GetTotalCoins() + 100);
            FindObjectOfType<GameManager>().coinsText.text = $"Coins: {GameManager.GetTotalCoins()}";
        }
        if (placementId.Equals(iOsRewardedAdUnitId) && UnityAdsShowCompletionState.COMPLETED.Equals(showCompletionState)) {
            Debug.Log("give player money");
            PlayerPrefs.SetInt("TotalCoins", GameManager.GetTotalCoins() + 100);
            FindObjectOfType<GameManager>().coinsText.text = $"Coins: {GameManager.GetTotalCoins()}";
        }
        Debug.Log($"OnUnityAdsShowComplete: {showCompletionState}");
        Time.timeScale = 1;
        FindObjectOfType<BackgroundLoop>().GetComponent<AudioSource>().mute = false;
        SceneManager.LoadScene(0);
        //FindObjectOfType<GameManager>().canvas.transform.GetChild(13).gameObject.SetActive(false);
        //Advertisement.Banner.Show((Application.platform == RuntimePlatform.IPhonePlayer) ? iOsBannerAdUnitId : androidBannerAdUnitId);
    }
}
