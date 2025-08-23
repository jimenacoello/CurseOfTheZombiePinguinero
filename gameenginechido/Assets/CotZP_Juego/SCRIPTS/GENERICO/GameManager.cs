using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int colectados;
    private GameObject player;
    private Vector3 posicionInicial;
    private List<string> recolectadosIDs = new List<string>();

    [Header("UI")]
    public GameObject imagenFin; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            posicionInicial = player.transform.position;
        }
    }

    private void Start()
    {
        CargarPartida();

        if (imagenFin != null)
            imagenFin.SetActive(false); 
    }

    // ----------------------
    // MÉTODOS PARA RECOLECTOR
    // ----------------------
    public void AgregarPingüino(string id)
    {
        if (!recolectadosIDs.Contains(id))
        {
            recolectadosIDs.Add(id);
            colectados++;
            GuardarPartida();

            // revisar si ya conseguimos los 10
            if (colectados >= 10)
            {
                FinDelJuego();
            }
        }
    }

    private void FinDelJuego()
    {
        // Pausar el juego
        Time.timeScale = 0f;

        // Activar la imagen
        if (imagenFin != null)
            imagenFin.SetActive(true);
    }

    public void GuardarProgreso()
    {
        GuardarPartida();
    }

    public int ObtenerContador()
    {
        return colectados;
    }

    // ----------------------
    // MÉTODOS DE GUARDADO
    // ----------------------
    public void GuardarPartida()
    {
        if (player == null) return;

        PerfilJugador perfil = new PerfilJugador(colectados, player.transform.position, recolectadosIDs);
        SistemaGuardado.GuardarPartida(perfil);
    }

    public void CargarPartida()
    {
        PerfilJugador perfil = SistemaGuardado.CargarPartida();
        if (perfil != null && player != null)
        {
            colectados = perfil.colect;
            recolectadosIDs = perfil.pinguinosRecolectados;
            player.transform.position = perfil.GetPosition();

            foreach (Pinguino p in FindObjectsOfType<Pinguino>())
            {
                p.gameObject.SetActive(!recolectadosIDs.Contains(p.ID));
            }

            // Si ya tenemos 10 o más, mostrar la imagen y pausar
            if (colectados >= 10)
            {
                FinDelJuego();
            }
        }
    }

    // ----------------------
    // MÉTODOS PARA MENÚ
    // ----------------------
    public void ReiniciarJuego()
    {
        colectados = 0;
        recolectadosIDs.Clear();

        if (player != null)
            player.transform.position = posicionInicial;

        foreach (Pinguino p in FindObjectsOfType<Pinguino>())
        {
            p.gameObject.SetActive(true);
        }

        if (imagenFin != null)
            imagenFin.SetActive(false);

        Time.timeScale = 1f; // asegurarnos de que el juego se reanude

        GuardarPartida();
    }

    public void ReiniciarDesdeUltimoPunto()
    {
        CargarPartida();
        Time.timeScale = 1f; // reanudar juego si estaba pausado
    }
}
