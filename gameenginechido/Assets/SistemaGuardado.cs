using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SistemaGuardado
{
    public static void GuardarPartida()
    {
        //Donde se guarda el archivo
        //string path = Application.dataPath + GameManager.Instance.nombreGuardado;

        //Se crea un flujo de informacion con la direccion y accion 
        //FileStream stream = new FileStream(path, FileMode.Create);

        //Mandamos llamar la info que se va guardar
        PerfilJugador perfil = new PerfilJugador();

        //Creamos una variable de formateo binario
        BinaryFormatter formatter = new BinaryFormatter();

        //Encriptar en binario la info
        //formatter.Serialize(stream,perfil);



        //Se cierra el flujo de la informacion
        //stream.Close();
    }



    public static void CargarPartida()
    {
        //Direccion donde se guarda el archivo
        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + "/jugador.fun";

        if(File.Exists(path))
        {
            FileStream stream = new FileStream(path, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            PerfilJugador perfil = formatter.Deserialize(stream) as PerfilJugador;
            stream.Close();

            //return perfil;
        }
        else
        {
            Debug.Log("No chencontro achivo");

            //return null;
        }
    }
}
