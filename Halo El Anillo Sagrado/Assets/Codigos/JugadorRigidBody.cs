using UnityEngine;

public class JugadorRigidBody : MonoBehaviour {

    /*** Variables Públicas ***/
    public Camera camara;
    public float velocidad_movimiento = 5f;
    public float velocidad_giro = 5f;
    public float fuerza_salto = 5f;

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
        if (collision.gameObject.tag == "Suelo") {
            en_suelo = true;
        }
    }

    void habilidades() {
        /*if (Input.GetMouseButtonDown(0)) {
            animator.SetBool("Ataque1", true);
        } else { animator.SetBool("Ataque1", true) } } */
    }
}
