using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BOTONES : MonoBehaviour
{
    //Funcion que nos permite cambiar de escena
    public void CambioEscena(string Nombre)
    {
        //Usando "SceneManager" cambiamos de escena mandando el nombre de la escena como parametro
        SceneManager.LoadScene(Nombre);
    }

    public void salir(){
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying=false;
    }
}