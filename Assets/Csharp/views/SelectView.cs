using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;

public class SelectView : MonoBehaviour {
    List<GameObject> _listAniClockGameObjects;
    private int _curIdx;
    void Awake()
    {
        _curIdx = UserData.getInstance().getCurChapterIdx();

        _listAniClockGameObjects = new List<GameObject>();
        for (int j = 0; j < 2; j++)
        {        
            for (int i = 0; i < 10; i++)
            {
                string path = "Canvas/scrollview/viewport/Content/panel" + j + "/btnquest" + i + "/clock";
                Debug.Log(path);
                var find = GameObject.Find(path);
                _listAniClockGameObjects.Add(find);
            }
        }
    }
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 20; i++)
        {
            if (UserData.getInstance().getCurChapterIdx() >= i)
            {
                _listAniClockGameObjects[i].SetActive(false);
            }
        }
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void checkShowUnlockAni(float delaytime)
    {
        if (_curIdx != UserData.getInstance().getCurChapterIdx())
        {
            _curIdx = UserData.getInstance().getCurChapterIdx();
            Invoke("showUnlockAni", delaytime);
        }
    }

    void showUnlockAni()
    {
        _listAniClockGameObjects[_curIdx].GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "jiesuo", false);
    }
}
