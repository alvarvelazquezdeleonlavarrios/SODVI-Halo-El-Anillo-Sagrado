using UnityEngine;

public class BalaEnemiga : MonoBehaviour {

    /*** Variables privadas ***/
    private float tiempo_vida = 1.7f;
    private float tiempo_vida_actual = 0f;


    /*** Funciones ***/

    void Update() {
        // Verifica que el GameObject de la bala esté activo en la escena
        if (gameObject.activeInHierarchy == true) {
            tiempo_vida_actual += Time.deltaTime;

            if (tiempo_vida_actual >= tiempo_vida) {
                tiempo_vida_actual = 0;
                gameObject.SetActive(false);
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        // Se asegura que la bala, al ser disparada, no detecte que colisionó con el Enemigo que la dispara
        if (collision.gameObject.tag == "Enemigo") {
            return;
        }

        tiempo_vida_actual = 0;
        gameObject.SetActive(false);
    }

}
