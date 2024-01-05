using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i_GameManager;
    [SerializeField] GameObject rain;
    [SerializeField] GameObject panel;
    public Text scoreText;
    public Text timeText;
    int totalScore;
    [SerializeField] float limit = 5.0f;
    void Awake()
    {
        i_GameManager = this;
    }
    void Start()
    {
        initGame();
        InvokeRepeating("makeRain", 0, 0.5f);
    }
    private void Update()
    {
        limit -= Time.deltaTime;
        if(limit < 0)
        {
            Time.timeScale = 0.0f;
            panel.SetActive(true);
            limit = 0.0f;
        }
        timeText.text = limit.ToString("N2");
    }

    public void makeRain()
    {
        Instantiate(rain);
    }

    public void addScore(int score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    public void retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    void initGame()
    {
        Time.timeScale = 1.0f;
        totalScore = 0;
        limit = 30.0f;
    }
}
