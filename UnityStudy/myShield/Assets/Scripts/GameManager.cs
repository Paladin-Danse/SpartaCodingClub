using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject square;
    [SerializeField] GameObject endPanel;
    [SerializeField] Text thisScoreTxt;
    [SerializeField] Text maxScoreTxt;
    [SerializeField] Text timeTxt;
    [SerializeField] Animator balloon_anim;
    
    float alive = 0f;
    float bestScore;
    bool isRunning = true;
    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        InitGame();
        InvokeRepeating("makeSquare", 0.0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunning)
        {
            alive += Time.deltaTime;
            timeTxt.text = alive.ToString("N2");
        }
    }

    public void makeSquare()
    {
        Instantiate(square);
    }
    public void GameOver()
    {
        isRunning = false;
        balloon_anim.SetBool("isDie", true);
        Invoke("timeStop", 0.5f);

        endPanel.SetActive(true);
        thisScoreTxt.text = alive.ToString("N2");
        if(bestScore < alive)
        {
            PlayerPrefs.SetFloat("bestScore", alive);
            bestScore = alive;
        }
        maxScoreTxt.text = bestScore.ToString("N2");
    }
    public void InitGame()
    {
        Time.timeScale = 1f;
        balloon_anim.SetBool("isDie", false);
        if (PlayerPrefs.HasKey("bestScore"))
            bestScore = PlayerPrefs.GetFloat("bestScore");
        else
            bestScore = 0f;
    }
    public void retryGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void timeStop()
    {
        Time.timeScale = 0f;
    }
}
