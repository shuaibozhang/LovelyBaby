using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using cn.sharesdk.unity3d;

public class NextView : MonoBehaviour {
    private Animator shareAnimator;
    private Animator popAnimator;
    public Text _answerDes;
    public Text _answerFrom;
    public GameObject _shareRoot;
    public GameObject _popRoot;
    // Use this for initialization
    void Start () {
        shareAnimator = _shareRoot.GetComponent<Animator>();
        popAnimator = _popRoot.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void btnOnShowShare()
    {
        //shareAnimator.Play("showshare");
        _shareRoot.SetActive(true);
    }
    public void btnOnShare(int idx)
    {
        PlatformType[] types = { PlatformType.WechatPlatform, PlatformType.WeChatMoments, PlatformType.QZone, PlatformType.SinaWeibo };
        ShareTools.getCurShareTools().shareByType(types[idx]);
    }

    public void btnOnNext()
    {
        if (!QuestionMgr.getInstance().showNextQuestion())
        {
            QuestionMgr.getInstance().showTestResult();
        }
        _shareRoot.SetActive(false);
        SceneMgr.getInstance().hideNext();

        //popAnimator.Play("nextPopIn");
    }

    public void showDesByIdx(int idx)
    {

    }

    public void btnOnCancle()
    {

    }

    public void reSetDes()
    {
        _answerDes.text = QuestionMgr.getInstance().getCurAnswerDes();
        _answerFrom.text = QuestionMgr.getInstance().getCurAnswerFrom();
    }
}
