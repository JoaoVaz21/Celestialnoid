using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
  
    public void UpdateVolume(float volume)
    {
        PlayerPrefs.SetFloat("VolumeValue", volume);
    }
    public void UpdateSFX(float volume)
    {
        PlayerPrefs.SetFloat("SFXValue", volume);
    }
}
