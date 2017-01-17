using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class PlayerTouch : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public GameObject _playerAni;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        
        Debug.Log(eventData.position.x - Screen.width / 2f);
        if (eventData.position.x - Screen.width / 2f < 0)
        {
            _playerAni.GetComponent<PlayerAniMgr>().playLeft();
        }
        else
        {
            _playerAni.GetComponent<PlayerAniMgr>().playRight();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        

    }

    public void OnPointerUp(PointerEventData eventData)
    {

       
    }
}
