using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform hitPoint;
    public float rangeAttack;
    public LayerMask mask;
    [SerializeField]
    private float hitPower = 30;

    [SerializeField] private float speed;
    private float currentSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    private string swipeName;
    private Vector3 move;    
    private CharacterController characterController;
    private Animator animator;

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
        currentSpeed = speed;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
            if(swipeName == "up")
            {
                if (characterController.isGrounded)
                {
                    Jump();
                    swipeName = null;
                }
            }
            if(swipeName == "down")
            {
                Punch();
                swipeName = null;
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
    private void Jump()
    {
        move.y = jumpPower;
        animator.SetTrigger("jump");
    }
    private void Punch()
    {
        animator.SetTrigger("punch");
        currentSpeed *= 2;
    }
    public void Hit()
    {
        Collider[] enemies = Physics.OverlapSphere(hitPoint.position, rangeAttack, mask);
        
        foreach(var enemy in enemies)
        {
            Debug.Log(enemy.name);
            enemy.gameObject.GetComponent<Enemy>().GetHit(hitPower);
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPoint.position, rangeAttack);
    }
    public void ResetSpeed()
    {
        currentSpeed = speed;
    }
    private void FixedUpdate()
    {
        move.y += gravity * Time.fixedDeltaTime;
        move.z = currentSpeed;
        characterController.Move(move * Time.fixedDeltaTime);
    }
    public void SwipeCheck(string _swipeName)
    {
        //Debug.Log(_swipeName);
        swipeName = _swipeName;
    }
}
