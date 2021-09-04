using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider musicSlider;
    [SerializeField]
    private Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("VolumeValue", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXValue", 1);

    }

    public void UpdateVolume(float volume)
    {
        PlayerPrefs.SetFloat("VolumeValue", volume);
        MusicManager.Instance.UpdateVolume(volume);
    }
    public void UpdateSFX(float volume)
    {
        PlayerPrefs.SetFloat("SFXValue", volume);
        SFXManager.Instance.UpdateVolume(volume);
    }
}
