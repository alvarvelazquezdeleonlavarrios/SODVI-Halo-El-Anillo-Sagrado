using UnityEngine;

public class JugadorRigidBody : MonoBehaviour {

    /*** Variables Públicas ***/
    [Header("Atributos Generales")]
    [SerializeField] private Camera camara;
    [SerializeField] private float velocidad_movimiento = 5f;
    [SerializeField] private float velocidad_giro = 5f;
    [SerializeField] private float fuerza_salto = 5f;

    [Space]
    [Header("Referencias Externas")]
    [SerializeField] private GeneradorBalas generador_balas;

    /*** Variables Privadas ***/
    private Rigidbody rb;
    private Animator animator;
    private bool en_suelo = false;
    private float mouseY;


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
            //animator.Play("Caminar");
            animator.SetBool("Reposo", false);
            animator.SetBool("Caminar", true);
        }
        // En caso contrario, reproduce la animación de reposo
        else {
            //animator.Play("Reposo");
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
            rb.AddForce(transform.up * fuerza_salto, ForceMode.Impulse);
            en_suelo = false;
        }


        // Movimiento de la cámara
        mouseY += Input.GetAxis("Mouse Y") * velocidad_giro;
        mouseY = Mathf.Clamp(mouseY, -70f, 70f);
        camara.transform.localEulerAngles = Vector3.left * mouseY;
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
    }

    private void finalizarAnimacionDisparo() {
        animator.SetBool("Disparar", false);
    }
}
