using UnityEngine;
using UnityEngine.UI;

public class GeneradorBalas : MonoBehaviour {

    /*** Variables públicas ***/
    [Header("Atributos de Bala")]
    [SerializeField] private JugadorRigidBody jugador;
    [SerializeField] private Bala prefab_bala;
    private Bala[] lista_balas;
    private int tamano_lista_balas = 20;

    [Space]
    [Header("Elementos en pantalla")]
    [SerializeField] private Text texto_balas_actuales;
    [SerializeField] private Text texto_balas_maximas;

    [Space]
    [Header("Sistema de Audio")]
    [SerializeField] private AudioSource sonido_recarga;
    [SerializeField] private AudioSource prefab_sonido_disparo;
    private AudioSource[] lista_audios_disparo;
    private int tamano_lista_audios_disparo = 25;


    /*** Variables privadas ***/
    private Animator animator_jugador;
    private Bala bala_generada;
    private Rigidbody rb_bala;
    private float velocidad_disparo = 40f;
    private int balas_actuales = 32;
    private int balas_maximas = 108;

    private float tiempo_recarga = 1.5f;
    private float tiempo_recarga_actual = 0f;
    private bool recargando = false;


    /*** Funciones ***/

    void Start() {
        animator_jugador = jugador.GetComponent<Animator>();
        lista_balas = new Bala[tamano_lista_balas];
        lista_audios_disparo = new AudioSource[tamano_lista_audios_disparo];

        // Genera nuevos GameObjects de tipo Bala
        for (int i=0; i< tamano_lista_balas; i++) {
            bala_generada = Instantiate(prefab_bala, transform.position, transform.rotation, jugador.transform);
            bala_generada.gameObject.SetActive(false);

            lista_balas[i] = bala_generada;
        }

        // Genera nuevas fuentes de audio para los disparos
        for (int i=0; i< tamano_lista_audios_disparo; i++) {
            AudioSource audio_generado = Instantiate(prefab_sonido_disparo, transform.position, transform.rotation, transform);
            audio_generado.Stop();

            lista_audios_disparo[i] = audio_generado;
        }

        // Muestra las balas iniciales en pantalla
        actualizarBalasUI();
    }

    void Update() {
        // Acción de disparar
        if (Input.GetMouseButtonDown(0)) {
            disparar();
        }

        // Acción de recargar
        if (Input.GetKeyDown(KeyCode.R)) {
            recargar();
        }

        // Tiempo de espera para que se recargue el arma
        if (recargando == true) {
            tiempo_recarga_actual += Time.deltaTime;

            // Pasado el tiempo de espera, el jugador recarga
            if (tiempo_recarga_actual >= tiempo_recarga) {
                recarga();

                recargando = false;
                tiempo_recarga_actual = 0f;
            }
        }
    }

    private void disparar() {
        if (balas_actuales > 0 && recargando == false) {
            
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

                // Reproduce el audio de bala disparada
                reproducirAudioDisponible();

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

            // Determina si no se está recargando en el momento actual
            if (recargando == false) {
                // Permite reproducir la animación de recarga
                animator_jugador.SetBool("Recargar", true);

                sonido_recarga.Play();
                recargando = true;
            }
            
        }
    }

    private void recarga() {
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

        // Actualiza la cantidad de balas en pantalla
        actualizarBalasUI();
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

    // Mediante la técnica de Object Pooling, reproduce el audio de disparo más próximo disponible
    private void reproducirAudioDisponible() {
        for (int i=0; i<lista_audios_disparo.Length; i++) {
            if (lista_audios_disparo[i].isPlaying == false) {
                lista_audios_disparo[i].Play();
                break;
            }
        }
    }

    void actualizarBalasUI() {
        texto_balas_actuales.text = balas_actuales.ToString("D2");
        texto_balas_maximas.text = balas_maximas.ToString("D3");
    }

    public void agregarMunicionMaxima(Collision objeto_cargador) {
        if (objeto_cargador != null && objeto_cargador.gameObject.tag == "Caja Municion") {

            // Solo se agregan balas si el cargador aún no está lleno
            if (balas_maximas < 216) {
                balas_maximas += 54;

                if (balas_maximas > 216) {
                    balas_maximas = 216;
                }

                actualizarBalasUI();
                Destroy(objeto_cargador.gameObject);
            }
        }
    }

}
