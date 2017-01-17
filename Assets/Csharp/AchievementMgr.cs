using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AchievementType
{
    ACHIEVE_GETSTART,
    ACHIEVE_PASS,
    ACHIEVE_PERFECT_PASS,
    ACHIEVE_FAILED_TIMES,
    ACHIEVE_HELP_TIMES,
    ACHIEVE_REMIND_TIME,
    ACHIEVE_CHAPTER_PASS
}

public enum AchievementStage
{
    ACHIEVESTAGE_UNFINISH,
    ACHIEVESTAGE_FINISHED_UNREWARD,
    ACHIEVESTAGE_REWARDED
}

[System.Serializable]
public class Achievement
{
    public int id;
    public string title;
    public string des;
    public AchievementType type;
    public int typeConfig;
    public RewardType reardType;
    public int rewardCount;
    public int compliteday;
}
public class AchievementMgr : MonoBehaviour {

    public List<Achievement> _achievementArray;
    public GameObject _rewardMgrObject;
    private RewardMgr _rewardMgr;
    public const string KEY_ACHIEVENT_TYPE_NUM = "key_achieve_type_num";
    public const string KEY_ACHIEVENT_REWARD = "key_achieve_reward";
    public const string KEY_ACHIEVENT_GET_DAY = "key_achieve_getday";

    static AchievementMgr _instance = null;
    public static AchievementMgr getCurAchievementMgr()
    {
        Debug.Assert(_instance != null, "instance is null");
        return _instance;
    }

    void Awake() {
        _instance = this;
        _rewardMgr = _rewardMgrObject.GetComponent<RewardMgr>();
        for (int i = 0; i < _achievementArray.Count; i++)
        {
           _achievementArray[i].compliteday = PlayerPrefs.GetInt(KEY_ACHIEVENT_GET_DAY + _achievementArray[i].id, -1);
        }

    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Insertion Sort
    public List<Achievement> getSortList()
    {
        var sortlist = new List<Achievement>();

        int curidx = 1;
        sortlist.Add(_achievementArray[0]);
       
        while (curidx < _achievementArray.Count)
        {
            var curtemp = _achievementArray[curidx];
            int sortlistidx = 0;
            bool findPos = false;

            while (findPos == false)
            {
                if (sortlistidx == sortlist.Count || curtemp.compliteday < sortlist[sortlistidx].compliteday)
                {
                    sortlist.Insert(sortlistidx, curtemp);
                    findPos = true;
                }
                else
                {
                    sortlistidx++;
                }
            }
            curidx++;
        }

        return sortlist;
    }

    public void addAchievementNum(AchievementType type, int count)
    {
        int num = PlayerPrefs.GetInt(KEY_ACHIEVENT_TYPE_NUM + type, 0);
        num += count;
        PlayerPrefs.SetInt(KEY_ACHIEVENT_TYPE_NUM + type, num);

        for (int i = 0; i < _achievementArray.Count; i++)
        {
            if (_achievementArray[i].type == type && _achievementArray[i].typeConfig <= num && PlayerPrefs.GetInt(KEY_ACHIEVENT_GET_DAY + _achievementArray[i].id, -1) == -1)
            {
                PlayerPrefs.SetInt(KEY_ACHIEVENT_GET_DAY + _achievementArray[i].id, TimeMgr.getCurTimeMgr().getCurDayFromStartDay());
                _achievementArray[i].compliteday = TimeMgr.getCurTimeMgr().getCurDayFromStartDay();
                getRewardById(_achievementArray[i].id);
            }
        }
    }

    int getAchievementNum(AchievementType type)
    {
       int num = PlayerPrefs.GetInt(KEY_ACHIEVENT_TYPE_NUM + type, 0);
       return num;
    }


    bool getRewardById(int id)
    {
        Achievement temp = null;
        for (int idx = 0; idx < _achievementArray.Count; idx++)
        {
            if (_achievementArray[idx].id == id)
            {
                temp = _achievementArray[idx];
                break;
            }
        }

        if (PlayerPrefs.GetInt(KEY_ACHIEVENT_REWARD + temp.id, 0) != 0 || temp == null)
        {
            return false;
        }
        else
        {
            if (_rewardMgr)
            {
                _rewardMgr.getReward(temp.reardType, temp.rewardCount);
                var sprite = Resources.Load<Sprite>("ui/achieve/achieve_name_" + temp.id);
                SceneMgr.getInstance().popAcheiveGet(sprite, temp.rewardCount);
            }
            else
            {
                Debug.Break();
            }
            return true;
        }

    }

    AchievementStage getAchieveStageById(int id)
    {
        Achievement temp = null;
        AchievementStage stage = AchievementStage.ACHIEVESTAGE_UNFINISH;
        for (int idx = 0; idx < _achievementArray.Count; idx++)
        {
            if (_achievementArray[idx].id == id)
            {
                temp = _achievementArray[idx];
                break;
            }
        }

        if (temp != null)
        {
            if (getAchievementNum(temp.type) >= temp.typeConfig)
            {
                stage = AchievementStage.ACHIEVESTAGE_FINISHED_UNREWARD;
                if (PlayerPrefs.GetInt(KEY_ACHIEVENT_REWARD + temp.id, 0) != 0)
                {
                    stage = AchievementStage.ACHIEVESTAGE_REWARDED;
                }
                else
                {

                }
            }
            else
            {

            }


        }

        return stage;
    }
}
