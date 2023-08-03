using UnityEngine;

public class Jugador : MonoBehaviour {

    /*** Variables Publicas ***/
    [SerializeField] private Camera camara;
    [SerializeField] private float velocidad_movimiento = 5f;
    [SerializeField] private float velocidad_giro_X = 4f, velocidad_giro_Y = 4f;
    [SerializeField] private float fuerza_salto = 4f;

    /*** Variables Privadas ***/
    private bool en_suelo = false;

    /*** Variables Auxiliares ***/
    private Vector3 dir_mov, velocidad_jugador;
    private float rotacion_vertical;
    private CharacterController controller;


    /*** Funciones ***/

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        mover();
        saltar();
    }

    // Permite mover al jugador en el escenario
    private void mover() {
        // Determina si el jugador está en el suelo o no
        en_suelo = controller.isGrounded;

        // Se asegura que no se genere una velocidad hacia abajo cuando el jugador
        // está en el suelo
        if (en_suelo == true && velocidad_jugador.y <= 0f) {
            velocidad_jugador.y = 0f;
        }

        // Mueve al jugador de izquierda a derecha, adelante y atrás
        float movX = Input.GetAxisRaw("Horizontal");
        float movZ = Input.GetAxisRaw("Vertical");
        dir_mov = transform.right * movX + transform.forward * movZ;
        controller.Move(dir_mov * velocidad_movimiento * Time.deltaTime);

        // Permite mirar hacia los lados, arriba y abajo
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * velocidad_giro_X);
        rotacion_vertical += Input.GetAxis("Mouse Y") * velocidad_giro_Y;
        rotacion_vertical = Mathf.Clamp(rotacion_vertical, -70f, 70f);
        camara.transform.localEulerAngles = Vector3.left * rotacion_vertical;

        // Agrega gravedad al jugador
        velocidad_jugador.y += -9.81f * Time.deltaTime;
        controller.Move(velocidad_jugador * Time.deltaTime);
    }

    // Permite al jugador saltar
    private void saltar() {
        if (Input.GetKeyDown(KeyCode.Space) && en_suelo == true) {
            velocidad_jugador.y += Mathf.Sqrt(fuerza_salto * -3f * -9.81f);
        }
    }

}
