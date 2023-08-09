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
                print("Muerte de Enemigo");
            }

            print(vida);
        }
    }

    private void mirarAJugador(GameObject jugador) {
        // Verifica que exista una distancia limite entre el enemigo y el jugador
        if (Vector3.Distance(transform.position, jugador.transform.position) <= 15f) {
            
            // Determina en qué posición debe mirar el enemigo
            Vector3 posicion_objetivo = new Vector3(jugador.transform.position.x, 
                                                    transform.position.y, 
                                                    jugador.transform.position.z);

            // El enemigo "mira" hacia el jugador
            transform.LookAt(posicion_objetivo);
        }
    }
}
