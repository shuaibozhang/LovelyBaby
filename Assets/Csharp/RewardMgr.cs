using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RewardType
{
    Reward_GOLD
}

public class RewardElement
{
    public RewardType type;
    public int count;
}

public class RewardMgr : MonoBehaviour {
    private static RewardMgr s_curRewardMgr = null;

    private List<RewardElement> _rewardArray;

    public static RewardMgr getCurRewardMgr()
    {
        Debug.Assert(s_curRewardMgr != null, "instance is null");
        return s_curRewardMgr;
    }
    void Awake()
    {
        s_curRewardMgr = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void getReward(RewardType rewardtype, int count)
    {
        switch (rewardtype)
        {
            case RewardType.Reward_GOLD:
                UserData.getInstance().addGoldNum(count);
                break;
            default:
                break;
        }
    }
}
