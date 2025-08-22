using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Referencias HUD")]
    public GameObject creditosPanel;

    private bool creditosActivos = false;

    private void Start()
    {
        MenuAudioManager.Instance.PlayMenuMusic(); 
    }

    private void Update()
    {
        if (creditosActivos && Input.GetKeyDown(KeyCode.Escape))
        {
            CerrarCreditos();
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }

    public void MostrarCreditos()
    {
        creditosPanel.SetActive(true);
        creditosActivos = true;
    }

    public void CerrarCreditos()
    {
        creditosPanel.SetActive(false);
        creditosActivos = false;
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void SonidoHoverBoton()
    {
        MenuAudioManager.Instance.PlayBoton();
    }
}
