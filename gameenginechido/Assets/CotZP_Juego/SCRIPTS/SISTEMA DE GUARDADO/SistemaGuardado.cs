using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SistemaGuardado
{
    private static string path = Application.persistentDataPath + "/jugador.dat";

    public static void GuardarPartida(PerfilJugador perfil)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, perfil);
        stream.Close();

        Debug.Log("Partida guardada en: " + path);
    }

    public static PerfilJugador CargarPartida()
    {
        if (File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PerfilJugador perfil = formatter.Deserialize(stream) as PerfilJugador;
            stream.Close();

            Debug.Log("Partida cargada");
            return perfil;
        }
        else
        {
            Debug.LogWarning("No se encontró archivo de guardado");
            return null;
        }
    }
}
