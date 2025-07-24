using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject options;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject TimeCounterGO;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pauseScreen != null)
        {
            if (Time.timeScale == 1)
                PauseGame();
            else
                ResumeGame();
        }
    }

    public void OpenOptions()
    {
        pauseScreen.SetActive(false);
        options.SetActive(true);
    }

    public void CloseOptions()
    {
        options.SetActive(false);
        pauseScreen.SetActive(true);
    }

    public void OpenOptionsStart()
    {
        startScreen.SetActive(false);
        options.SetActive(true);
    }

    public void CloseOptionsStart()
    {
        options.SetActive(false);
        startScreen.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        options.SetActive(false);
        pauseScreen.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverScreen.SetActive(true);
    }



}
