using UnityEngine;
using System.Collections;

public class SceneMgr : MonoBehaviour {

    public enum SceneEnum
    {
        SCENE_SELECT,
        SCENE_QUESTION,
        SCENE_WIN,
        SCENE_FAILED,
    }
    private static SceneMgr s_instance = null;

    public GameObject selectLayer;
    public GameObject questionLayer;
    public GameObject failedLayer;
    public GameObject popNextLayer;
    public GameObject succeedLayer;
    public GameObject achieveView;
    public GameObject dayRewardView;
    public GameObject acheiveGetPop;
    public GameObject settingRoot;
    public GameObject shareRoot;

    private bool _isPopDayReward = false;

    public bool _isInQuestionView = false;

    public delegate void SceneChangeDelegate(SceneEnum scene);
    public static event SceneChangeDelegate _sceneChangeDelegate;
    public static SceneMgr getInstance() {
        return s_instance;
    }

    void OnEnable()
    {
        TimeMgr._timeGet += OnTimeGet;
    }

    void OnDisable()
    {
        TimeMgr._timeGet -= OnTimeGet;
    }

    void Awake() {
        s_instance = this;
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

   public void showSelectLayer()
    {
        selectLayer.SetActive(true);
        questionLayer.SetActive(false);
        failedLayer.SetActive(false);
        popNextLayer.SetActive(false);
        succeedLayer.SetActive(false);

        _isInQuestionView = false;

        if (_sceneChangeDelegate != null)
        {
            _sceneChangeDelegate(SceneEnum.SCENE_SELECT);
        }

        selectLayer.GetComponent<SelectView>().checkShowUnlockAni(1f);
    }

   public void showQuestionLayer()
    {
        //var animator = selectLayer.GetComponent<Animator>();
        //animator.SetBool("bShowQuestion", true);

        selectLayer.SetActive(false);
        questionLayer.SetActive(true);
        failedLayer.SetActive(false);
        popNextLayer.SetActive(false);
        succeedLayer.SetActive(false);

        _isInQuestionView = true;

        if (_sceneChangeDelegate != null)
        {
            _sceneChangeDelegate(SceneEnum.SCENE_QUESTION);
        }
    }

    public void showFailedLayer(int wrongnum)
    {       
        selectLayer.SetActive(false);
        questionLayer.SetActive(false);
        failedLayer.SetActive(true);
        popNextLayer.SetActive(false);
        succeedLayer.SetActive(false);

        failedLayer.GetComponent<FailedView>().setWrongNum(wrongnum);

        if (_sceneChangeDelegate != null)
        {
            _sceneChangeDelegate(SceneEnum.SCENE_FAILED);
        }
    }

    public void showSucceedLayer(int starsnum, int score)
    {
        selectLayer.SetActive(false);
        questionLayer.SetActive(false);
        failedLayer.SetActive(false);
        popNextLayer.SetActive(false);
        succeedLayer.SetActive(true);

        succeedLayer.GetComponent<SecceedView>().setScore(score);
        succeedLayer.GetComponent<SecceedView>().showStar(starsnum);

        if (_sceneChangeDelegate != null)
        {
            _sceneChangeDelegate(SceneEnum.SCENE_WIN);
        }
    }

    public void popResoult()
    {
        popNextLayer.SetActive(true);
        popNextLayer.GetComponent<NextView>().reSetDes();
    }

    public void popSetting()
    {
        settingRoot.SetActive(true);       
    }

    public void popShare()
    {
        shareRoot.SetActive(true);
    }

    public void popAchieveView()
    {
        var view = Instantiate(achieveView);
        view.transform.SetParent(selectLayer.transform.parent);
        view.transform.localScale = new Vector3(1f, 1f, 1f);
        view.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        view.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        //view.GetComponent<RectTransform>().SetSiblingIndex(0);
    }

    public void hideNext()
    {
        popNextLayer.SetActive(false);
    }

    void OnTimeGet(System.DateTime time)
    {
        if (_isPopDayReward == false && UserData.getInstance().getLastDayInYear() != time.DayOfYear)
        {
            if (UserData.getInstance().getDayRewardIdx() < 7)
            {
                var view = Instantiate(dayRewardView);
                view.transform.SetParent(selectLayer.transform.parent);
                view.transform.localScale = new Vector3(1f, 1f, 1f);
                view.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
                view.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
                _isPopDayReward = true;
            }
        }
    }

    public void popAcheiveGet(Sprite image, int rewardNum)
    {
        acheiveGetPop.gameObject.SetActive(true);
        acheiveGetPop.GetComponent<Animator>().Play("achieveget");
        acheiveGetPop.GetComponent<AcheveGetPop>()._rewardNum.text = "" + rewardNum;
        acheiveGetPop.GetComponent<AcheveGetPop>()._title.sprite = image;
        acheiveGetPop.GetComponent<AcheveGetPop>()._title.SetNativeSize();
        acheiveGetPop.GetComponent<RectTransform>().SetSiblingIndex(100);
    }
}
