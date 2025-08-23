using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PerfilJugador
{
    public int colect;
    public float[] pos;
    public List<string> pinguinosRecolectados;

    public PerfilJugador(int colect, Vector3 position, List<string> recolectados)
    {
        this.colect = colect;
        pos = new float[3];
        pos[0] = position.x;
        pos[1] = position.y;
        pos[2] = position.z;

        pinguinosRecolectados = recolectados;
    }

    public Vector3 GetPosition()
    {
        return new Vector3(pos[0], pos[1], pos[2]);
    }
}
