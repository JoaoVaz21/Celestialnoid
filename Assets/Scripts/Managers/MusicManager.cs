using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region Singleton
    private static MusicManager _instance;
    public static MusicManager Instance => _instance;
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
    [SerializeField]
    private  AudioClip[] levelsMusic;


    protected virtual void Start()
    {
        // If the game starts in a menu scene, play the appropriate music
        source.volume = PlayerPrefs.GetFloat("VolumeValue", 1);
        PlayMusic(0);
    }


    public static void PlayMusic( int index)
    {
        if (_instance != null)
        {
            if (_instance.source != null)
            {
                _instance.source.Stop();
                _instance.source.clip = _instance.levelsMusic[index];
                _instance.source.Play();
            }
        }
        else
        {
            Debug.LogError("Unavailable MusicPlayer component");
        }
    }
    public void UpdateVolume(float volume)
    {
        source.volume = volume;
    }
}
