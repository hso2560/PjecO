using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdmobManager : MonoBehaviour
{
    public bool isTestMode;

    private void Start()
    {
        LoadBannerAd();
       
    }


    const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerID = "ca-app-pub-4275491322703228/7791883048";
    BannerView bannerAd;

    void LoadBannerAd()
    {
        bannerAd = new BannerView(isTestMode ? bannerTestID : bannerID, AdSize.SmartBanner, AdPosition.Center);
        bannerAd.LoadAd(GetAdRequest());

    }

    public void Show()
    {
        bannerAd.Show();
    }

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
    /* private readonly string InterstitialID = "ca-app-pub-4275491322703228/5236650703";


     private InterstitialAd _interAd;
     public static bool _showAd = false;


     void Start()
     {
         _interAd = new InterstitialAd(InterstitialID);

         _interAd.OnAdLoaded += oal;
         _interAd.OnAdFailedToLoad += oaftl;
         _interAd.OnAdOpening += oao;
         _interAd.OnAdClosed += oac;
         _interAd.OnAdLeavingApplication += oala;

         load();

         show();
     }

     //구글에서 광고를 불러와 저장해놓습니다.
     private void load()
     {
         AdRequest _request = new AdRequest.Builder().Build();
         _interAd.LoadAd(_request);
     }

     //광고를 보여주고 싶은 상황에서 이 메서드를 호출하여 사용하시면 됩니다.
     public void show()
     {
         StartCoroutine("showInterAd");
     }

     private IEnumerator showInterAd()
     {
         while (!_interAd.IsLoaded())
         {
             yield return null;
         }
         _interAd.Show();
     }


     //광고 로드 완료 시
     public void oal(object sender, EventArgs args)
     {

     }
     //광고 로드 실패 시
     public void oaftl(object sender, AdFailedToLoadEventArgs args)
     {

     }
     //광고 재생 시작 시
     public void oao(object sender, EventArgs args)
     {

     }
     //광고 재생 종료 시
     public void oac(object sender, EventArgs args)
     {
         //보여준 광고를 제거하고 새로운 광고 가져오기
         _interAd.Destroy();
         load();
     }
     //재생 중 광고 클릭으로 화면 전환 시
     public void oala(object sender, EventArgs args)
     {

     }*/
    /*  public static AdmobManager instance;

      BannerView bannerView; // 광고배너

      public static AdmobManager Instance
      {
          get { return instance; }
      }

      private void Awake()
      {
          if (instance == null)
          {
              instance = this;
              DontDestroyOnLoad(this.gameObject);
          }
          else
              Destroy(this.gameObject);

          RequestBanner(); // 배너 광고 설정
      }

      private void RequestBanner()
      {
  #if UNITY_EDITOR
          string adUnitId = "unused";
  #elif UNITY_ANDROID
      string adUnitId = "ca-app-pub-4275491322703228/5971982027";
  #elif UNITY_IPHONE
      string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
  #else
      string adUnitId = "unexpected_platform";
  #endif

          // Create a 320x50 banner at the top of the screen.
          bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Center);
          // Create an empty ad request.
          AdRequest request = new AdRequest.Builder().Build();
          // Load the banner with the request.

          bannerView.LoadAd(request);
      }

      public void Load_ad()
      {
          Debug.Log("load_ad");
          bannerView.Show();
      }

      public void Exit_ad()
      {
          Debug.Log("exit_ad");
          bannerView.Hide();
      }*/
}




/*using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobManager : MonoBehaviour
{
    public bool isTestMode;
  

    private void Start()
    {

        LoadBannerAd();
        Invoke("ShowBanner", 1f);
       *//* LoadFrontAd();
        StartCoroutine(ShowScreenAd());*//*
    }


  const string bannerTestID = "ca-app-pub-3940256099942544/6300978111";
    const string bannerId = "ca-app-pub-4275491322703228/5971982027";
    BannerView bannerAd;
    void LoadBannerAd()
    {
        bannerAd = new BannerView(isTestMode ? bannerTestID : bannerId, AdSize.SmartBanner,AdPosition.Center);
        bannerAd.LoadAd(GetAdRequest());
     
        //ToggleBannerAd(true);
        
    }
    public void ShowBanner()
    {
        bannerAd.Show();
    }
    public void HideBanner()
    {
        bannerAd.Hide();
    }
    public void ToggleBannerAd(bool b)
    {
        if (b) bannerAd.Show();
        else bannerAd.Hide();
    }


   *//* const string frontTestID = "ca-app-pub-3940256099942544/1033173712";
    const string frontID = "ca-app-pub-4275491322703228/5236650703";
    InterstitialAd frontAd;

    void LoadFrontAd()
    {
        frontAd = new InterstitialAd(isTestMode ? frontTestID : frontID);
        AdRequest request = new AdRequest.Builder().Build();
        frontAd.LoadAd(request);
        //frontAd.LoadAd(GetAdRequest());

        frontAd.OnAdClosed += (sender, e) => Debug.Log("광고 닫힘");
        frontAd.OnAdLoaded += (sender, e) => Debug.Log("광고 로드");
    }*//*

   

    AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }


    
   *//* IEnumerator ShowScreenAd()
    {
        while(!frontAd.IsLoaded())
        {
            yield return null;
        }

        frontAd.Show();
    }*//*
}*/

