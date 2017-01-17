using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RewardView : MonoBehaviour {
    public int _curDayIdx;
    public Image[] _getFlagImages;
    public Image[] _getTextImages;
    public int[] _rewrdGolds;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < _getFlagImages.Length; i++)
        {
            if (i >= UserData.getInstance().getDayRewardIdx())
            {                
                _getTextImages[i].gameObject.SetActive(false);
            }

            if (i == UserData.getInstance().getDayRewardIdx())
            {
                _getFlagImages[i].gameObject.SetActive(true);
            }
            else
            {
                _getFlagImages[i].gameObject.SetActive(false);
            }
        }

        _curDayIdx = UserData.getInstance().getDayRewardIdx();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void btnGetReward()
    {
        int curidx = UserData.getInstance().getDayRewardIdx();
        RewardMgr.getCurRewardMgr().getReward(RewardType.Reward_GOLD, _rewrdGolds[curidx]);
        UserData.getInstance().addDayRewardIdx();
        removeSelf();
    }

    public void btnCancle()
    {
        removeSelf();
    }

    void removeSelf()
    {
        Destroy(gameObject);
    }
}
