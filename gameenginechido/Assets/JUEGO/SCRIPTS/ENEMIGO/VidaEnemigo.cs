using UnityEngine;

public class VidaEnemigo : MonoBehaviour
{
    [SerializeField]
    int vida = 3;

    public void DanoEnemigo(int dano)
    {
        vida -= dano;

        if (vida <= 0)
        {
            Destroy(this.gameObject);

        }
    }
}
