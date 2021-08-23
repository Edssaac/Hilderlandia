using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    public GameObject credits;
    private bool isShowing = false;

    private AudioSource music;

    public GameObject GameMusic;

    private void Start()
    {
        DontDestroyOnLoad(GameMusic);
        music = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        AudioManager.instance.buttonFX.Play();
        GameMusic.GetComponent<AudioSource>().clip = (AudioClip) AudioManager.instance.gamemusic;
        music.Stop();
        GameMusic.SetActive(true);
        SceneManager.LoadScene("lvl_00");
    }

    public void Credits()
    {
        AudioManager.instance.buttonFX.Play();
        if (isShowing)
        {
            credits.SetActive(false);
            isShowing = !isShowing;
        }
        else
        {
            credits.SetActive(true);
            isShowing = !isShowing;
        }
    }

    public void QuitGame()
    {
        AudioManager.instance.buttonFX.Play();
        music.Stop();

        #if UNITY_EDITOR
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        #else
        {
            Application.Quit();
        }
        #endif
    }

}
