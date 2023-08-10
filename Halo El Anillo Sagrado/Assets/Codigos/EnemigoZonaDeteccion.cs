using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoZonaDeteccion : MonoBehaviour {

    /*** Variables publicas ***/
    [Header("Referencias Externas")]
    [SerializeField] private Enemigo enemigo;
    [SerializeField] private Transform enemigo_generador_bala;
    private Rigidbody rb_enemigo;

    [Space]
    [Header("Atributos Bala Enemiga")]
    [SerializeField] private BalaEnemiga prefab_bala_enemiga;
    [SerializeField] private BalaEnemiga[] lista_balas;

    // Variables para el manejo de balas
    private BalaEnemiga bala_generada;
    private Rigidbody rb_bala;
    private int tamano_lista_balas = 15;
    private float velocidad_disparo = 20f;
    private float tiempo_cadencia_disparo = 2.5f;
    private float tiempo_cadencia_actual = 0f;

    // Variables para el movimiento automático del enemigo
    Vector3 movimiento;
    private bool jugador_detectado = false;
    private float tiempo_movimiento = 2f;
    private float tiempo_movimiento_actual = 0f;
    private int movimiento_aleatorio = 0;


    /*** Funciones ***/

    void Start() {
        lista_balas = new BalaEnemiga[tamano_lista_balas];

        for (int i = 0; i < tamano_lista_balas; i++) {
            // Genera nuevos GameObjects de tipo Bala en la jerarquía
            bala_generada = Instantiate(prefab_bala_enemiga, transform.position, transform.rotation, enemigo_generador_bala);
            bala_generada.gameObject.SetActive(false);

            lista_balas[i] = bala_generada;
        }

        // Obtiene el RigidBody del enemigo para poder moverlo
        rb_enemigo = enemigo.GetComponent<Rigidbody>();
    }

    void Update() {
        // Si hay un jugador cerca, el enemigo comenzará a moverse
        if (jugador_detectado == true) {
            tiempo_movimiento_actual += Time.deltaTime;

            if (tiempo_movimiento_actual >= tiempo_movimiento) {
                movimiento_aleatorio = Random.Range(0, 5);
                tiempo_movimiento_actual = 0f;
            }

            // El enemigo se mueve aleatoriamente
            switch (movimiento_aleatorio) {
                // El enemigo se queda quieto
                case 0:
                    break;

                // El enemigo se mueve hacia adelante
                case 1:
                    movimiento = enemigo.transform.forward * 2.5f * Time.deltaTime;
                    rb_enemigo.MovePosition(enemigo.transform.position + movimiento);
                    break;

                // El enemigo se mueve hacia atrás
                case 2:
                    movimiento = -enemigo.transform.forward * 2.5f * Time.deltaTime;
                    rb_enemigo.MovePosition(enemigo.transform.position + movimiento);
                    break;

                // El enemigo se mueve hacia la izquierda
                case 3:
                    movimiento = -enemigo.transform.right * 2.5f * Time.deltaTime;
                    rb_enemigo.MovePosition(enemigo.transform.position + movimiento);
                    break;

                // El enemigo se mueve hacia la derecha
                case 4:
                    movimiento = enemigo.transform.right * 2.5f * Time.deltaTime;
                    rb_enemigo.MovePosition(enemigo.transform.position + movimiento);
                    break;

                default: break;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        // Un jugador se acerca a este enemigo
        if (other.gameObject.tag == "Player") {
            jugador_detectado = true;
        }
    }

    void OnTriggerExit(Collider other) {
        // Un jugador se aleja de este enemigo
        if (other.gameObject.tag == "Player") {
            jugador_detectado = false;
        }
    }

    void OnTriggerStay(Collider other) {
        // Un jugador está cerca de este enemigo
        if (other.gameObject.tag == "Player") {

            // El enemigo mira al jugador
            mirarAJugador(other.gameObject);

            // El enemigo dispara al jugador
            dispararAJugador(other.gameObject);
        }
    }

    private void mirarAJugador(GameObject jugador) {
        // Determina en qué posición debe mirar el enemigo
        Vector3 posicion_objetivo = new Vector3(jugador.transform.position.x, enemigo.transform.position.y, jugador.transform.position.z);

        // El enemigo "mira" hacia el jugador
        enemigo.transform.LookAt(posicion_objetivo);
    }

    private void dispararAJugador(GameObject jugador) {
        // Determina en qué posición debe apuntar el generador de balas
        Vector3 posicion_objetivo = new Vector3(jugador.transform.position.x, jugador.transform.position.y, jugador.transform.position.z);

        // El generador de balas apunta hacia el jugador
        enemigo_generador_bala.transform.LookAt(posicion_objetivo);


        tiempo_cadencia_actual += Time.deltaTime;

        // El enemigo va disparando una bala cada cierto tiempo
        if (tiempo_cadencia_actual >= tiempo_cadencia_disparo) {
            

            // Dispara una bala creada previamente mediante la técnica Object Pooling
            BalaEnemiga bala_obtenida = obtenerBalaDisponible();
            if (bala_obtenida != null) {
                // Posiciona la bala en el lugar del generador
                bala_obtenida.transform.position = enemigo_generador_bala.transform.position;
                bala_obtenida.transform.rotation = enemigo_generador_bala.transform.rotation;

                // Activa la bala y la "dispara"
                bala_obtenida.gameObject.SetActive(true);
                rb_bala = bala_obtenida.GetComponent<Rigidbody>();
                rb_bala.velocity = enemigo_generador_bala.forward * velocidad_disparo;
            }

            // Reinicia el tiempo de cadencia actual
            tiempo_cadencia_actual = 0;
        }
    }

    // Mediante la técnica de Object Pooling, obtiene la bala más próxima disponible para ser utilizada
    private BalaEnemiga obtenerBalaDisponible() {
        for (int i = 0; i < lista_balas.Length; i++) {
            if (lista_balas[i].gameObject.activeInHierarchy == false) {
                return lista_balas[i];
            }
        }
        return null;
    }

}
