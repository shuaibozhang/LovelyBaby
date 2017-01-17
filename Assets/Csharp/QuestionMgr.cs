using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Spine.Unity;
using System.Collections.Generic;

public class QuestionMgr : MonoBehaviour
{
    public GameObject objectSeletAni;
    private static QuestionMgr _instance = null;
    private QuestionStruct _curQuestionStruct;
    private bool _isLoadFile = false;
    public Text questionText;
    public Text[] answers = new Text[4];

    private int _curQuestionIdxInFile = 0;
    private int _curFileIdx;

    private bool _bHaveSelect = false;

    private int _curRightNums = 0;

    public GameObject _playerAniMgr;

    private GameObject _rightAni;
    private GameObject _wrongAni;

    public GameObject _removeSelectLineObject;

    private List<int> _wrongSeletIdxArr;
    private List<GameObject> _lineObjects;
    private List<SingleQuestion> _trueShowQuestions;
    public static QuestionMgr getInstance(){
        return _instance;
    }
    void Awake()
    {
        _instance = this;
        _wrongSeletIdxArr = new List<int>();
        _lineObjects = new List<GameObject>();
        _trueShowQuestions = new List<SingleQuestion>();
    }

    void Start()
    {
        _rightAni = Instantiate(objectSeletAni);
        _rightAni.transform.SetParent(questionText.transform.parent);
        _rightAni.transform.localScale = new Vector3(1f, 1f, 1f);

        _wrongAni = Instantiate(objectSeletAni);
        _wrongAni.transform.SetParent(questionText.transform.parent);
        _wrongAni.transform.localScale = new Vector3(1f, 1f, 1f);

        SkeletonGraphic skeletonAnimation = _rightAni.GetComponent<SkeletonGraphic>();
        skeletonAnimation.AnimationState.Complete += AnimationState_Complete;

        SkeletonGraphic skeletonAnimation2 = _wrongAni.GetComponent<SkeletonGraphic>();
        skeletonAnimation2.AnimationState.Complete += AnimationState_Complete;

        _rightAni.SetActive(false);
        _wrongAni.SetActive(false);
    }

    public void loadQuestFromJsonFile(int fileidx) {
        var textString = Resources.Load<TextAsset>("json/lovelybaby");
        _curQuestionStruct = JsonUtility.FromJson<QuestionStruct>(textString.text);
        _curQuestionIdxInFile = 0;

        Dictionary<int, bool> dic = new Dictionary<int, bool>();

        _trueShowQuestions.Clear();
        int needCount = 10;
        int count = _curQuestionStruct.context.Length;
        for (int i = 0; i < needCount; i++)
        {
            int p = Random.Range(0, count);
            while (dic.ContainsKey(p) == true)
            {
                p = Random.Range(0, count);
            }
            dic.Add(p, true);

            _trueShowQuestions.Add(_curQuestionStruct.context[p]);

        }

        showQuestionByIdx(0);

        _curFileIdx = fileidx;
        _isLoadFile = true;
    }

    public string getQuestionByIdx(int idx) {
        if (_isLoadFile && idx >= 0 && idx < _trueShowQuestions.Count){
            return _trueShowQuestions[idx].ask;
        }
        else {
            return "";
        }
    }

    public void showQuestionByIdx(int idx)
    {
        _curQuestionIdxInFile = idx;
        questionText.text = _trueShowQuestions[idx].ask;
        answers[0].text = _trueShowQuestions[idx].a;
        answers[1].text = _trueShowQuestions[idx].b;
        answers[2].text = _trueShowQuestions[idx].c;
        answers[3].text = _trueShowQuestions[idx].d;

        _wrongSeletIdxArr.Clear();
        int rightidx = _trueShowQuestions[_curQuestionIdxInFile].trueAnsweridx;
        for (int i = 0; i < 4; i++)
        {
            (answers[i]).raycastTarget = true;
            if (i != rightidx)
            {
                _wrongSeletIdxArr.Add(i);
            }
        }
    }

