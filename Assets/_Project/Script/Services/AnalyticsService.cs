using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using Facebook.Unity;
using System.Collections.Generic;
//using AppsFlyerSDK;
using System.Text.RegularExpressions;

public class AnalyticsService : MonoBehaviour {

    public const string APPSFLYER_DEV_KEY = "r9vNC83N8nYpCzYGigyjUh";
    public const string APPSSTORE_ID = "1529423469";

    public const string MENU_SCENE = "menu_scene";
    public const string GAME_OVER_SCENE = "game_over_scene";
    public const string GAME_PLAY_SCENE = "game_play_scene";
    public const string PAUSE_SCENE = "pause_scene";
    public const string REVIVIAL_SCENE = "revival_scene";
    public const string PURCHASE_SCENE = "purchase_scene";

    public const string AD_REMOVE_PURCHASED = "ad_remove_purchased";
    public const string RATE_OFFERED = "rate_offered";
    public const string CHARACTER_UNLOCK_OFFERED = "character_unlock_offered";
    public const string CHARACTER_UNLOCK_OFFER_TAKEN = "character_unlocked";
    public const string FREE_GIFT_OFFERED = "free_gift_offered";
    public const string FREE_GIFT_TAKEN = "free_gift_taken";
    public const string COIN_VIDEO_OFFERED_FROM_GAMEOVER = "coin_video_offered_from_gameover";
    public const string COIN_VIDEO_TAKEN_FROM_GAMEOVER = "coin_video_taken_from_gameover";
    public const string RATE_CLICKED = "rate_clicked";
    public const string CHARACTED_PURCHASED = "character_purchased";
    public const string SHARE_BUTTON_CLICKED = "share_button_clicked";
    public const string COIN_VIDEO_OFFERED_FROM_PURCHASE_PAGE = "coin_video_offered_from_purchase_page";
    public const string COIN_VIDEO_TAKEN_FROM_PURCHASE_PAGE = "coin_video_taken_from_purchase_page";
    public const string FB_LIKE_OFFERED = "fb_like_offered";
    public const string FB_LIKE_CLICKED = "fb_like_clicked";

    public const string BANNER_SHOWN = "banner_shown";
    public const string LEVEL_ACHIEVED = "level_finish";
    public const string LEVEL_STARTED = "level_start";
    public const string GAME_START = "game_start";
    public const string AD_SHOWN_INTERSTITIAL = "ad_shown_interstitial";
    public const string OMC_SHOWN = "omc_shown";

    private static bool isClickedOnAd;
    private static string clickedAdType;
    public static int continuationCounter;
    private static bool isWatchingFullScreenAd;
    private static string lastRewardAdPlacement;
    private static string lastRewardAdRestult;
    static bool isFirstStart;
    void Awake() {

        // AnalyticsService.Technical("fb_initializing");

        if (!FB.IsInitialized) {
            AnalyticsService.Technical("fb_initializing");
            FB.Init(InitCallback, OnHideUnity);
        }
        else {
            FB.ActivateApp();
        }
 
    }

    private void Start()
    {
        lastRewardAdPlacement = "_";
        lastRewardAdRestult = "_";
        AnalyticsService.Technical("appsflyer_loading");
//        AppsFlyer.setAppsFlyerKey(APPSFLYER_DEV_KEY);
//        /* For detailed logging */
//        /* AppsFlyer.setIsDebug (true); */
//#if UNITY_IOS
//              /* Mandatory - set your apple app ID
//              NOTE: You should enter the number only and not the "ID" prefix */
//              AppsFlyer.setAppID (APPSSTORE_ID);
//              AppsFlyer.getConversionData();
//              AppsFlyer.trackAppLaunch ();
//#elif UNITY_ANDROID
//        /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
//        AppsFlyer.init(APPSFLYER_DEV_KEY, "AppsFlyerTrackerCallbacks");
//#endif
    }

