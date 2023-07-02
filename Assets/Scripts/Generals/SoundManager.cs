using UnityEngine;

/// <summary>
/// Sound Manage, you can give it a sound or a song and it will play it
/// </summary>
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    public AudioSource MusicSource { get => musicSource; set => musicSource = value; }

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
        effectSource.PlayOneShot(clip);
    }
    /// <summary>
    /// Play the music inserted
    /// </summary>
    /// <param name="clip"></param>
    public void PlayMusic(AudioClip clip)
    {
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
        effectSource.mute = !effectSource.mute;
        MusicSource.mute = !MusicSource.mute;
    }
}
