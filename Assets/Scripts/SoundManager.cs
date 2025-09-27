using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSrc;
    [SerializeField] AudioSource sfxSrc;


    [Header("Audio Clips")]
    public AudioClip music;
    public AudioClip fire;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSrc.clip = music;
        musicSrc.Play();

    }
    
        public void PlaySFX(AudioClip clip)
    {
        sfxSrc.PlayOneShot(clip);
    }

}