    public bool showNextQuestion()
    {
        _rightAni.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "qq", false);
        _wrongAni.GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "qq", false);

        _rightAni.SetActive(false);
        _wrongAni.SetActive(false);

        _bHaveSelect = false;

        for (int i = _lineObjects.Count - 1; i >= 0; i--)
        {
            Destroy(_lineObjects[i]);
        }

        _lineObjects.Clear();

        bool haveNext = false;
        _curQuestionIdxInFile++;
        if (_curQuestionIdxInFile < _trueShowQuestions.Count)
        {
            showQuestionByIdx(_curQuestionIdxInFile);
            haveNext = true;
        }

        return haveNext;
    }

    public void playerSeletcCallBack(int selectidx)
    {
        if (_bHaveSelect)
        {
            return;
        }
        else
        {
            _bHaveSelect = true;
        }
              
        int rightidx = _trueShowQuestions[_curQuestionIdxInFile].trueAnsweridx;
        if (selectidx == rightidx)
        {
            _rightAni.SetActive(true);

            _rightAni.transform.localPosition = answers[selectidx].transform.localPosition + new Vector3(-270f, 0f, 0f);
            SkeletonGraphic skeletonAnimation = _rightAni.GetComponent<SkeletonGraphic>();
           skeletonAnimation.AnimationState.SetAnimation(0, "dui", false);
            _playerAniMgr.GetComponent<PlayerAniMgr>().playAnswerOk();

            UserData.getInstance().addHappinessNum(HappyChangeEvent.EventRight);

            _curRightNums++;
        }
        else
        {
            _wrongAni.SetActive(true);

            _wrongAni.transform.localPosition = answers[selectidx].transform.localPosition + new Vector3(-270f, 0f, 0f);
            SkeletonGraphic skeletonAnimation = _wrongAni.GetComponent<SkeletonGraphic>();
            _playerAniMgr.GetComponent<PlayerAniMgr>().playWrong();
            skeletonAnimation.AnimationState.SetAnimation(0, "cuo", false);

            UserData.getInstance().addHappinessNum(HappyChangeEvent.EventWrong);
        }          
        
    }

    private void AnimationState_Complete(Spine.AnimationState state, int trackIndex, int loopCount)
    {
        if (state.GetCurrent(0).animation.name.Equals("dui"))
        {
            SceneMgr.getInstance().popResoult();
        }
        else if (state.GetCurrent(0).animation.name.Equals("cuo"))
        {
            showRightAnswer();
        }
    }

    private void showRightAnswer()
    {
        int rightidx = _trueShowQuestions[_curQuestionIdxInFile].trueAnsweridx;
        _rightAni.SetActive(true);
        _rightAni.transform.localPosition = answers[rightidx].transform.localPosition + new Vector3(-270f, 0f, 0f);

        SkeletonGraphic skeletonAnimation = _rightAni.GetComponent<SkeletonGraphic>();
        skeletonAnimation.AnimationState.SetAnimation(0, "dui", false);
    }

    public string getCurAnswerDes()
    {
        return _trueShowQuestions[_curQuestionIdxInFile].answer;
    }

    public string getCurAnswerFrom()
    {
        return _trueShowQuestions[_curQuestionIdxInFile].from;
    }

    public void clearInfo()
    {
        _curQuestionIdxInFile = 0;
        _curRightNums = 0;
        _bHaveSelect = false;

        for (int i = _lineObjects.Count - 1; i >= 0; i--)
        {
            Destroy(_lineObjects[i]);
        }
        _lineObjects.Clear();

        _wrongSeletIdxArr.Clear();
        for (int i = 0; i < 4; i++)
        {
            (answers[i]).raycastTarget = true;           
        }

        _playerAniMgr.GetComponent<PlayerAniMgr>().updateAniState();
    }

    public void showTestResult()
    {
        bool win = false;
        if (_curRightNums >= 10)
        {
            SceneMgr.getInstance().showSucceedLayer(3, 300);
            UserData.getInstance().addGoldNum(300);
            UserData.getInstance().addHappinessNum(HappyChangeEvent.PassThreeStars);

            _playerAniMgr.GetComponent<PlayerAniMgr>().playSucceed();

            AchievementMgr.getCurAchievementMgr().addAchievementNum(AchievementType.ACHIEVE_PERFECT_PASS, 1);
            AchievementMgr.getCurAchievementMgr().addAchievementNum(AchievementType.ACHIEVE_GETSTART, 3);
            AchievementMgr.getCurAchievementMgr().addAchievementNum(AchievementType.ACHIEVE_PASS, 1);

            win = true;
        }
        else if (_curRightNums >= 9)
        {
            SceneMgr.getInstance().showSucceedLayer(2, 200);
            UserData.getInstance().addGoldNum(200);
            UserData.getInstance().addHappinessNum(HappyChangeEvent.PassTwoStars);

            _playerAniMgr.GetComponent<PlayerAniMgr>().playSucceed();

            AchievementMgr.getCurAchievementMgr().addAchievementNum(AchievementType.ACHIEVE_GETSTART, 2);
            AchievementMgr.getCurAchievementMgr().addAchievementNum(AchievementType.ACHIEVE_PASS, 1);

            win = true;
        }
        else if (_curRightNums >= 7)
        {
            SceneMgr.getInstance().showSucceedLayer(1, 100);
            UserData.getInstance().addGoldNum(100);
            UserData.getInstance().addHappinessNum(HappyChangeEvent.PassOneStars);

            _playerAniMgr.GetComponent<PlayerAniMgr>().playSucceed();

            AchievementMgr.getCurAchievementMgr().addAchievementNum(AchievementType.ACHIEVE_GETSTART, 1);
            AchievementMgr.getCurAchievementMgr().addAchievementNum(AchievementType.ACHIEVE_PASS, 1);

            win = true;
        }
        else
        {
            SceneMgr.getInstance().showFailedLayer(10 - _curRightNums);
            UserData.getInstance().addHappinessNum(HappyChangeEvent.FailedPass);

            _playerAniMgr.GetComponent<PlayerAniMgr>().playFailed();

            AchievementMgr.getCurAchievementMgr().addAchievementNum(AchievementType.ACHIEVE_FAILED_TIMES, 1);
        }

        if (win && _curFileIdx == UserData.getInstance().getCurChapterIdx())
        {
            UserData.getInstance().setCurChapterIdx(_curFileIdx + 1);
        }
    }

    public void RemoveWrongSelect()
    {
        if (_wrongSeletIdxArr.Count == 0)
            return;

        int idx = Random.Range(0, _wrongSeletIdxArr.Count);
        int removeidx = _wrongSeletIdxArr[idx];

        _wrongSeletIdxArr.RemoveAt(idx);

        var line = Instantiate(_removeSelectLineObject);
        line.transform.SetParent(questionText.transform.parent);
        line.transform.localScale = new Vector3(1f, 1f, 1f);
        line.transform.localPosition = answers[removeidx].transform.localPosition + new Vector3(-270f, 0f, 0f);
        _lineObjects.Add(line);

        (answers[removeidx]).raycastTarget = false;

        AchievementMgr.getCurAchievementMgr().addAchievementNum(AchievementType.ACHIEVE_REMIND_TIME, 1);
    }

    public int getWrongCount()
    {
        return _wrongSeletIdxArr.Count;
    }

    public bool checkCanRemoveWrong()
    {
        if (getWrongCount() > 0 && _curQuestionIdxInFile < _trueShowQuestions.Count)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
