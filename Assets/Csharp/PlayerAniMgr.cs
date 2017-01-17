using UnityEngine;
using System.Collections;
using Spine.Unity;

[System.Serializable]
public class AniStruct
{
    [SpineAnimation]
    public string[] aniName;
    public int happinessMin;
    public int happinessMax;
}

public class PlayerAniMgr : MonoBehaviour {

    [Header("Ani Config")]
    public AniStruct[] _aniConfig;
    public string _lastAnimationName;
    SkeletonGraphic skeletonAnimation;

    private bool _needKeepLastAni = false;
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonGraphic>();
        skeletonAnimation.AnimationState.Complete += AnimationState_Complete;
        updateAniState();     
    }

    private void AnimationState_Complete(Spine.AnimationState state, int trackIndex, int loopCount)
    {
        if (_needKeepLastAni)
            return;   
        updateAniState();                   
    }

    // Update is called once per frame
    void Update () {
	
	}

    public void updateAniState()
    {
        _needKeepLastAni = false;

        if (SceneMgr.getInstance()._isInQuestionView)
        {
            skeletonAnimation.AnimationState.SetAnimation(0, "daiji", true);
            return;
        }

        int cur = UserData.getInstance().getHappinessNum();
        for (int i = 0; i < _aniConfig.Length; i++)
        {
            var config = _aniConfig[i];

            if (cur < config.happinessMax && cur >= config.happinessMin)
            {
                int aniidx = Random.Range(0, config.aniName.Length);
                _lastAnimationName = config.aniName[aniidx];
                skeletonAnimation.AnimationState.SetAnimation(0, config.aniName[aniidx], true);
                return;
            }
        }
    }

    public void playExtAni(string name, bool loop = false)
    {
        skeletonAnimation.AnimationState.SetAnimation(0, name, loop);      
    }

    public void playAnswerOk()
    {
        if (Random.Range(0, 100) < 50)
            playExtAni("zhengque");
        else
            playExtAni("guzhang");
        
    }

    public void playWrong()
    {
        if (Random.Range(0, 100) < 50)
            playExtAni("cuowu");
        else
            playExtAni("shengqi");
        
    }

    public void playSucceed()
    {      
        playExtAni("shengli", true);     
        _needKeepLastAni = true;
    }

    public void playFailed()
    {       
        playExtAni("shibai", false);     
        _needKeepLastAni = true;
    }

    public void playLeft()
    {
        playExtAni("dianjizuo");
    }

    public void playRight()
    {
        playExtAni("dianjiyou");
    }
}
