using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update
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
