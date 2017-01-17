using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections;
using System.Collections.Generic;

public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    private ScrollRect rect;
    private bool isDrag = false;
    private float targethorizontal = 0;

    public float contextWidth;
    public int pageCount = 1;
    public float smooting = 4f;
    public float pageWidth;
    public float autoScrollPercent = 0.5f;

    public float dragStartPos = 0;

    private int curPageIdx = 0;

    public Sprite spriteTipOn;
    public Sprite spriteTipOff;
    public List<Image> tipImages;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPos = rect.horizontalNormalizedPosition;
        isDrag = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (pageCount > 1)
        {
            float posX = rect.horizontalNormalizedPosition;
            int passpage = (int)((posX - dragStartPos) / pageWidth);

            curPageIdx += passpage;
            float off = Mathf.Abs((posX - dragStartPos) % pageWidth);

            if (posX > dragStartPos)
            {
                if (off >= autoScrollPercent)
                {
                    curPageIdx++;
                }

            }
            else
            {
                if (off >= autoScrollPercent)
                {
                    curPageIdx--;
                }
            }

            if (curPageIdx < 0)
            {
                curPageIdx = 0;
            }
            else if (curPageIdx >= pageCount)
            {
                curPageIdx = pageCount - 1;
            }

            targethorizontal = curPageIdx * (1.0f / (pageCount - 1));
        }
        else
        {
            targethorizontal = 0.0f;
        }

        for (int i = 0; i < tipImages.Count; i++)
        {
            if (i == curPageIdx)
            {
                tipImages[i].sprite = spriteTipOn;
            }
            else
            {
                tipImages[i].sprite = spriteTipOff;
            }
        }
        

        isDrag = false;

    }

    // Use this for initialization
    void Start()
    {
        rect = transform.GetComponent<ScrollRect>();
        contextWidth = rect.content.rect.width;
        pageWidth = contextWidth / pageCount;
        if (pageCount > 1)
        {
            autoScrollPercent = autoScrollPercent * (1.0f / (pageCount - 1));
        }
        
    }

    void Update()
    {
        if (!isDrag)
        {
            rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, targethorizontal, Time.deltaTime * smooting);
        }
    }

}
