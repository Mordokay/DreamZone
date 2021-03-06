﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject pausePanel;
    public GameObject gameOverPanel;

    public bool pausedGame;
    public bool onMenu;
    public bool gameOver;

    public Text mySeed;

    GameObject player;

    public Sprite soundOnSprite;
    public Sprite soundOfSprite;
    public GameObject muteSprite;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        gameOver = false;
       if (!onMenu)
            pausePanel.SetActive(false);
        pausedGame = false;
    }
	
    public void ToggleMute()
    {
        if(AudioListener.volume == 0)
        {
            AudioListener.volume = 1.0f;
            muteSprite.GetComponent<Image>().sprite = soundOnSprite;
        }
        else
        {
            AudioListener.volume = 0;
            muteSprite.GetComponent<Image>().sprite = soundOfSprite;
        } 
    }
   
    void Update () {
        if (pausedGame || onMenu || gameOver)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !onMenu && !gameOver)
        {
            if (pausePanel.activeSelf)
            {
                pausePanel.SetActive(false);
                pausedGame = false;
            }
            else
            {
                pausePanel.SetActive(true);
                pausedGame = true;
            }
        }
	}
    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void CreateWorld()
    {
        PlayerPrefs.SetInt("seed", int.Parse(mySeed.text));
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("highscore", player.GetComponent<PlayerStats>().highscore);

        gameOverPanel.SetActive(true);
        gameOver = true;
    }
    public void Continue()
    {
        pausePanel.SetActive(false);
        pausedGame = false;
    }
}
