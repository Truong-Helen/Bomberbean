using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIController : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject bombIndicators;
    public GameObject gameOver;
    private bool isPaused;

    [Header("Bomb UI")]
    public TextMeshProUGUI count;
    public float maxWidth = 500;
    public Image cooldownBar;
    private RectTransform barSize;

    [Header ("Player UI")]
    public Player player1;
    public TextMeshProUGUI livesUI;

    [Header ("Keys UI")]
    public TextMeshProUGUI keysUI;

    [Header ("Game Win")]
    public GameObject winScreen;

    [Header ("Timer UI")]
    float currentTime;
    public int startMinutes;
    public TextMeshProUGUI currentTimeText;

    [Header ("Audio")]
    public AudioSource loseSound;
    public AudioSource winSound;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        gameOver.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
        barSize = cooldownBar.rectTransform;
        CooldownBar(0);

        currentTime = startMinutes * 60;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                UnpauseGame();
            else   
                PauseGame();
        }
        currentTime -= Time.deltaTime;
        
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        if (time.Seconds == 0 && time.Minutes == 0)
        {
            GameOver();
        }
        currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();

        
       
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        bombIndicators.SetActive(false);
        isPaused = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        bombIndicators.SetActive(true);
        isPaused = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
        loseSound.Play();
    }

    public void GameWin()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
        winSound.Play();
    }

     public void CooldownBar(float percentOfSize)
    {
        barSize.sizeDelta = new Vector2(maxWidth * percentOfSize, barSize.sizeDelta.y);
    }

    public void UpdateBomb(int newCount)
    {
        count.text = "" + newCount;
    }

    public void UpdatePlayer()
    {
        livesUI.text = "Lives: " + player1.numLives;   
    }

    public void UpdateKeys()
    {
        keysUI.text = "Keys: " + player1.numKeys;
    }

}
