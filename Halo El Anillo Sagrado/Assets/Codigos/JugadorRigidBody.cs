using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JugadorRigidBody : MonoBehaviour {

    /*** Variables Públicas ***/
    [Header("Atributos Generales")]
    [SerializeField] private Camera camara;
    [SerializeField] private float velocidad_movimiento = 5f;
    [SerializeField] private float velocidad_giro = 5f;
    [SerializeField] private float fuerza_salto = 5f;

    [Space]
    [Header("Elementos en Pantalla")]
    [SerializeField] private Image barra_escudo;
    [SerializeField] private Image barra_vida;
    [SerializeField] private Text coleccionables_actuales;
    private float escudo = 100f;
    private float vida = 100f;
    private float tiempo_espera_escudo = 5f;
    private float tiempo_espera_escudo_actual = 0f;
    private bool escudo_lleno = true;
    private bool recargando_escudo = false;

    [Space]
    [Header("Referencias Externas")]
    [SerializeField] private GeneradorBalas generador_balas;

    [Space]
    [Header("Sistema de Audio")]
    [SerializeField] private AudioSource fuente_audio;
    [SerializeField] private AudioClip sonido_escudo_bajo, sonido_escudo_apagado, sonido_escudo_recargando;

    /*** Variables Privadas ***/
    private Rigidbody rb;
    private Animator animator;
    private bool en_suelo = false;
    private float mouseY;

    // Conteo para los coleccionables del nivel
    private int cantidad_coleccionables = 0;


    /*** Funciones ***/

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        animator = GetComponent<Animator>();
    }

    void Update() {
        // Entrada desde teclado
        float input_horizontal = Input.GetAxis("Horizontal");
        float input_vertical = Input.GetAxis("Vertical");
        bool input_salto = Input.GetKeyDown(KeyCode.Space);

        // Calcula el vector de movimiento
        Vector3 direccion = transform.forward * input_vertical + transform.right * input_horizontal;
        direccion = direccion.normalized;

        // Si el jugador presiona las teclas de WASD, reproduce la animación de caminar
        if (Mathf.Abs(input_horizontal) > 0.2 || Mathf.Abs(input_vertical) > 0.2) {
            animator.SetBool("Reposo", false);
            animator.SetBool("Caminar", true);
        }
        // En caso contrario, reproduce la animación de reposo
        else {
            animator.SetBool("Reposo", true);
            animator.SetBool("Caminar", false);
        }

        // Aplica movimiento al jugador
        Vector3 movimiento = direccion * velocidad_movimiento * Time.deltaTime;
        rb.MovePosition(transform.position + movimiento);


        // Entrada desde ratón
        float mouseX = Input.GetAxis("Mouse X");

        // Calcula el vector de rotacion
        Vector3 rotacion = new Vector3(0, mouseX, 0) * velocidad_giro * 10f * Time.deltaTime;

        // Movimiento de giro
        transform.Rotate(rotacion);


        // Movimiento de Salto
        if (input_salto && en_suelo) {
            rb.AddForce(transform.up * fuerza_salto * 50f, ForceMode.Impulse);
            en_suelo = false;
        }


        // Movimiento de la cámara
        mouseY += Input.GetAxis("Mouse Y") * velocidad_giro;
        mouseY = Mathf.Clamp(mouseY, -70f, 70f);
        camara.transform.localEulerAngles = Vector3.left * mouseY;

        // Obliga al cursor del ratón a permanecer siempre en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;

        // Revisar si el escudo no esta lleno
        recargarEscudo();
    }

    void OnCollisionEnter(Collision collision) {
        // El jugador toca el suelo
        if (collision.gameObject.tag == "Suelo") {
            en_suelo = true;
        }

        // El jugador toca una caja de municiones
        else if (collision.gameObject.tag == "Caja Municion") {
            generador_balas.agregarMunicionMaxima(collision);
        }

        // El jugador recibe una bala enemiga
        else if (collision.gameObject.tag == "Bala Enemiga") {

            // En caso de que se estuviese recargando el escudo y se recibe una bala, se detiene el sonido
            if (fuente_audio.clip == sonido_escudo_recargando && fuente_audio.isPlaying == true) {
                fuente_audio.Stop();
            }

            // El jugador todavía tiene escudo
            if (escudo > 0f) {
                escudo -= 15f;

                // Audio de escudo bajo
                if (escudo <= 25f && escudo > 0f) {
                    fuente_audio.clip = sonido_escudo_bajo;
                    fuente_audio.loop = true;
                    fuente_audio.Play();
                }
                // Audio de escudo apagado
                else if (escudo <= 0f) {
                    escudo = 0f;
                    fuente_audio.clip = sonido_escudo_apagado;
                    fuente_audio.loop = true;
                    fuente_audio.Play();
                }

            }
            // El jugador no tiene escudo
            else {
                escudo = 0f;
                vida -= 15f;

                // El jugador "muere"
                if (vida <= 0f) {
                    SceneManager.LoadScene("MUERTE");
                }
            }

            recargando_escudo = false;
            escudo_lleno = false;
            tiempo_espera_escudo_actual = 0f;
            actualizarEscudoUI();
        }

        // El jugador toca un objeto coleccionable
        else if (collision.gameObject.tag == "Coleccionable") {
            cantidad_coleccionables++;
            coleccionables_actuales.text = cantidad_coleccionables.ToString();
            Destroy(collision.gameObject);

            // Ya se consiguieron todos los coleccionables del nivel (Misión Cumplida!!)
            if (cantidad_coleccionables == 4) {
                print("Mision Cumplida!!");

                /*
                 * ¡¡¡ Cargar Escena de "Victoria" !!!
                 * */
            }
        }

        // El jugador, por alguna razón, se cae al vacío
        else if (collision.gameObject.tag == "Vacio") {
            SceneManager.LoadScene("MUERTE");
        }
    }

    private void finalizarAnimacionDisparo() {
        animator.SetBool("Disparar", false);
    }

    private void finalizarAnimacionRecarga() {
        animator.SetBool("Recargar", false);
    }

    private void recargarEscudo() {
        // Si no está lleno el escudo, se espera un tiempo hasta que se recargue
        if (escudo_lleno == false && recargando_escudo == false) {
            tiempo_espera_escudo_actual += Time.deltaTime;

            // Pasado ese tiempo, el escudo comienza a recargarse
            if (tiempo_espera_escudo_actual >= tiempo_espera_escudo) {
                recargando_escudo = true;
                tiempo_espera_escudo_actual = tiempo_espera_escudo;

                // Audio de escudo recargando
                fuente_audio.Stop();
                fuente_audio.clip = sonido_escudo_recargando;
                fuente_audio.loop = false;
                fuente_audio.Play();
            }
        }

        // Va recargando los escudos durante un tiempo
        if (recargando_escudo == true) {
            escudo = Mathf.MoveTowards(escudo, 100f, 35f * Time.deltaTime);
            vida = Mathf.MoveTowards(vida, 100f, 35f * Time.deltaTime);
            actualizarEscudoUI();

            // Los escudos ya se recargaron por completo
            if (escudo >= 100f && vida >= 100f) {
                escudo = 100f;
                vida = 100f;
                recargando_escudo = false;
                escudo_lleno = true;

                actualizarEscudoUI();
            }
        }
    }

    private void actualizarEscudoUI() {
        barra_escudo.fillAmount = escudo / 100f;
        barra_vida.fillAmount = vida / 100f;
    }
}
