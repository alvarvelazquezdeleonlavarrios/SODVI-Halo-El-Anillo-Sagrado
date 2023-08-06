using UnityEngine;
using UnityEngine.UI;

public class GeneradorBalas : MonoBehaviour {

    /*** Variables públicas ***/
    [Header("Atributos Generales")]
    [SerializeField] private JugadorRigidBody jugador;
    [SerializeField] private Bala prefab_bala;
    [SerializeField] private Bala[] lista_balas;
    [SerializeField] private int tamano_lista_balas = 20;

    [Space]
    [Header("Elementos en pantalla")]
    [SerializeField] private Text texto_balas_actuales;
    [SerializeField] private Text texto_balas_maximas;

    /*** Variables privadas ***/
    private Animator animator_jugador;
    private Bala bala_generada;
    private Rigidbody rb_bala;
    private float velocidad_disparo = 20f;
    private int balas_actuales = 32;
    private int balas_maximas = 108;

    //private float tiempo_recarga = 1.5f;


    /*** Funciones ***/

    void Start() {
        animator_jugador = jugador.GetComponent<Animator>();
        lista_balas = new Bala[tamano_lista_balas];

        for (int i=0; i< tamano_lista_balas; i++) {
            // Genera nuevos GameObjects de tipo Bala en la jerarquía
            bala_generada = Instantiate(prefab_bala, transform.position, transform.rotation);
            bala_generada.gameObject.SetActive(false);

            lista_balas[i] = bala_generada;
        }

        // Muestra las balas iniciales en pantalla
        actualizarBalasUI();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            disparar();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            recargar();
        }
    }

    private void disparar() {
        if (balas_actuales > 0) {
            
            // Dispara una bala creada previamente mediante la técnica Object Pooling
            Bala bala_obtenida = obtenerBalaDisponible();
            if (bala_obtenida != null) {
                // Resta el contador de balas actuales
                balas_actuales--;

                // Posiciona la bala en el lugar del generador
                bala_obtenida.transform.position = transform.position;
                bala_obtenida.transform.rotation = transform.rotation;

                // Activa la bala y la "dispara"
                bala_obtenida.gameObject.SetActive(true);
                rb_bala = bala_obtenida.GetComponent<Rigidbody>();
                rb_bala.velocity = transform.forward * velocidad_disparo;

                // Permite reproducir la animación de disparo
                animator_jugador.SetBool("Disparar", true);

                // Actualiza la cantidad de balas en pantalla
                actualizarBalasUI();
            }
        }

        if (balas_actuales == 0) {
            recargar();
        }
    }


    private void recargar() {
        // Primero verifica que el cargador actual del arma no esté lleno y que todavía haya balas en el inventario
        if (balas_actuales < 32 && balas_maximas > 0) {

            // Determina la cantidad de balas que hacen falta en el cargador actual
            int balas_faltantes = 32 - balas_actuales;

            // La cantidad de balas en el inventario es mayor o igual a las balas que hacen falta
            if (balas_maximas >= balas_faltantes) {
                balas_actuales = 32;
                balas_maximas -= balas_faltantes;
            }
            // La cantidad de balas en el inventario es menor a las balas que hacen falta
            else {
                balas_actuales += balas_maximas;
                balas_maximas = 0;
            }

            actualizarBalasUI();
        }
    }

    // Mediante la técnica de Object Pooling, obtiene la bala más próxima disponible para ser utilizada
    private Bala obtenerBalaDisponible() {
        for (int i=0; i<lista_balas.Length; i++) {
            if (lista_balas[i].gameObject.activeInHierarchy == false) {
                return lista_balas[i];
            }
        }
        return null;
    }

    void actualizarBalasUI() {
        texto_balas_actuales.text = balas_actuales.ToString("D2");
        texto_balas_maximas.text = balas_maximas.ToString("D3");
    }

}
