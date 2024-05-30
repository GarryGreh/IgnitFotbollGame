using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    public delegate void OnSwipe(string swipeName);
    public static event OnSwipe swipeEvent;

    private bool isTap, isLeft, isRight, isUp, isDown;

    private bool isMobile;
    private bool isSwipe;
    private Vector2 tapPosition;
    private Vector2 swipeDelta;
    public float swipeTrue = 70;

    private void Start()
    {
        isMobile = Application.isMobilePlatform;
    }
    private void Update()
    {
         isTap = isLeft = isRight = isUp = isDown = false;
        
        if (!isMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSwipe = true;
                tapPosition = Input.mousePosition;
                //isTap = true;
            }
            else if (Input.GetMouseButtonUp(0))
            {              
                SwipeReset();
            }
        }
        else
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    isSwipe = true;
                    tapPosition = Input.GetTouch(0).position;
                   // isTap = true;
                }
                else if(Input.GetTouch(0).phase == TouchPhase.Canceled ||
                    Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    SwipeReset();
                }
            }
        }
        SwipeCheck();
    }
    private void SwipeReset()
    {
        isTap = isLeft = isRight = isUp = isDown = false;
        isSwipe = false;

        tapPosition = Vector2.zero;
        swipeDelta = Vector2.zero;
    }
    private void SwipeCheck()
    {
        swipeDelta = Vector2.zero;

        if (isSwipe)
        {
            if(!isMobile && Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - tapPosition;
            }
            else if(Input.touchCount > 0)
            {
                swipeDelta = Input.GetTouch(0).position - tapPosition;
            }           
        }
        if(swipeDelta.magnitude > swipeTrue)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if(swipeEvent != null)
            {
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if(x > 0)
                    {
                        isRight = true;                        
                    }
                    else
                    {
                        isLeft = true;
                    }
                }
                else
                {
                    if(y > 0)
                    {
                        isUp = true;
                    }
                    else
                    {
                        isDown = true;
                    }
                }
            }
            isTap = false;
            swipeEvent(CheckNameSwipe());
            SwipeReset();
        }        
    }
    private string CheckNameSwipe()
    {
        string nameSwipe = null;
        if (isTap)
        {
            nameSwipe = "tap";
        }
        else if (isLeft)
        {
            nameSwipe = "left";
        }
        else if (isRight)
        {
            nameSwipe = "right";
        }
        else if (isUp)
        {
            nameSwipe = "up";
        }
        else if (isDown)
        {
            nameSwipe = "down";
        }
        return nameSwipe;
    }
}
