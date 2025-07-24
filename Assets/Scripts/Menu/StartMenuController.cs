using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    public void OnStartClick()
    {
        SoundEffectManager.Play("Click");
        Time.timeScale = 1;
        MusicManager.instance.PauseBackgroundMusic();
        SceneManager.LoadScene("GameEngine");
    }

    public void OnContinueClick()
    {
        SoundEffectManager.Play("Click");
        gameManager.ResumeGame();
    }

    public void OnOptionsClick()
    {
        SoundEffectManager.Play("Click");
        gameManager.OpenOptions();
    }

    public void OnOptionsBack()
    {
        SoundEffectManager.Play("Click");
        gameManager.CloseOptions();
    }

    public void OnOptionsStartClick()
    {
        SoundEffectManager.Play("Click");
        gameManager.OpenOptionsStart();
    }

    public void OnOptionsStartClose()
    {
        SoundEffectManager.Play("Click");
        gameManager.CloseOptionsStart();
    }

    public void OnMainMenuClick()
    {
        SoundEffectManager.Play("Click");
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }

    public void OnExitClick()
    {
        SoundEffectManager.Play("Click");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
