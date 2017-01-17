using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SecceedView : MonoBehaviour {
    public GameObject _imageStars;
    public Text _score;

    public GameObject[] _bgStarObject;

    private int _curStarNum = 0;
    private List<GameObject> _arrCreateStars;

    void Awake()
    {
        _arrCreateStars = new List<GameObject>();
    }

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void showStar(int num)
    {
        StartCoroutine("CoroutineShowStars", num);
    }

    private void showStarAni(int idx)
    {
        var starobject = Instantiate(_imageStars);
        starobject.transform.SetParent(transform);
        starobject.transform.localScale = new Vector3(1f, 1f, 1f);

        starobject.transform.localPosition = _bgStarObject[idx].transform.localPosition;

        _arrCreateStars.Add(starobject);
    }

    public void setScore(int score)
    {
        _score.text = "/" + score;
    }

    public void btnOnHome()
    {
        StopAllCoroutines();

        for (int i = _arrCreateStars.Count - 1; i >= 0; i--)
        {
            Destroy(_arrCreateStars[i]);
        }
        _arrCreateStars.Clear();

        _curStarNum = 0;
        QuestionMgr.getInstance().clearInfo();
        SceneMgr.getInstance().showSelectLayer();
    }

    private IEnumerator CoroutineShowStars(int starsnum)
    {
        while (true)
        {
            if (_curStarNum < starsnum)
            {
                showStarAni(_curStarNum);
                _curStarNum++;
               
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
