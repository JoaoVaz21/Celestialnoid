using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{

    private void Start()
    {
        Screen.SetResolution(540, 960, false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        MusicManager.PlayMusic(1);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
