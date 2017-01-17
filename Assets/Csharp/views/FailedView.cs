using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FailedView : MonoBehaviour {
    public Text _wrongText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void btnOnRestart()
    {
        QuestionMgr.getInstance().clearInfo();
        QuestionMgr.getInstance().showQuestionByIdx(0);
        SceneMgr.getInstance().showQuestionLayer();
    }

    public void btnOnHome()
    {
        QuestionMgr.getInstance().clearInfo();
        SceneMgr.getInstance().showSelectLayer();
    }

    public void setWrongNum(int num)
    {
        _wrongText.text = "" + num;
    }
}
