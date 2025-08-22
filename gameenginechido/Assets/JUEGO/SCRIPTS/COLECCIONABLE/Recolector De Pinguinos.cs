using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecolectorDePinguinos : MonoBehaviour
{
    [Header("Detección")]
    [SerializeField] private float radio = 2f;
    [SerializeField] private LayerMask layerPinguino;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI contadorText;

    private int pinguinosRecolectados = 0;

    private void Update()
    {
        Collider[] pinguinos = Physics.OverlapSphere(transform.position, radio, layerPinguino);

        if (pinguinos.Length > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Collider col in pinguinos)
            {
                col.gameObject.SetActive(false);
                pinguinosRecolectados++;

                AudioManager.Instance.PlayPickup(); 

                ActualizarContador();
                break;
            }
        }
    }

    private void ActualizarContador()
    {
        if (contadorText != null)
        {
            contadorText.text = "" + pinguinosRecolectados;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
