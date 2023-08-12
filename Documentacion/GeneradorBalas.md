# Documentación de GeneradorBalas.cs

## Variables Públicas
### 1. `jugador` (`[SerializeField] private JugadorRigidBody jugador`)
Jugador en cuestión al cual hace referencia para almacenar las balas del arma.

### 2. `prefab_bala` (`[SerializeField] private Bala prefab_bala`)
Prefabricado de la bala a disparar.

### 3. `texto_balas_actuales` (`[SerializeField] private Text texto_balas_actuales`)
Texto en pantalla que imprime la cantidad de balas del cargador actual.

### 4. `texto_balas_maximas` (`[SerializeField] private Text texto_balas_maximas`)
Texto en pantalla que imprime la cantidad de balas disponibles en el inventario.

### 5. `sonido_recarga` (`[SerializeField] private AudioSource sonido_recarga`)
Fuente de audio que se reproduce cada vez que el jugador recarga.

### 6. `sonido_recoger_municion` (`[SerializeField] private AudioSource sonido_recoger_municion`)
Fuente de audio que se reproduce cada vez que el jugador recoge munición del suelo.

### 7. `prefab_sonido_disparo` (`[SerializeField] private AudioSource prefab_sonido_disparo`)
Prefabricado que contiene el sonido de disparo de una bala de jugador.


## Variables Privadas
### 1. `lista_balas` (`private Bala[] lista_balas`)
Arreglo que contiene las balas generadas al inicio del juego.

### 2. `tamano_lista_balas` (`private int tamano_lista_balas = 20`)
Tamaño inicial del arreglo de balas.

### 3. `lista_audios_disparo` (`private AudioSource[] lista_audios_disparo`)
Arreglo que contiene los audios de disparo generados al inicio del juego.

### 4. `tamano_lista_audios_disparo` (`private int tamano_lista_audios_disparo = 25`)
Tamaño inicial del arreglo de audios.

### 5. `animator_jugador` (`private Animator animator_jugador`)
Componente de animaciones del jugador.

### 6. `bala_generada` (`private Bala bala_generada`)
Variable temporal para almacenar cada nueva bala en el arreglo de balas.

### 7. `rb_bala` (`private Rigidbody rb_bala`)
Componente de física de la bala en cuestión.

### 8. `velocidad_disparo` (`private float velocidad_disparo = 70f`)
Velocidad con la cual se disparan las balas.

### 9. `balas_actuales` (`private int balas_actuales = 32`)
Cantidad de balas en el cargador actual.

### 10. `balas_maximas` (`private int balas_maximas = 108`)
Cantidad de balas disponibles en el inventario

### 11. `tiempo_recarga` (`private float tiempo_recarga = 1.5f`)
Tiempo que tarda el jugador en recargar su arma.

### 12. `tiempo_recarga_actual` (`private float tiempo_recarga_actual = 0f`)
Tiempo actual de recarga del arma. Llegado al tope de tiempo, se recarga el arma.

### 13. `recargando` (`private bool recargando = false`)
Variable que indica si se está recargando en el momento actual.


## Métodos

### 1. `void Start()`
Inicializa los arreglos de balas y audio, generando las cantidades correspondientes. Actualiza la cantidad de balas en pantalla.

### 2. `void Update()`
Función que se ejecuta cada frame y que detecta si el jugador presionó el clic izquierdo del ratón para disparar, la tecla R para recargar el arma o si el jugador se encuentra recargando en el momento actual fara finalizar la acción de recarga.

### 3. `void disparar()`
Función en la que, si el jugador tiene balas en el cargador actual y no se encuentra recargando, busca una bala disponible mediante *Object Pooling* para dispararla en la dirección la cual apunta el jugador, activar la animación de disparo, reproducir el audio de disparo y actualizar la cantidad de balas en pantalla. Si se acabó el cargador completo, se recarga automáticamente.

### 4. `void recargar()`
Función en la que, si el cargador actual no está lleno, hay balas disponibles en el inventario y no se está recargando, el jugador ejecuta la animación de cargar y se indica que está recargando el arma.

### 5. `void recarga()`
Función que recarga el arma con la cantidad de balas necesarias para llenar o tratar de llenar el cargador actual y restando dicha cantidad a las balas en inventario. Actualiza la cantidad de balas en pantalla.

### 6. `Bala obtenerBalaDisponible()`
Mediante la técnica de Object Pooling, obtiene la bala más próxima disponible para ser utilizada.

### 7. `void reproducirAudioDisponible()`
Mediante la técnica de Object Pooling, reproduce el audio de disparo más próximo disponible.

### 8. `void actualizarBalasUI()`
Función que actualiza los textos en pantalla de balas del cargador actual y balas disponibles en el inventario.

### 9. `void agregarMunicionMaxima()`
Función que se ejecuta cuando el objeto jugador toca una caja de munición, agregando una cantidad correspondiente a las balas en inventario sin exceder un tope máximo. Reproduce el sonido de munición recogida, actualiza la información en pantalla de balas y destruye el objeto cargador.

## Usos y Consideraciones
1. Asignar esta script como componente de un objeto vacío.
2. Dicho objeto vacío debe ser objeto hijo del jugador.