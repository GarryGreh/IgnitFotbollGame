using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        //if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        //{
        //    if (eventData.delta.x > 0)
        //    {
        //        Debug.Log(eventData.delta.normalized.x);
        //    }
        //    else
        //    {
        //        Debug.Log(eventData.delta.normalized.x);
        //    }
        //}
        //else
        //{
        //    if(eventData.delta.y > 0)
        //    {
        //        Debug.Log(eventData.delta.y);
        //    }
        //    else
        //    {
        //        Debug.Log(eventData.delta.y);
        //    }
        //}
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y))
        {
            if (eventData.delta.x > 0)
            {
                Debug.Log(eventData.delta.normalized.x);
            }
            else
            {
                Debug.Log(eventData.delta.normalized.x);
            }
        }
        else
        {
            if (eventData.delta.y > 0)
            {
                Debug.Log(eventData.delta.y);
            }
            else
            {
                Debug.Log(eventData.delta.y);
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
