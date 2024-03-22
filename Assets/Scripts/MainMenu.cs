using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuCanvas ;
    [SerializeField] private AudioManager audioManager ;

    void Awake()
    {
        if (_mainMenuCanvas == null)
        {
            return;
        }
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            _mainMenuCanvas.SetActive(false);
            return;
        }
        Time.timeScale = 0f;    
    }

    public void playButton()
    {
        Time.timeScale = 1f;
    }

    public void pauseButton()
    {
        Time.timeScale = 0f;
    }

    public void resumeButton() 
    { 
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene("SampleScene");
        audioManager.audioSource.Stop();
    }
}
