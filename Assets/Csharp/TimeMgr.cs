using UnityEngine;
using System.Collections;

public class TimeMgr : MonoBehaviour {
    public string timeURL = "http://time.tianqi.com/";
    private System.DateTime _onlineTime;
    private bool _getOnlineTime = false;

    public float _invokeRepeatDeltaTime = 1.0f;

    public delegate void TimeGetDelegate(System.DateTime time);
    public static event TimeGetDelegate _timeGet;

    private const int _gameStartYear = 2016;
    private const int _gameStartMonth = 11;
    private const int _gameStartDayInMonth = 11;

    private static TimeMgr _instance = null;

    public static TimeMgr getCurTimeMgr()
    {
        Debug.Assert(_instance != null, "instance is null");
        return _instance;
    }

    void Awake()
    {
        PlatTools.AndroidPopTos("awake");
        _instance = this;
        _onlineTime = System.DateTime.Now;
    }

    // Use this for initialization
    void Start () {
        PlatTools.AndroidPopTos("start");
        InvokeRepeating("Timer", _invokeRepeatDeltaTime, _invokeRepeatDeltaTime);
        //because when open the app, onAppLicationPause(false) will be called
        // StartCoroutine(GetTime());
    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            _getOnlineTime = false;
            StartCoroutine(GetTime());
        }
        else
        {
            reSetLeaveTime();
        }
    }

    void reSetLeaveTime() {
        if (_getOnlineTime)
        {
            PlayerPrefs.SetInt(UserData.KEY_LEAVE_YEAR, _onlineTime.Year);
            PlayerPrefs.SetInt(UserData.KEY_LEAVE_DAYINYEAR, _onlineTime.DayOfYear);
            PlayerPrefs.SetInt(UserData.KEY_LEAVE_SECINDAY, _onlineTime.Hour * 3600 + _onlineTime.Minute * 60 + _onlineTime.Second);
        }     
    }

    public bool isGetOnlineTime()
    {
        return _getOnlineTime;
    }

    IEnumerator GetTime()
    {
        Debug.Log("Start get web time");
        WWW www = new WWW(timeURL);
        while (!www.isDone)
        {
            Debug.Log("Getting web time");
            yield return www;
            if (www.error == null)
            {
                var data = www.responseHeaders["DATE"];
                _onlineTime = System.DateTime.Parse(data);
                Debug.Log("get online time");              

                _getOnlineTime = true;

                if (_timeGet != null)
                {
                    _timeGet(_onlineTime);
                }

                reSetLeaveTime();
            }
            else
            {
                Debug.Log("get time error");
                _getOnlineTime = false;
            }
        }
    }

    void Timer()
    {
        if (_getOnlineTime)
        {
            _onlineTime = _onlineTime.AddSeconds(_invokeRepeatDeltaTime);
        }
    }

    /**
    *before call getOnlineTime, you should check "_getOnlineTime" value is "true", otherwise the time is not right
    */
    public System.DateTime getOnlineTime()
    {        
        return _onlineTime;        
    }

    public static int compareTheDay(int srcYear, int srcDay, int desYear, int desDay)
    {
        int ret = 0;

        if (srcYear == desYear)
        {
            ret = desDay - srcDay;
        }
        else
        {
            bool isSmall = (srcYear < desYear);
            int minYear = isSmall ? srcYear : desYear;
            int maxYear = isSmall ? desYear : srcYear;
            int totalDay = 0;

            for (int i = minYear; i < maxYear; ++i)
            {
                int days = isLeapYear(i) ? 366 : 365;

                totalDay += days;
            }

            if (isSmall)
            {
                totalDay = totalDay - srcDay + desDay;
            }
            else
            {
                totalDay = totalDay - desDay + srcDay;
                totalDay = -totalDay;
            }

            ret = totalDay;
        }

        return ret;
    }

    public static bool isLeapYear(int nYear)
    {
        bool bRet = false;

        if ((0 == nYear % 4 && 0 != nYear % 100) || (0 == nYear % 400))
        {
            bRet = true;
        }

        return bRet;
    }

    public static int getDayInYear(int nYear, int nMonth, int nDay)
    {
        int nRet = 0;

        switch (nMonth - 1)
        {
            case 12:
                nRet += 31;
                goto case 11;
            //need't break
            case 11:
                nRet += 30;
                goto case 10;
            case 10:
                nRet += 31;
                goto case 9;
            case 9:
                nRet += 30;
                goto case 8;
            case 8:
                nRet += 31;
                goto case 7;
            case 7:
                nRet += 31;
                goto case 6;
            case 6:
                nRet += 30;
                goto case 5;
            case 5:
                nRet += 31;
                goto case 4;
            case 4:
                nRet += 30;
                goto case 3;
            case 3:
                nRet += 31;
                goto case 2;
            case 2:
                {
                    if (isLeapYear(nYear))
                    {
                        nRet += 29;
                    }
                    else
                    {
                        nRet += 28;
                    }
                    goto case 1;
                }
            case 1:
                nRet += 31;
                break;
            default:
                break;
        }

        nRet += nDay;

        return nRet;
    }

    public int getCurDayFromStartDay()
    {
        int day = compareTheDay( _gameStartYear, getDayInYear(_gameStartYear, _gameStartMonth, _gameStartDayInMonth), getOnlineTime().Year, getOnlineTime().DayOfYear);
        return day;
    }
}
