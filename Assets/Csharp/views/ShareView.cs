using UnityEngine;
using System.Collections;
using cn.sharesdk.unity3d;

public class ShareView : MonoBehaviour {
    public GameObject _aniRoot;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void btnOnShare(int idx)
    {
        PlatformType[] types = { PlatformType.WechatPlatform, PlatformType.WeChatMoments, PlatformType.QZone, PlatformType.SinaWeibo };
        if (SceneMgr.getInstance()._isInQuestionView)
        {
            ShareTools.getCurShareTools().shareByTypeWithHide(types[idx]);
        }
        else
        {
            ShareTools.getCurShareTools().shareByTypeWithoutCapture(types[idx]);
        }
    }

        

    public void btnOnClose()
    {
        _aniRoot.GetComponent<Animator>().Play("sharehide");
        Invoke("removeSelf",0.5f);
    }


    public void removeSelf()
    {
        gameObject.SetActive(false);
    }
}
