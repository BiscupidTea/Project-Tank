using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Fix - Unclear name
public class Sound : MonoBehaviour
{
    [SerializeField] AudioClip music;
    void Start()
    {
        SoundManager.Instance.PlayMusic(music);
    }

    //TODO: TP2 - Remove unused methods/variables/classes
    void Update()
    {
        
    }
}
