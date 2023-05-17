using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioClip music;
    void Start()
    {
        SoundManager.Instance.PlayMusic(music);
    }

    void Update()
    {
        
    }
}
