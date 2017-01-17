using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using admob;

public class UnityAdsInitializer : MonoBehaviour {
    [SerializeField]
    private string
        androidGameId = "18658",
        iosGameId = "18660";

    [SerializeField]
    private bool testMode;

    [SerializeField]
    private string androidAdmobBannerId = "ca-app-pub-9548390162005173/7702130442";
    // Use this for initialization
    void Start () {
        string gameId = null;

#if UNITY_ANDROID
        gameId = androidGameId;
#elif UNITY_IOS
        gameId = iosGameId;
#endif

        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, testMode);
        }

        Admob.Instance().initAdmob(androidAdmobBannerId, "");
        Admob.Instance().showBannerRelative(AdSize.Banner, AdPosition.BOTTOM_CENTER, 0);
        if (testMode)
        {
            Admob.Instance().setTesting(true);
            Admob.Instance().setForChildren(true);
        }
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
