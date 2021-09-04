using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    #region Singleton
    private static SFXManager _instance;
    public static SFXManager Instance => _instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);

        }

    }
    #endregion
    [SerializeField]
    /// <summary>
    /// The component that plays the music
    /// </summary>
    private AudioSource source;
    public AudioClip[] SfxSounds;


    protected virtual void Start()
    {
        // If the game starts in a menu scene, play the appropriate music
        source.volume = PlayerPrefs.GetFloat("SFXValue", 1);
    }


    public static void PlaySFX(int index)
    {
        if (_instance != null)
        {
            if (_instance.source != null)
            {
                _instance.source.Stop();
                _instance.source.clip = _instance.SfxSounds[index];
                _instance.source.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable SFXPlayer component");
        }
    }
    public void UpdateVolume(float volume)
    {
        source.volume = volume;
    }
}
