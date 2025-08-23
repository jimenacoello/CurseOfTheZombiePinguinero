using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource sonidoDeAmbiente;
    public AudioSource sonidoDePersecusion;
    public AudioSource sonidoPasosJugador;
    public AudioSource sonidoPasosEnemigo;
    public AudioSource sonidoRecogerPinguino;

    [Header("Audio Clips")]
    public AudioClip clipDeAmbiente;
    public AudioClip clipDePersecusion;
    public AudioClip clipPasosJugador;
    public AudioClip clipPasosEnemigo;
    public AudioClip clipRecogerPinguino;

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
        }
    }

    private void Start()
    {
        PlayAmbient(); 
    }

    public void PlayAmbient()
    {
        if (!sonidoDeAmbiente.isPlaying)
        {
            sonidoDeAmbiente.clip = clipDeAmbiente;
            sonidoDeAmbiente.loop = true;
            sonidoDeAmbiente.Play();
        }

        sonidoDePersecusion.Stop();
    }

    public void PlayChaseMusic()
    {
        if (!sonidoDePersecusion.isPlaying)
        {
            sonidoDePersecusion.clip = clipDePersecusion;
            sonidoDePersecusion.loop = true;
            sonidoDePersecusion.Play();
        }

        sonidoDeAmbiente.Stop();
    }

    public void PlayPlayerFootsteps(bool play)
    {
        if (play)
        {
            if (!sonidoPasosJugador.isPlaying)
            {
                sonidoPasosJugador.clip = clipPasosJugador;
                sonidoPasosJugador.loop = true;
                sonidoPasosJugador.Play();
            }
        }
        else
        {
            sonidoPasosJugador.Stop();
        }
    }

    public void PlayEnemyFootsteps(bool play)
    {
        if (play)
        {
            if (!sonidoPasosEnemigo.isPlaying)
            {
                sonidoPasosEnemigo.clip = clipPasosEnemigo;
                sonidoPasosEnemigo.loop = true;
                sonidoPasosEnemigo.Play();
            }
        }
        else
        {
            sonidoPasosEnemigo.Stop();
        }
    }

    public void PlayPickup()
    {
        sonidoRecogerPinguino.PlayOneShot(clipRecogerPinguino);
    }
}
