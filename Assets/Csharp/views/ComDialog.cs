using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComDialog : MonoBehaviour {
    public Text _text;
    public string _textContext;
    public Button _btnOk;

    public int[] _removeWrongCostNum;
    // Use this for initialization
    private bool _pressFlag = false;
    void Start () {
        _text.text = _textContext;
        GetComponent<Animator>().Play("dialogshow");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

   public void btnCancle() {
        if (_pressFlag)
            return;
        GetComponent<Animator>().SetBool("close", true);
        _pressFlag = true;
    }

    public void btnOnRemoveWrong()
    {
        if (_pressFlag)
            return;

        int curidx = 3 - QuestionMgr.getInstance().getWrongCount();
        if (curidx == 3)
        {
            PlatTools.AndroidPopTos("没有错误答案了");
            PlatTools.JavaToUnity();
            return;
        }

        if (UserData.getInstance().getGoldNum() >= _removeWrongCostNum[curidx])
        {
            if (QuestionMgr.getInstance().checkCanRemoveWrong())
            {
                UserData.getInstance().addGoldNum(-_removeWrongCostNum[curidx]);
                QuestionMgr.getInstance().RemoveWrongSelect();
                btnCancle();
                _pressFlag = true;
            }
            else
            {
                PlatTools.AndroidPopTos("无法移除错误答案");              
                return;
            }
           
        }
        else
        {
            PlatTools.AndroidPopTos("金币不足");
        }

    }

    public void removeSelf()
    {
        Destroy(gameObject);
    }
}
