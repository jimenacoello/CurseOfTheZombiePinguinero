using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAudioManager : MonoBehaviour
{
    public static MenuAudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource fondoMenuSource;
    public AudioSource botonSource;

    [Header("Audio Clips")]
    public AudioClip fondoMenuClip;
    public AudioClip botonClip;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        PlayMenuMusic();
    }

    public void PlayMenuMusic()
    {
        if (!fondoMenuSource.isPlaying)
        {
            fondoMenuSource.clip = fondoMenuClip;
            fondoMenuSource.loop = true;
            fondoMenuSource.Play();
        }
    }

    public void PlayBoton()
    {
        if (botonClip != null)
        {
            botonSource.PlayOneShot(botonClip);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 0)
        {
            Destroy(this.gameObject);
        }
    }
}
