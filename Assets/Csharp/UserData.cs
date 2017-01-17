using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class HappyChangeEvent
{
    public const int EventWrong = -2;
    public const int EventRight = 2;
    public const int EventRemind = 1;
    public const int EventHelp = 1;
    public const int PassThreeStars = 8;
    public const int PassTwoStars = 6;
    public const int PassOneStars = 4;
    public const int FailedPass = -5;
}

public enum BabyStage
{
    BABY_XINFEN = 0,
    BABY_KAIXIN,
    BABY_NOR,
    BABY_SAD,
    BABY_CRY,
}
public class UserData{
    private static UserData _instance = null;
    private bool _isNeedSave = false;
    public static Action<int> _happinessChangeAction;
    public static Action<int> _goldChangeAction;
    private UserData() { }

    public static UserData getInstance()
    {
        if (_instance == null)
        {
            _instance = new UserData();
        }
        return _instance;
    }

    public void saveData() {
        _isNeedSave = true;
    }

    public void saveToDisk() {
        if (_isNeedSave) {
            PlayerPrefs.Save();
        }     
    }

    public void addGoldNum(int num)
    {
        int cur = PlayerPrefs.GetInt(KEY_GOLD_NUM, 0);
        cur += num;
        PlayerPrefs.SetInt(KEY_GOLD_NUM, cur);

        if (_goldChangeAction != null)
        {
            _goldChangeAction(cur);
        }
    }

    public int getDayRewardIdx()
    {
        return PlayerPrefs.GetInt(KEY_DAYREWARD_IDX, 0);
    }

    public void addDayRewardIdx()
    {
        int cur = PlayerPrefs.GetInt(KEY_DAYREWARD_IDX, 0);
        cur += 1;
        if (cur >= 7)
            cur = 0;
        PlayerPrefs.SetInt(KEY_DAYREWARD_IDX, cur);
    }

    public int getGoldNum()
    {
        return PlayerPrefs.GetInt(KEY_GOLD_NUM, 0);
    }

    public int getHappinessNum()
    {
        return PlayerPrefs.GetInt(KEY_HAPPYINESS, 40);
    }

    public void addHappinessNum(int num)
    {
        int cur = PlayerPrefs.GetInt(KEY_HAPPYINESS, 40);
        cur += num;

        if (cur < 0)
            cur = 0;
        if (cur > 100)
            cur = 100;

        PlayerPrefs.SetInt(KEY_HAPPYINESS, cur);

        if (_happinessChangeAction != null)
        {
            _happinessChangeAction(cur);
        }
    }

    public bool isFirstPlay()
    {
        int flag = PlayerPrefs.GetInt(KEY_ISFIRST_PLAY, 1);
        return flag == 0 ? false : true;
    }

    public void setFirstPlayFalse()
    {
        PlayerPrefs.SetInt(KEY_ISFIRST_PLAY, 0);
    }

    public BabyStage getCurBabyStage()
    {
        int curnum = getHappinessNum();
        BabyStage stage = BabyStage.BABY_XINFEN;
        if (curnum >= 80)
        {
            stage = BabyStage.BABY_XINFEN;
        }
        else if (curnum >= 60)
        {
            stage = BabyStage.BABY_KAIXIN;
        }
        else if (curnum >= 40)
        {
            stage = BabyStage.BABY_NOR;
        }
        else if (curnum >= 20)
        {
            stage = BabyStage.BABY_SAD;
        }
        else if (curnum >= 0)
        {
            stage = BabyStage.BABY_CRY;
        }

        return stage;
    }

    public int getLastDayInYear()
    {
        return PlayerPrefs.GetInt(UserData.KEY_LEAVE_DAYINYEAR);
    }

    public int getCurChapterIdx()
    {
        return PlayerPrefs.GetInt(UserData.KEY_CHAPTER_IDX, 0);
    }

    public void setCurChapterIdx(int idx)
    {
        PlayerPrefs.SetInt(UserData.KEY_CHAPTER_IDX, idx);
    }

    public float getSoundValue()
    {
        return PlayerPrefs.GetFloat(KEY_SOUND_VALUE, 1f);
    }

    public void setSoundValue(float value)
    {
       PlayerPrefs.SetFloat(KEY_SOUND_VALUE, value);
    }

    public float getEffectValue()
    {
        return PlayerPrefs.GetFloat(KEY_EFFECT_VALUE, 1f);
    }

    public void setEffectValue(float value)
    {
       PlayerPrefs.SetFloat(KEY_EFFECT_VALUE, value);
    }



    public static string KEY_ISFIRST_PLAY = "key_isfirst_play";
    public static string KEY_FIRSTPLAY_DAYINYEAR = "key_firstplay_dayInYear";
    public static string KEY_FIRSTPLAY_YEAR = "key_firstplay_year";

    public static string KEY_TILI_NUM = "key_tili_num";
    public static string KEY_GOLD_NUM = "key_gold_num";

    public static string KEY_LEAVE_YEAR = "key_leave_year";
    public static string KEY_LEAVE_DAYINYEAR = "key_leave_dayinyear";
    public static string KEY_LEAVE_SECINDAY = "key_leave_secinday";

    public static string KEY_HAPPYINESS = "key_player_happiness";

    public static string KEY_DAYREWARD_IDX = "key_dayreward_idx";

    public static string KEY_CHAPTER_IDX = "key_chapter_idx";

    public static string KEY_SOUND_VALUE = "key_sound_value";
    public static string KEY_EFFECT_VALUE = "key_effect_value";

}
