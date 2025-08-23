using UnityEngine;

public class Pinguino : MonoBehaviour
{
    public string ID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AgregarPingüino(ID);
            gameObject.SetActive(false); 
        }
    }
}
