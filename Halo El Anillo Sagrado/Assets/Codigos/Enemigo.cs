using UnityEngine;

public class Enemigo : MonoBehaviour {

    /*** Variables publicas ***/
    [Header("Atributos Generales")]
    [SerializeField]  private float vida = 100f;

    [Space]
    [Header("Sistema de Audio")]
    [SerializeField] private AudioSource fuente_audio;
    [SerializeField] private AudioClip[] audios_enemigo;
    [SerializeField] private float tiempo_espera_audio = 3.2f;
    private float tiempo_espera_audio_actual = 0f;


    /*** Funciones ***/

    void Update() {
        tiempo_espera_audio_actual += Time.deltaTime;

        // Luego de pasado un tiempo, el enemigo reproduce un audio aleatorio
        if (tiempo_espera_audio_actual >= tiempo_espera_audio) {
            tiempo_espera_audio_actual = 0f;

            int audio_aleatorio = Random.Range(0, audios_enemigo.Length);
            fuente_audio.clip = audios_enemigo[audio_aleatorio];
            fuente_audio.Play();
        }
    }

    void OnCollisionEnter(Collision collision) {
        // El enemigo recibe una bala del jugador
        if (collision.gameObject.tag == "Bala") {
            vida -= 8f;

            // El enemigo "muere"
            if (vida <= 0f) {
                // Se destruye al objeto de juego del enemigo y todos sus objetos hijo
                Destroy(gameObject);
            }
        } 
        // El enemigo, por alguna razón, se cae al vacío
        else if (collision.gameObject.tag == "Vacio") {
            Destroy(gameObject);
        }
    }

}
