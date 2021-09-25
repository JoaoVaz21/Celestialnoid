using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : OptionsMenu
{
    public GameObject Menu;
    // Start is called before the first frame update
    public void Pause(bool pause)
    {
        GameManager.Instance.IsGamePaused = true;
        Menu.SetActive(true);
        Time.timeScale = 0;
    }
    public void UnPause(bool pause)
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
        GameManager.Instance.UnpauseGame();

    }
    public void Home()
    {
        SceneManager.LoadScene(0);
        MusicManager.PlayMusic(0);
        Time.timeScale = 0;
    }
}
