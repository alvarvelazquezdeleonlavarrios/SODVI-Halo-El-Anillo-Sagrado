using UnityEngine;

public class Bala : MonoBehaviour {

    /*** Variables privadas ***/
    private float tiempo_vida = 1.7f;
    private float tiempo_vida_actual = 0f;


    /*** Funciones ***/

    void Update() {
        if (gameObject.activeInHierarchy == true) {
            tiempo_vida_actual += Time.deltaTime;

            if (tiempo_vida_actual >= tiempo_vida) {
                tiempo_vida_actual = 0;
                gameObject.SetActive(false);
            }
        }
    }

    void OnCollisionEnter(Collision collision) {
        tiempo_vida_actual = 0;
        gameObject.SetActive(false);
    }

}
