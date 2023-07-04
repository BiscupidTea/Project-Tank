using UnityEngine;

/// <summary>
/// Sound Manage, you can give it a sound or a song and it will play it
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;
    [SerializeField] private float currentVolumeMusic;
    [SerializeField] private float currentVolumeSfx;

    public AudioSource MusicSource { get => musicSource; set => musicSource = value; }
    public AudioSource EffectSource { get => effectSource; set => effectSource = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Play the audio clip inserted
    /// </summary>
    /// <param name="clip"></param>
    public void PlaySound(AudioClip clip)
    {
        EffectSource.volume = currentVolumeSfx;
        EffectSource.PlayOneShot(clip);
    }
    /// <summary>
    /// Play the music inserted
    /// </summary>
    /// <param name="clip"></param>
    public void PlayMusic(AudioClip clip)
    {
        MusicSource.volume = currentVolumeMusic;
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    /// <summary>
    /// Stop music that is playing
    /// </summary>
    public void StopMusic()
    {
        MusicSource.Stop();
    }

    /// <summary>
    /// change audio state off/on
    /// </summary>
    public void ToggleAudio()
    {
        EffectSource.mute = !EffectSource.mute;
        MusicSource.mute = !MusicSource.mute;
    }

    /// <summary>
    /// adds the entered value to the current SFX volume, if it passes 10(max) or 0(min) it will return to the nearest number
    /// </summary>
    /// <param name="Value"></param>
    public void ChangeVolumeSFXSlider(float Value)
    {
        currentVolumeSfx = Value;
        effectSource.volume = currentVolumeSfx;
    }

    /// <summary>
    /// adds the entered value to the current MUSIC volume, if it passes 10(max) or 0(min) it will return to the nearest number
    /// </summary>
    /// <param name="Value"></param>
    public void ChangeVolumeMusicSlider(float Value)
    {
        currentVolumeMusic = Value;
        musicSource.volume = currentVolumeMusic;
    }
}