    private void InitCallback() {
        if (FB.IsInitialized) {
            AnalyticsService.Technical("fb_initialized");
            FB.ActivateApp();
            Report(GAME_START);
        }
        else {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown) {
    }

    public static void Report(string sceneName)
    {
        ReportToFacebook(sceneName);
        ReportToFirebase(sceneName);
        //ReportToAppMetric(sceneName);
    }

    public static void ReportToFacebook(string eventName) {
        if (FB.IsInitialized) {
            FB.LogAppEvent(
                eventName
            );
        }

    }

    public static void ReportToFirebase(string eventName) {
#if !UNITY_EDITOR
        //Firebase.Analytics.FirebaseAnalytics.LogEvent(
        //    eventName
        //);
#endif
    }

    public static void ReportToAppMetric(string eventName, Dictionary<string, object> param)
    {
        AppMetrica.Instance.ReportEvent(eventName, param);
        AppMetrica.Instance.SendEventsBuffer();
    }

    public static void Technical(string stepName)
    {
         bool isFirst = false;
        if (!PlayerPrefs.HasKey("FirstTime"))
        {           
            isFirst = true;
        }
        else
        {           
            isFirst = false;
        }       


        ReportToAppMetric("technical", new Dictionary<string, object>() {
            { "step_name", stepName },
            { "first_start", isFirst }
        });
        
    }

    public static void TechnicalFirst(bool isFirst)
    {
        ReportToAppMetric("technical", new Dictionary<string, object>() {
            { "first_start", isFirst }
        });

    }

    public static void LevelStart(int levelNum, string levelName, int loopCount)
    {
        isWatchingFullScreenAd = false;
        levelName = levelName.ToLower();
        Debug.Log("levell start" + levelName + " number " + levelNum + " israndom " + loopCount);
        ReportToAppMetric("level_start", new Dictionary<string, object>() {
            {"level_number", levelNum},
            {"level_name", levelName }
            
        });
    }

    public static void LevelFinish(int levelNum, string levelName, int loopCount)
    {
        levelName = levelName.ToLower();
        Debug.Log("levell finish" + levelName + " number "+ levelNum+ " israndom " + loopCount);
 
        ReportToAppMetric("level_finish", new Dictionary<string, object>() {
            {"level_number", levelNum},
            {"level_name", levelName }
         
        });
    }
    
    /*
    public static void LevelStart(string levelName)
    {
        ReportToAppMetric("level_start", new Dictionary<string, object>() {
            { "level_number", Controller.self.levelController.GetCurrentLevelIndex() },
            {"level_name",levelName }            
        });
    }

    public static void LevelFinish(bool win, string levelName, int time, bool levelRandom)
    {
        ReportToAppMetric("level_finish", new Dictionary<string, object>() {
            { "level_number", Controller.self.levelController.GetCurrentLevelIndex() },
            {"result", win ? "win" : "lose" },
            {"level_name", levelName },
            {"time", time },
            { "level_random", levelRandom}
        });
    }
    */
    public static void VideoAdsAvailable(string adType, string placement, string result)
    {
        placement = placement.ToLower();
        if (isClickedOnAd && adType == "rewarded")
        {
            isClickedOnAd = false;
        }
        isWatchingFullScreenAd = false;

        ReportToAppMetric("video_ads_available", new Dictionary<string, object>() {
            {"ad_type", adType },
            {"placement", placement},
            {"result", result},
            {"connection", Application.internetReachability != NetworkReachability.NotReachable}

            //add more things
        });
    }

    public static void VideoAdsStarted(string adType, string placement, string result)
    {
        placement = placement.ToLower();
        if (isClickedOnAd && adType == "rewarded")
        {
            isClickedOnAd = false;
        }
        isWatchingFullScreenAd = true;

        Debug.Log("Adservice : video_ads_started " + adType + " " + placement + " " + result);
        ReportToAppMetric("video_ads_started", new Dictionary<string, object>() {
            {"ad_type", adType },
            {"placement",  placement},
            {"result", result },
            {"connection",  Application.internetReachability != NetworkReachability.NotReachable}
            //add more 
        });
    }

    public static void VideoAdsWatched(string adType, string placement, string result)
    {
        placement = placement.ToLower();
       
        if(adType == "rewarded")
        {
            if(result == "clicked")
            {
                isClickedOnAd = true;
                return;
            }
            if(result == "watched" && isClickedOnAd)
            {
                result = "clicked";
                isClickedOnAd = false;
            }
        }

    

        if (isWatchingFullScreenAd && adType == "banner")
        {
            return;
        }
       



        Debug.Log("Adservice : video_ads_watch " + adType + " " + placement + " " + result);
        ReportToAppMetric("video_ads_watch", new Dictionary<string, object>(){
            { "ad_type", adType},
            { "placement", placement},
            {"result", result },
            { "connection", Application.internetReachability != NetworkReachability.NotReachable}
        });
    }

    public static void PaymentSucceed(string ID,  string currencyType = "soft", string price = "100", string currency = "USD")  //need to fix
    {
        price = Regex.Replace(price, "[^a-zA-Z0-9_.,]+", "", RegexOptions.Compiled);

        ReportToAppMetric("payment_succeed", new Dictionary<string, object>() {
            {"inapp_id" , ID },
            { "currency", currencyType == "soft" ? "Virtual" : currency},
            { "price", price },
            {"inapp_type", currencyType }
        });
        System.Collections.Generic.Dictionary<string, string> purchaseEvent = new
        System.Collections.Generic.Dictionary<string, string>();
        /*purchaseEvent.Add(AFInAppEvents.CURRENCY, currency);
        //purchaseEvent.Add(AFInAppEvents.REVENUE, "200");
        purchaseEvent.Add(AFInAppEvents.QUANTITY, "1");
        purchaseEvent.Add(AFInAppEvents.REVENUE, price);
        // purchaseEvent.Add(AFInAppEvents.CONTENT_TYPE, "category_a");
        purchaseEvent.Add(AFInAppEvents.CONTENT_ID, ID);
        AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, purchaseEvent);
        */
    }

    public static void SkinUnlock(string skin_name, string skin_type, string skin_rarity, string unlock_type)
    {
        ReportToAppMetric("skin_unlock", new Dictionary<string, object>()
        {
             {"skin_name" , skin_name },
             {"skin_type" , skin_type },
             {"skin_rarity" , skin_rarity },
            {"unlock_type" , unlock_type },
        });

        Debug.Log("Skin Unlock: skin_name " + skin_name + " skin_type " + skin_type + " skin_rarity " + skin_rarity + " unlock_type " + unlock_type);
    }

    public static void RateUs(string reason, int rateResult)
    {
        ReportToAppMetric("rate_us", new Dictionary<string, object>()
        {
             {"show_reason" , reason },
             { "rate_result", rateResult },
        });
    }

}
