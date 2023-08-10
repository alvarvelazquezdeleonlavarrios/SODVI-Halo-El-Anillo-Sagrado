using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour {

    /*** Variables privadas ***/
    private float vida = 100f;


    /*** Funciones ***/

    void OnCollisionEnter(Collision collision) {
        // El enemigo recibe una bala del jugador
        if (collision.gameObject.tag == "Bala") {
            vida -= 8f;

            // El enemigo "muere"
            if (vida <= 0f) {
                vida = 0;

                // Se destruye el objeto de juego y a sus hijos
                Destroy(this.gameObject);
            }
        }
    }

}
