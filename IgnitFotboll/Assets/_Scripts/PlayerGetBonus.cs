using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetBonus : MonoBehaviour
{
    public GameObject[] playerPart;

    private PlayerController playerController;

    private float invisibleTimer = 7.0f;
    private bool isStopInvisibleTimer  = true;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    public void Coin()
    {
        GameManager.Instance.AddCoins();
    }
    public void Heart()
    {
        GameManager.Instance.AddHeart();
    }
    public void Invisibility(bool _visible)
    {
        isStopInvisibleTimer = _visible;
        foreach (var part in playerPart)
        {
            part.SetActive(_visible);
        }
    }
    public void PowerHit()
    {
        playerController.HitBonus(true);
    }
    public void IncreaseSpeed()
    {
        playerController.BonusSpeed();
    }
    private void Update()
    {
        if (!isStopInvisibleTimer)
        {
            invisibleTimer -= Time.deltaTime;
            if(invisibleTimer <= 0.0f)
            {
                invisibleTimer = 7.0f;
                Invisibility(true);
            }
        }
    }
}
