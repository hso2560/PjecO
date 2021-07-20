using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdMng : MonoBehaviour
{
    const string gameID = "3894331";

    private void Start()
    {
        Advertisement.Initialize(gameID);
        ShowAd();
    }
    public void ShowAd()
    {
        StartCoroutine(ShowBannerWhenReady());
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady())
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Show("Video");
    }
   /* public string bannerPlacement = "DefpBanner";
#if UNITY_IOS 
    public const string gameID = "3894330"; 
#elif UNITY_ANDROID 
    public const string gameID = "3894331";
#endif

    private void Start()
    {
        ShowAd();
    }
    public void ShowAd()
        {
            Advertisement.Initialize(gameID);
            StartCoroutine(ShowBannerWhenReady());
        }

        IEnumerator ShowBannerWhenReady()
        {
            while (!Advertisement.IsReady(bannerPlacement))
            {
                yield return new WaitForSeconds(0.5f);
            }
            Advertisement.Banner.Show(bannerPlacement);
        }*/
        /* private const string android_game_id = "3894331";
         private const string ios_game_id = "3894330";
         private const string rewarded_video_id = "rewardedVideo";
         private void Start()
         {
             Initialize();
        Invoke("ShowRewardedAd", 1.5f);
         }
         private void Initialize()
         {
     #if UNITY_ANDROID
             Advertisement.Initialize(android_game_id);
     #elif UNITY_IOS
             Advertisement.Initialize(ios_game_id);
     #endif
         }

         public void ShowRewardedAd()
         {
             if(Advertisement.IsReady(rewarded_video_id))
             {
                 var options = new ShowOptions { resultCallback = HandleShowResult };
                 Advertisement.Show("rewardedVideo", options);
                 Advertisement.Show(rewarded_video_id);
             }
         }

         private void HandleShowResult(ShowResult result)
         {
             switch(result)
             {
                 case ShowResult.Finished:
                     break;
                 case ShowResult.Skipped:
                     break;
                 case ShowResult.Failed:
                     break;
             }
         }*/
    }
