using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogoData
{
    public string nombre;
    [TextArea(3, 10)]
    public string texto;
    public Sprite personaje;
    public Sprite caja;
}

public class Conversacion : MonoBehaviour
{
    [Header("Datos del personaje")]
    [SerializeField] private DialogoData dialogo;

    [Header("Sistema de UI (compartido)")]
    [SerializeField] private GameObject sistemaDialogos;
    [SerializeField] private Image caja;
    [SerializeField] private TextMeshProUGUI nombre;
    [SerializeField] private TextMeshProUGUI texto;

    [Header("Interacción")]
    [SerializeField] private float radio = 2f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float velocidadTexto = 0.05f;

    private bool jugadorCerca = false;
    private bool textoMostrando = false;

    private void Start()
    {
        sistemaDialogos.SetActive(false); 
    }

    private void Update()
    {
        jugadorCerca = Physics.CheckSphere(transform.position, radio, playerLayer);

        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            if (!textoMostrando)
            {
                MostrarDialogo();
            }
            else
            {
                OcultarDialogo();
            }
        }
    }

    private void MostrarDialogo()
    {
        sistemaDialogos.SetActive(true); 
        nombre.text = dialogo.nombre;
        caja.sprite = dialogo.caja;

        StopAllCoroutines();
        StartCoroutine(PrintText(dialogo.texto));
    }

    private void OcultarDialogo()
    {
        StopAllCoroutines();
        texto.text = "";
        nombre.text = "";
        sistemaDialogos.SetActive(false); 
        textoMostrando = false;
    }

    private IEnumerator PrintText(string textoCompleto)
    {
        textoMostrando = true;
        texto.text = "";

        foreach (char letra in textoCompleto)
        {
            texto.text += letra;
            yield return new WaitForSeconds(velocidadTexto);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
