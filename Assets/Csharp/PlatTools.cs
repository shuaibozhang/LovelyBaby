using UnityEngine;
using System.Collections;

public class PlatTools {

    public static void AndroidPopTos(string message)
    {


        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc1 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo1 = jc1.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject jo = new AndroidJavaObject("com.gl.unitytoandroid.UnityToAndroid", jo1);
            if (jo == null)
            {

            }
            else
            {
                jo.Call("popTo", message);
            }
        }
        else
        {
            Debug.Log(message);
        }

        //jc1.CallStatic("UnitySendMessage", "Main Camera", "JavaMessage", "whoowhoo");
    }

    public static void JavaToUnity()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            AndroidJavaClass jc1 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");           
            jc1.CallStatic("UnitySendMessage", "Main Camera", "JavaMessage", "whoowhoo");
        }
    }

    public static void buy(string itemid, int price)
    {
        AndroidJavaClass jc1 = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = jc1.GetStatic<AndroidJavaObject>("currentActivity");
      
        if (activity == null)
        {

        }
        else
        {
            activity.Call("buy", itemid, price);
        }
    }

}
