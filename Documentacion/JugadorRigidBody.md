# Documentación de JugadorRigidBody.cs

## Variables Públicas
### 1. `camara` (`[SerializeField] private Camera camara`)
Cámara del jugador

### 2. `velocidad_movimiento` (`[SerializeField] private float velocidad_movimiento = 5f`)
Velocidad con la que se mueve el jugador.

### 3. `velocidad_giro` (`[SerializeField] private float velocidad_giro = 5f`)
Velocidad con la que mira el jugador.

### 4. `fuerza_salto` (`[SerializeField] private fuerza_salto = 5f`)
Fuerza con la que salta el jugador.

### 5. `barra_escudo` (`[SerializeField] private Image barra_escudo`)
Imagen en pantalla que representa la barra del escudo de energía.

### 6. `barra_vida` (`[SerializeField] private Image barra_vida`)
Imagen en pantalla que representa la barra del vida del jugador.

### 7. `coleccionables_actuales` (`[SerializeField] private Text coleccionables_actuales`)
Texto en pantalla que indica la cantidad de coleccionables que ha recogido el jugador.

### 8. `generador_balas` (`[SerializeField] private GeneradorBalas generador_balas`)
GameObject vacío en el cual se generan las balas a disparar.

### 9. `fuente_audio` (`[SerializeField] private AudioSource fuente_audio`)
Fuente de audio en la cual se reproducen los efectos de sonido correspondientes.

### 10. `sonido_escudo_bajo` (`[SerializeField] private AudioClip sonido_escudo_bajo`)
Archivo de audio de escudo de energía bajo.

### 11. `sonido_escudo_apagado` (`[SerializeField] private AudioClip sonido_escudo_apagado`)
Archivo de audio de escudo de energía apagado.

### 12. `sonido_escudo_recargando` (`[SerializeField] private AudioClip sonido_escudo_recargando`)
Archivo de audio de escudo de energía recargándose.


## Variables Privadas
### 1. `escudo` (`private float escudo = 100f`)
Cantidad actual de escudo de energía del jugador.

### 2. `vida` (`private float vida = 100f`)
Cantidad actual de vida del jugador.

### 3. `tiempo_espera_escudo` (`private float tiempo_espera_escudo = 5f`)
Tiempo que debe esperar el jugador para que comience la acción de recarga del escudo y la vida.

### 4. `tiempo_espera_escudo_actual` (`private float tiempo_espera_escudo_actual = 0f`)
Tiempo del frame actual para detectar si ya se debe recargar el escudo y la vida.

### 5. `escudo_lleno` (`private bool escudo_lleno = true`)
Variable que indica si el escudo y la vida ya se recargaron por completo.

### 6. `recargando_escudo` (`private bool recargando_escudo = false`)
Variable que indica si en el momento actual se están recargando el escudo y la vida.

### 7. `rb` (`private Rigidbody rb`)
Componente de físicas del jugador.

### 8. `animator` (`private Animator animator`)
Componente de animaciones del jugador.

### 9. `en_suelo` (`private bool en_suelo = false`)
Variable que indica si el jugador está en el suelo para poder saltar.

### 10. `mouseY` (`private float mouseY`)
Variable utilizada en conjunto con el movimiento hacia arriba y abajo del ratón para poder rotar la cámara del jugador hacia arriba y abajo.

### 11. `cantidad_coleccionables` (`private int cantidad_coleccionables = 0`)
Cantidad actual de coleccionables que el jugador a adquirido.


## Métodos

### 1. `void Start()`
Configura los componentes de RigidBody y Animator del jugador para acceder a estos más tarde. Obliga al cursor del ratón a permanecer siempre en el centro de la pantalla

### 2. `void FixedUpdate()`
Permite al usuario mover al jugador izquierda, derecha, adelante y atrás presionando las teclas WASD y controlando su componente RigidBody (dependiendo si estas se presionan o no reproduce las animaciones de caminar o de reposo). Además, con el movimiento del ratón permite rotar hacia los lados al jugador y a la cámara hacia arriba y abajo. Detecta en todo momento si se debe recargar el escudo o no. La función, al ser FixedUpdate, se asegura de que el tiempo de ejecución sea el mismo independientemente del hardware del dispositivo en donde se esté ejecutando el juego.

### 3. `void OnCollisionEnter(Collision collision)`
Función que detecta si el jugador colisionó con alguno de los siguientes objetos:
- **Suelo:** permite al jugador saltar.
- **Caja de Munición:** agrega munición adicional a las balas en el inventario.
- **Bala Enemiga:** resta una cantidad de escudo o de vida cuando el escudo está apagado (igual a 0f). Dependiendo la cantidad de escudo, se reproduce el audio correspondiente. En caso de que, tanto el escudo como la vida lleguen a 0, se carga la escena de "Muerte" y se puede mover nuevamente el cursor en toda la pantalla. Detiene la recarga automática del escudo y actualiza las barras en pantalla.
- **Coleccionable:** incrementa en 1 la cantidad de coleccionables que el jugador ha recogido en el nivel. Si ya consiguio todos los coleccionables, carga la escena de victoria.
- **Vacio:** si por alguna extraña razón el jugador se cae al vacío, carga la escena de "Muerte" y se puede mover nuevamente el cursor en toda la pantalla.


### 4. `void finalizarAnimacionDisparo()`
Función de evento configurada desde la animación que permite finalizar la animación de disparo cuando el jugador dispara una bala para regresar, por defecto, a la animación de reposo.

### 5. `void finalizarAnimacionRecarga()`
Función de evento configurada desde la animación que permite finalizar la animación de recarga cuando el jugador presiona la tecla de recarga para regresar, por defecto, a la animación de reposo.

### 6. `void recargarEscudo()`
Función en la que, si el escudo no está lleno y no se está recargando, realiza el conteo de tiempo para que comience la recarga automática luego de pasado dicho lapso. Una vez que se indica el inicio de la recarga automática, la cantidad de escudo y vida van aumentando a cierta velocidad conforme transcurre el tiempo sin exceder el tope de los 100 puntos.

### 7. `void actualizarEscudoUI()`
Función que va actualizando las barras de escudo y vida en pantalla conforme transcurre el tiempo.


## Usos y Consideraciones
1. El canvas para el jugador debe ser un objeto hijo de este.
2. Asignar al objeto del juego el tag "Jugador".