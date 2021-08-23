using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static int totalScore;
    public int score;
    public Text scoreText;

    public Text timeText;
    private float timer=0;
    
    private bool isGameOver = false;
    private bool isPaused = false;

    public static GameController instance;
    public GameObject gameOverPanel;
    public GameObject gamePausePanel;

    public GameObject gameJoystick;

    // Start is called before the first frame update
    void Start()
    {
        if ( SceneManager.GetActiveScene().name == "lvl_06" )
        {
            GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().clip = (AudioClip) AudioManager.instance.bossfight;
            GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().Play();        
        }

        instance = this;
        UpdateScore();
    }

    void Update()
    {
        if ( !isGameOver )
        {
            timeText.text = Mathf.Abs(Mathf.RoundToInt(timer)).ToString("D4");
            timer -= Time.deltaTime;

            if ( Input.GetKeyDown(KeyCode.Escape) )
            {
                PauseGame();
            }
        }
    }

    public void UpdateScore()
    {
        scoreText.text = $"Score: {totalScore+score}";
    }

    public void UpdateFinalScore()
    {
        totalScore += score;
    }

    public void ShowGameOver()
    {
        isGameOver = true;

        if ( SceneManager.GetActiveScene().name != "lvl_06" )
            GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().Pause();
        else
            GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().Stop();

        AudioManager.instance.deathFX.Play();
        gameJoystick.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        AudioManager.instance.deathFX.Stop();

        if ( SceneManager.GetActiveScene().name != "lvl_06" )
            GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().UnPause();
        else
            GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().Play();

        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }

    public void PauseGame()
    {
        AudioManager.instance.buttonFX.Play();

        if (!isPaused)
        {
            Time.timeScale = 0;
            isPaused = !isPaused;
            gameJoystick.SetActive(false);

            GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().Pause();
            
            gamePausePanel.SetActive(isPaused);
        }
        else
        {
            Time.timeScale = 1;
            isPaused = !isPaused;
            gameJoystick.SetActive(true);

            GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().UnPause();
            
            gamePausePanel.SetActive(isPaused);
        }
    }

    public void Menu()
    {
        PauseGame();
        Destroy(GameObject.FindWithTag("GameMusic"));
        totalScore = 0;
        SceneManager.LoadScene("_Menu");
    }
    
    public void UIButtonPause()
    {
        GameController.instance.PauseGame();
    }

    public void Continue()
    {
        GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().clip = (AudioClip) AudioManager.instance.gamemusic;
        GameObject.FindWithTag("GameMusic").GetComponent<AudioSource>().Play();
        GameController.instance.UpdateFinalScore();
        Time.timeScale = 1;
        SceneManager.LoadScene( "lvl_00" );
    }

}


