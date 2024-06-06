using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI heartsText;

    public GameObject gameOverPanel;
    public TextMeshProUGUI disOverText;

    private float distance;
    private int coins;
    private int maxHearts = 3;
    private int currentHeart;

    private void Start()
    {
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(false);
        Instance = this;
        currentHeart = maxHearts;
    }
    public void SetDistance(float _distance)
    {
        distance = _distance / 5;
        distanceText.text = distance.ToString("f0") + " m";
    }
    public void AddCoins()
    {
        coins++;
        coinsText.text = coins.ToString();
    }
    public void AddHeart()
    {
        if(currentHeart < maxHearts)
        {
            currentHeart++;
            heartsText.text = currentHeart.ToString();
        }
    }
    public void SubstractHeart()
    {
        currentHeart--;
        if (currentHeart > 0)        {
            
            heartsText.text = currentHeart.ToString();
        }
        else if(currentHeart <= 0)
        {
            //конец игры
            //пройденная дистанция
            GameOver();
        }
    }
    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOverPanel.SetActive(true);
        disOverText.text = "Distance: " + distance.ToString("f0") + " m";
    }
}
