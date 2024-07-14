using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource defaultSource;

    public bool pause = false;
    public bool clickPause = false;

    public AudioSource MusicSource { get { return musicSource; } }
    public AudioSource ClickSource { get { return defaultSource; } }

    public static AudioManager Instance;

    private void OnEnable()
    {
        ServiceLocator.AddService(this);
    }

    private void OnDisable()
    {
        ServiceLocator.RemoveService(this);
    }

    private void Awake()
    {
        if (Instance) Destroy(Instance.gameObject);

        Instance = this;

        DontDestroyOnLoad(gameObject);
        musicSource.Play();
    }

    public void PlaySound(AudioClip clip)
    {
        defaultSource.clip = clip;
        defaultSource.Play();
    }
}
