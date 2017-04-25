using System.Collections;
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

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        gameOver = false;
       if (!onMenu)
            pausePanel.SetActive(false);
        pausedGame = false;
    }
	
	// Update is called once per frame
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
}
