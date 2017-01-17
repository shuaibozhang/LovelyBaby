using UnityEngine;
using System.Collections;
using System.IO;
using cn.sharesdk.unity3d;

public class ShareTools : MonoBehaviour {
    private static ShareTools s_curShareTools = null;
    private ShareSDK ssdk;
    public GameObject shareSDKObject;
    public GameObject shareview;
    private static bool bSaveFile = false;
    private static bool bNeedHideView = false;
    void Awake()
    {
        s_curShareTools = this;
    }

    public static ShareTools getCurShareTools()
    {
        return s_curShareTools;
    }

    public void shareByType(PlatformType type)
    {
        StartCoroutine("Capture", type);
    }

    public void shareByTypeWithHide(PlatformType type)
    {
        bNeedHideView = true;
        StartCoroutine("Capture", type);
    }

    public void shareByTypeWithoutCapture(PlatformType type)
    {
        ShareContent content = new ShareContent();
        content.SetText("this is a test string.");
        content.SetTitle("test title");
        content.SetShareType(ContentType.Image);

        if (type == PlatformType.QZone)
        {
            content.SetSite("Mob-ShareSDK");
            content.SetSiteUrl("http://www.mob.com");
            content.SetTitleUrl("http://www.mob.com");
            content.SetUrl("http://www.mob.com");
            content.SetComment("test description");
        }
        ssdk.ShowShareContentEditor(type, content);
    }

    private IEnumerator Capture(PlatformType type)
    {
        if (bNeedHideView)
        {
            shareview.GetComponent<CanvasGroup>().alpha = 0;
        }

        string pathSave = Application.persistentDataPath + "/" + "share.png";
        if (File.Exists(pathSave))
        {
            File.Delete(pathSave);
        }

        yield return new WaitForEndOfFrame();

        var path = ShareTools.CaptureScreenshot("share.png");

        while (!File.Exists(path))
        {
            yield return 0;
        }

        if (bNeedHideView)
        {
            shareview.GetComponent<CanvasGroup>().alpha = 1;
            bNeedHideView = false;
        }

        ShareContent content = new ShareContent();
        content.SetText("this is a test string.");
        content.SetImagePath(Application.persistentDataPath + "/share.png");
        content.SetTitle("test title");
        content.SetShareType(ContentType.Image);

        if (type == PlatformType.QZone)
        {
            content.SetSite("Mob-ShareSDK");
            content.SetSiteUrl("http://www.mob.com");
            content.SetTitleUrl("http://www.mob.com");
            content.SetUrl("http://www.mob.com");
            content.SetComment("test description");
            //content.SetImageUrl("https://f1.webshare.mob.com/code/demo/img/1.jpg");
        }

        //ssdk.ShareContent(PlatformType.SinaWeibo, content);
        //ssdk.Authorize(type);
        ssdk.ShowShareContentEditor(type, content);
        
    }

    public static string CaptureScreenshot(string filename)
    {
        Application.CaptureScreenshot(filename);
        string pathSave = Application.persistentDataPath + "/" + filename;
        //if (Application.platform == RuntimePlatform.Android)
        //{
        //    string destination = "/mnt/sdcard/DCIM/LovelyBaby/";
        //    if (!Directory.Exists(destination))
        //    {
        //        Directory.CreateDirectory(destination);
        //    }
        //    destination = destination + "/" + filename;

        //    FileStream fs = new FileStream(pathSave, FileMode.Open);           
        //    long size = fs.Length;
        //    byte[] array = new byte[size];
        //    fs.Read(array, 0, array.Length);
        //    fs.Close();
        //    File.WriteAllBytes(destination, array);

        //    bSaveFile = true;
        //}
        return pathSave;
    }

    // Use this for initialization
    void Start () {
        ssdk = shareSDKObject.GetComponent<ShareSDK>();
        ssdk.authHandler = AuthResultHandler;
        ssdk.shareHandler = ShareResultHandler;
        ssdk.showUserHandler = GetUserInfoResultHandler;
    }
    void ShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("share result :");
            print(MiniJSON.jsonEncode(result));
            PlatTools.AndroidPopTos("分享成功");
        }
        else if (state == ResponseState.Fail)
        {
            print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
            PlatTools.AndroidPopTos("分享失败" + "fail!error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
            PlatTools.AndroidPopTos("分享取消");
        }
    }

    void AuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("authorize success !");
            PlatTools.AndroidPopTos("认证成功");
        }
        else if (state == ResponseState.Fail)
        {
            print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
            PlatTools.AndroidPopTos("认证失败");
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
            PlatTools.AndroidPopTos("认证取消");
        }
    }

    void GetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable result)
    {
        if (state == ResponseState.Success)
        {
            print("get user info result :");
            print(MiniJSON.jsonEncode(result));
            PlatTools.AndroidPopTos("获取信息成功");
        }
        else if (state == ResponseState.Fail)
        {
            print("fail! error code = " + result["error_code"] + "; error msg = " + result["error_msg"]);
            PlatTools.AndroidPopTos("获取信息失败");
        }
        else if (state == ResponseState.Cancel)
        {
            print("cancel !");
            PlatTools.AndroidPopTos("用户取消");
        }
    }
    // Update is called once per frame
    void Update () {
	
	}
}
