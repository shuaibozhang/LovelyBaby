using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AcheveGetPop : MonoBehaviour {
    public Image _title;
    public Text _rewardNum;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void setActive()
    {
        gameObject.SetActive(false);
    }
}
