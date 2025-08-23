using TMPro;
using UnityEngine;

public class RecolectorDePinguinos : MonoBehaviour
{
    [Header("Detecci�n")]
    [SerializeField] private float radio = 2f;
    [SerializeField] private LayerMask layerPinguino;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI contadorText;

    private void Update()
    {
        // Buscar ping�inos cerca
        Collider[] pinguinos = Physics.OverlapSphere(transform.position, radio, layerPinguino);

        // Presionar Q para recoger uno
        if (pinguinos.Length > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            foreach (Collider col in pinguinos)
            {
                // Busca el componente Pinguino para leer su ID (en el mismo GO o en el padre)
                Pinguino ping = col.GetComponent<Pinguino>();
                if (ping == null) ping = col.GetComponentInParent<Pinguino>();
                if (ping == null)
                {
                    Debug.LogWarning("El objeto en rango no tiene script 'Pinguino' con ID.");
                    continue;
                }

                // Sumar al contador global con el ID correcto
                GameManager.Instance.AgregarPing�ino(ping.ID);

                // Guardar el progreso (posici�n del jugador + lista de IDs)
                GameManager.Instance.GuardarProgreso();

                if (AudioManager.Instance != null) AudioManager.Instance.PlayPickup();

                // Desactivar ese ping�ino del mapa
                ping.gameObject.SetActive(false);

                // Actualizar la UI desde el GameManager
                ActualizarContador();

                break; 
            }
        }
    }

    private void ActualizarContador()
    {
        if (contadorText != null)
        {
            contadorText.text = GameManager.Instance.ObtenerContador().ToString();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
