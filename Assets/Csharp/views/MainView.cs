using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainView : MonoBehaviour {
    public Image _happinessBar;
    public Image _faceImage;

    public Sprite[] _faces;
    private float _startPos;
    private float _distance;

    public GameObject _removePopDialog;
    public Text _goldNum;

    public Image _textBtnShare;
    public Sprite _spriteShare;
    public Sprite _spriteHelp;
    public Image _backOrSettingImage;
    public Sprite _settingSprite;
    public Sprite _backSprite;
    void OnEnable()
    {
        UserData._happinessChangeAction += happinessChange;
        UserData._goldChangeAction += goldChange;
        SceneMgr._sceneChangeDelegate += sceneChange;
    }

    void OnDisable()
    {
        UserData._happinessChangeAction -= happinessChange;
        UserData._goldChangeAction -= goldChange;
        SceneMgr._sceneChangeDelegate -= sceneChange;

    }
	// Use this for initialization
	void Start () {
        _startPos = _happinessBar.rectTransform.localPosition.y - _happinessBar.rectTransform.rect.height/2f;
        _distance = _happinessBar.rectTransform.rect.height;
        goldChange(UserData.getInstance().getGoldNum());
        happinessChange(UserData.getInstance().getHappinessNum());
        sceneChange(SceneMgr.SceneEnum.SCENE_SELECT);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void happinessChange(int curnum)
    {
        _happinessBar.fillAmount = curnum / 100f;
        var curpos = _faceImage.rectTransform.localPosition;
        _faceImage.rectTransform.localPosition = new Vector3(curpos.x, _startPos + _distance * _happinessBar.fillAmount, curpos.z);

        updataFace();
    }

    void goldChange(int curnum)
    {
        _goldNum.text = "" + curnum;
    }

    void updataFace()
    {
        var stage = UserData.getInstance().getCurBabyStage();
        _faceImage.sprite = _faces[(int)stage];
    }

    public void popTipDialpg()
    {
        //_removePopDialog.transform.SetParent(gameObject.transform.parent);
        var temp = Instantiate(_removePopDialog);
        temp.transform.SetParent(gameObject.transform.parent);
        temp.transform.localScale = new Vector3(1f, 1f, 1f);
        temp.GetComponent<RectTransform>().offsetMax = new Vector2(5f,0f);
        temp.GetComponent<RectTransform>().offsetMin = new Vector2(-5f,0f);
        //temp.GetComponent<ComDialog>()._btnOk.onClick.AddListener(delegate ()
        //{
        //    QuestionMgr.getInstance().RemoveWrongSelect();
        //});

    }

    public void btnBack()
    {
        if (SceneMgr.getInstance()._isInQuestionView == true)
        {
            QuestionMgr.getInstance().clearInfo();
            SceneMgr.getInstance().showSelectLayer();
        }
        else
        {
            SceneMgr.getInstance().popSetting();
        }
        
    }

    public void btnShare()
    {

        SceneMgr.getInstance().popShare();
       
    }

    public void sceneChange(SceneMgr.SceneEnum scene)
    {
        if (scene == SceneMgr.SceneEnum.SCENE_QUESTION)
        {
            _textBtnShare.sprite = _spriteHelp;
            _backOrSettingImage.sprite = _backSprite;
            _backOrSettingImage.SetNativeSize();
        }
        else
        {
            _textBtnShare.sprite = _spriteShare;
            _backOrSettingImage.sprite = _settingSprite;
            _backOrSettingImage.SetNativeSize();
        }
    }
}
