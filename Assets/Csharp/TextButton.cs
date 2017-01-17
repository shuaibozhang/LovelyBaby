using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;

public class TextButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private bool bScaled = false;
    private bool bSelect = false;
    public int id;
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale -= new Vector3(0.02f, 0.1f, 0);
        bScaled = true;

        bSelect = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (bScaled)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            bScaled = false;
        }

        bSelect = false;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (bScaled)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            bScaled = false;
        }

        if (bSelect)
        {
            Debug.Log("point up");
            BtnRouter.getInstance().btnOnAnswerByIdx(id);
        }

        bSelect = false;
        
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
