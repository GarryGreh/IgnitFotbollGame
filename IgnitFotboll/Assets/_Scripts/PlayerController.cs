using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float speed;
    [SerializeField]
    private float speed;
    private string swipeName;
    private Vector3 move;    
    private CharacterController characterController;

    private int currentLineMove = 0; // -1 - лева€ полоса, 0 - средн€€, 1 - права€
    private float distanceToLine = 3;

    private void OnEnable()
    {
        Swipe.swipeEvent += SwipeCheck;
    }
    private void OnDisable()
    {
        Swipe.swipeEvent -= SwipeCheck;
    }
    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {        
        if(swipeName != null)
        {
            if(swipeName == "left")
            {
                if(currentLineMove > -1)
                {
                    currentLineMove--;
                    swipeName = null;
                }
            }
            if(swipeName == "right")
            {
                if(currentLineMove < 1)
                {
                    currentLineMove++;
                    swipeName = null;
                }
            }
        }
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        
        if(currentLineMove == -1)
        {
            targetPosition += Vector3.left * distanceToLine;
        }
        else if(currentLineMove == 1)
        {
            targetPosition += Vector3.right * distanceToLine;
        }
        transform.position = targetPosition;
        //Debug.Log(currentLineMove);
       // Debug.Log(targetPosition);
        //move = new Vector3(transform.position.x, transform.position.y, 1.0f);
        //characterController.Move(move * speed * Time.deltaTime);
        //transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3, 3), 0.0f, transform.position.z);
    }
    private void FixedUpdate()
    {
        move.z = speed;
        characterController.Move(move * Time.fixedDeltaTime);
    }
    public void SwipeCheck(string _swipeName)
    {
        //Debug.Log(_swipeName);
        swipeName = _swipeName;
    }
}
