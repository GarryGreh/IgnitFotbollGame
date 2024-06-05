using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform hitPoint;
    public float rangeAttack;
    public LayerMask mask;
    [SerializeField]
    private float hitPower = 200;
    private bool isHitBonus;
    private bool isHit;

    [SerializeField] private float speed;
    private float currentSpeed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravity;
    private string swipeName;
    private Vector3 move;    
    private CharacterController characterController;
    private Animator animator;

    private Vector3 startPos;
    private Vector3 currentPos;
    private float distance;

    private bool isBonusSpeed;
    private float bonusTimer = 10;

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
        startPos = transform.position;
    }
    private void Update()
    {
        //Debug.Log(isHit);
        if (isBonusSpeed)
        {
            bonusTimer -= Time.deltaTime;
            if(bonusTimer <= 0 )
            {
                isBonusSpeed = false;
                bonusTimer = 10;
                ResetSpeed();
            }
        }

        currentPos = transform.position;
        distance = Vector3.Distance(startPos, currentPos);
        GameManager.Instance.SetDistance(distance);

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

        characterController.enabled = false;
        characterController.transform.position = new Vector3 (targetPosition.x, transform.position.y, transform.position.z);
        characterController.enabled = true;
       // move.x = targetPosition.x;
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
        isHit = true;

        Collider[] enemies = Physics.OverlapSphere(hitPoint.position, rangeAttack, mask);
        
        if (!isHitBonus)
        {
            hitPower = 200;
        }
        else
        {
            hitPower = 500;
            isHitBonus = false;
        }
        foreach(var enemy in enemies)
        {
            //Debug.Log(enemy.name);
            if (enemy.gameObject.GetComponent<Enemy>())
            {
                enemy.gameObject.GetComponent<Enemy>().GetHit(hitPower);
            }
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitPoint.position, rangeAttack);
    }
    public void HitBonus(bool _hit)
    {
        isHitBonus = _hit;
    }
    public void BonusSpeed()
    {
        currentSpeed *= 2;
        isBonusSpeed = true;
    }
    public void ResetSpeed()
    {
        currentSpeed = speed;
        isHit = false;
    }
    private void FixedUpdate()
    {
        move.y += gravity * Time.fixedDeltaTime;
        move.z = currentSpeed;
        characterController.Move(move * Time.fixedDeltaTime);
        //Debug.Log(move.x);
    }
    public void SwipeCheck(string _swipeName)
    {
        //Debug.Log(_swipeName);
        swipeName = _swipeName;
    }
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.tag == "Enemy" && !isHit)
        {
            GameManager.Instance.SubstractHeart();
           // Debug.Log("dfffefff");
        }
    }
}
