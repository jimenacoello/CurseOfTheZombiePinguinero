using UnityEngine;
using UnityEngine.Rendering.PostProcessing; 
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Post Processing")]
    public PostProcessVolume postProcessVolume;
    private DepthOfField dof;

    [Header("UI")]
    public GameObject menuUI;
    public GameObject creditsUI;
    [Tooltip("Imagen que aparece solo cuando estás en el juego")]
    public GameObject inGameImage;

    private bool isMenuActive = true;
    private bool isCreditsActive = false;

    void Start()
    {
        if (postProcessVolume != null)
        {
            postProcessVolume.profile.TryGetSettings(out dof);

            if (dof != null)
            {
                dof.active = true;
                dof.focusDistance.value = 0.1f;
            }
        }

        if (menuUI != null) menuUI.SetActive(true);
        if (creditsUI != null) creditsUI.SetActive(false);
        if (inGameImage != null) inGameImage.SetActive(false);

        PauseGame(true);
    }

    void Update()
    {
        // Nuevo juego con Enter
        if (isMenuActive && Input.GetKeyDown(KeyCode.Return))
        {
            NuevoJuego();
        }

        // Continuar desde el ultimo guardado con R
        if (isMenuActive && Input.GetKeyDown(KeyCode.R))
        {
            ContinuarPartida();
        }

        // Creditos
        if (isMenuActive && Input.GetKeyDown(KeyCode.C))
        {
            ToggleCredits();
        }

        // Pausar/Reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuActive) ResumeGame();
            else PauseGame(true);
        }
    }

    void NuevoJuego()
    {
        // Reiniciar progreso y pinguinos
        GameManager.Instance.ReiniciarJuego();
        ResumeGame();
    }

    void ContinuarPartida()
    {
        // Cargar lo ultimo guardado
        GameManager.Instance.CargarPartida();
        ResumeGame();
    }

    void ResumeGame()
    {
        if (dof != null)
        {
            dof.focusDistance.value = 10f;
        }

        if (menuUI != null) menuUI.SetActive(false);
        if (creditsUI != null) creditsUI.SetActive(false);
        if (inGameImage != null) inGameImage.SetActive(true);

        PauseGame(false);
    }

    void PauseGame(bool pause)
    {
        isMenuActive = pause;

        Time.timeScale = pause ? 0f : 1f;

        if (menuUI != null) menuUI.SetActive(pause);
        if (inGameImage != null) inGameImage.SetActive(!pause);

        if (dof != null)
        {
            dof.focusDistance.value = pause ? 0.1f : 10f;
        }
    }

    void ToggleCredits()
    {
        if (creditsUI == null) return;

        isCreditsActive = !isCreditsActive;
        creditsUI.SetActive(isCreditsActive);
    }
}
