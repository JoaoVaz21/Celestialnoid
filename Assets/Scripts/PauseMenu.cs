using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Menu;
    // Start is called before the first frame update
    public void Pause(bool pause)
    {
        Menu.SetActive(true);
        Time.timeScale = 0;
    }
    public void UnPause(bool pause)
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }
}
