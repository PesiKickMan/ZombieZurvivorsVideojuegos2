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
    public AudioClip enemyDeath;
    public AudioClip playerDeath;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            EnsureAudioSources();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Asegurarse de que la fuente de música exista antes de usarla
        if (music != null)
        {
            EnsureMusicSource();
            musicSrc.clip = music;
            musicSrc.loop = true;
            musicSrc.playOnAwake = false;
            musicSrc.Play();
        }
    }

    // Método genérico para reproducir cualquier SFX
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        EnsureSfxSource();
        sfxSrc.PlayOneShot(clip);
    }

    // Método específico para el disparo (opcional)
    public void PlayFire()
    {
        if (fire == null) return;
        EnsureSfxSource();
        sfxSrc.PlayOneShot(fire);
    }

    void EnsureAudioSources()
    {
        EnsureMusicSource();
        EnsureSfxSource();
    }

    void EnsureMusicSource()
    {
        // la comparación "== null" también detecta objetos Unity destruidos
        if (musicSrc == null)
        {
            musicSrc = gameObject.AddComponent<AudioSource>();
            musicSrc.playOnAwake = false;
            musicSrc.loop = true;
        }
    }

    void EnsureSfxSource()
    {
        if (sfxSrc == null)
        {
            sfxSrc = gameObject.AddComponent<AudioSource>();
            sfxSrc.playOnAwake = false;
            sfxSrc.loop = false;
        }
    }
}
