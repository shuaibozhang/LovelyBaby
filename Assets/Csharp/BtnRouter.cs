using UnityEngine;
using System.Collections;

public class BtnRouter : MonoBehaviour {
    public GameObject sceneMgrGameObject;
    public GameObject questionMgrObject;

    private SceneMgr sceneMgr;
    private QuestionMgr questionMgr;
    // Use this for initialization
    private static BtnRouter s_instance = null;
    
    public static BtnRouter getInstance()
    {
        return s_instance;
    }

    void Awake()
    {
        sceneMgr = sceneMgrGameObject.GetComponent<SceneMgr>();
        questionMgr = questionMgrObject.GetComponent<QuestionMgr>();
        s_instance = this;
    }

	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void btnOnQuestionByIdx(int idx)
    {
        if (UserData.getInstance().getCurChapterIdx() < idx)
        {
            PlatTools.AndroidPopTos("关卡未解锁");
        }
        else if (TiliMgr.getInstance().useTili(8))
        {
            QuestionMgr.getInstance().loadQuestFromJsonFile(idx);
            sceneMgr.showQuestionLayer();
        }
        else
        {
            PlatTools.AndroidPopTos("体力不足");
            QuestionMgr.getInstance().loadQuestFromJsonFile(idx);
            sceneMgr.showQuestionLayer();
        }
       
    }

    public void btnOnAnswerByIdx(int idx)
    {
        questionMgr.playerSeletcCallBack(idx);
    }

    public void shareToOther(int idx)
    {

    }

    public void nextQuestion()
    {
    }
}
