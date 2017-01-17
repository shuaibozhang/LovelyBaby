using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class TiliMgr : MonoBehaviour {
    public float _invokeRepeatDeltaTime = 1.0f;
    public float _maxTiliNum = 120f;
    public delegate void TiliChangeDelegate(int curTili);
    public static event TiliChangeDelegate _tiliChangeDelegate;
    public GameObject _timeMgrObject;
    private TimeMgr _timeMgr;

    public float _recoverRate = 1f / 60f;

    private static TiliMgr s_curTiliMgr = null;

    public Text tiliText;
    void Start () {
        s_curTiliMgr = this;

        _timeMgr = _timeMgrObject.GetComponent<TimeMgr>();
        InvokeRepeating("Timer", _invokeRepeatDeltaTime, _invokeRepeatDeltaTime);
        happinessChange(UserData.getInstance().getHappinessNum());
    }

    void OnDestroy() {
        s_curTiliMgr = null;       
    }

   static public TiliMgr getInstance() {
        return s_curTiliMgr;
    }

    void OnEnable()
    {
        TimeMgr._timeGet += OnTimeGet;
        UserData._happinessChangeAction += happinessChange;
    }

    void OnDisable()
    {
        UserData._happinessChangeAction -= happinessChange;
        TimeMgr._timeGet -= OnTimeGet;
    }

    void happinessChange(int curnum)
    {
        float[] stageRecoverRate = { 1f / 60f, 1f / 60f / 1.2f , 1f / 60f / 1.5f , 1f / 60f / 1.8f , 1f / 60f / 2f };
        var stage = UserData.getInstance().getCurBabyStage();
        _recoverRate = stageRecoverRate[(int)stage];
    }

    // Update is called once per frame
    void Update () {
	
	}

    void Timer()
    {
        if (_timeMgr.isGetOnlineTime())
        {
            float curTili = PlayerPrefs.GetFloat(UserData.KEY_TILI_NUM, 0f);
            curTili += (_recoverRate * _invokeRepeatDeltaTime);
            if (curTili >= _maxTiliNum)
            {
                curTili = _maxTiliNum;
            }
            PlayerPrefs.SetFloat(UserData.KEY_TILI_NUM, curTili);
            //Debug.Log("curTili:" + curTili);

            tiliText.text = "" + (int)curTili;
        }
       // Debug.Log(System.DateTime.Now.Second);
    }

    void fixTili()
    {
        float curTili = PlayerPrefs.GetFloat(UserData.KEY_TILI_NUM, 0.0f);

        if (curTili >= _maxTiliNum)
        {
            return;
        }

        int lastLeaveYear = PlayerPrefs.GetInt(UserData.KEY_LEAVE_YEAR);
        int lastLeaveDayInYear = PlayerPrefs.GetInt(UserData.KEY_LEAVE_DAYINYEAR);
        int lastLeaveSecInDay = PlayerPrefs.GetInt(UserData.KEY_LEAVE_SECINDAY);
        DateTime cur = _timeMgr.getOnlineTime();
        int curYear = cur.Year;
        int curDayInYear = cur.DayOfYear;
        int curSenInDay = cur.Hour * 3600 + cur.Minute * 60 + cur.Second;

        if (lastLeaveYear != curYear)
        {
            PlayerPrefs.SetFloat(UserData.KEY_TILI_NUM, _maxTiliNum);
        }
        else if (curDayInYear - lastLeaveDayInYear > 1)
        {
            PlayerPrefs.SetFloat(UserData.KEY_TILI_NUM, _maxTiliNum);
        }
        else
        {
            int passsec = 0;

            if (curDayInYear - lastLeaveDayInYear == 1)
            {
                passsec = curSenInDay + 24 * 60 * 60 - lastLeaveSecInDay;
            }
            else
            {
                passsec = curSenInDay - lastLeaveSecInDay;
            }
                
            
            if (passsec <= 0)
            {
                passsec = 0;
            }
            curTili += (float)passsec * _recoverRate * _invokeRepeatDeltaTime;

            if (curTili >= _maxTiliNum)
            {
                curTili = _maxTiliNum;
            }
            PlayerPrefs.SetFloat(UserData.KEY_TILI_NUM, curTili);

        }

    }


    void OnTimeGet(System.DateTime time) {
        Debug.Log("time chang in tili");
        fixTili();
    }

    public bool useTili(int num)
    {
        float curTili = PlayerPrefs.GetFloat(UserData.KEY_TILI_NUM);
        if (curTili >= num)
        {
            curTili = curTili - num;
            PlayerPrefs.SetFloat(UserData.KEY_TILI_NUM, curTili);
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
