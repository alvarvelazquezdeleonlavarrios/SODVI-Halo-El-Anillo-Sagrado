# Documentación de EnemigoZonaDeteccion.cs
Script que permite detectar si un jugador se encuentra dentro del rango de "visión" de un enemigo, además de llevar a cabo la lógica de los disparos y movimientos "inteligentes" del enemigo.

## Variables Públicas
### 1. `enemigo` (`[SerializeField] private Enemigo enemigo`)
Enemigo en cuestión al cual hace referencia para poder moverlo "inteligentemente".

### 2. `enemigo_generador_bala` (`[SerializeField] private Transform enemigo_generador_bala`)
Objeto vacío por el cual se disparan las balas enemigas.

### 3. `velocidad_movimiento` (`[SerializeField] private float velocidad_movimiento = 3.6f`)
Cantidad de movimiento con la cual se mueve el enemigo.

### 4. `tiempo_movimiento` (`[SerializeField] private float tiempo_movimiento = 2f`)
Intervalo de tiempo con el cual el enemigo se mueve en una dirección aleatoria.

### 5. `prefab_bala_enemiga` (`[SerializeField] private BalaEnemiga prefab_bala_enemiga`)
Prefabricado de la bala a disparar.

### 6. `velocidad_disparo` (`[SerializeField] private float velocidad_disparo = 20f`)
Velocidad con la cual se disparan las balas.

### 7. `tiempo_cadencia_disparo` (`[SerializeField] private float tiempo_cadencia_disparo = 1.5f`)
Intervalo de tiempo con el cual se dispara cada una de las balas enemigas.

### 8. `prefab_sonido_disparo` (`[SerializeField] private AudioSource prefab_sonido_disparo`)
Prefabricado que contiene el sonido de disparo de una bala enemiga.


## Variables Privadas
### 1. `lista_balas` (`private BalaEnemiga[] lista_balas`)
Arreglo que contiene las balas generadas al inicio del juego.

### 2. `lista_audios_disparo` (`private AudioSource[] lista_audios_disparo`)
Arreglo que contiene los audios de disparo generados al inicio del juego.

### 3. `tamano_lista_audios_disparo` (`private int tamano_lista_audios_disparo = 6`)
Tamaño inicial del arreglo de audios.

### 4. `tiempo_cadencia_actual` (`private float tiempo_cadencia_actual = 0f`)
Tiempo de espera en el frame actual para disparar la siguiente bala enemiga.

### 5. `bala_generada` (`private BalaEnemiga bala_generada`)
Variable temporal para almacenar cada nueva bala en el arreglo de balas.

### 6. `rb_bala` (`private Rigidbody rb_bala`)
Componente de física de la bala en cuestión.

### 7. `tamano_lista_balas` (`private int tamano_lista_balas = 18`)
Tamaño inicial del arreglo de balas.

### 8. `movimiento` (`Vector3 movimiento`)
Vector de movimiento para el enemigo.

### 9. `jugador_detectado` (`private bool jugador_detectado = false`)
Detecta si un jugador está cerca del enemigo.

### 10. `tiempo_movimiento_actual` (`private float tiempo_movimiento_actual = 0f`)
Intervalo de tiempo en el frame actual para elegir el siguiente movimiento aleatorio.

### 11. `movimiento_aleatorio` (`private int movimiento_aleatorio = 0`)
Selecciona un movimiento aleatorio dentro de un rango.

## Métodos

### 1. `void Start()`
Inicializa los arreglos de balas y audio, generando las cantidades correspondientes.

### 2. `void Update()`
Si se detecta a un jugador cerca, cada frame del juego va moviendo al perosnaje en una dirección aleatoria. Pasado un tiempo, selecciona otro movimiento aleatorio.

### 3. `void OnTriggerEnter(Collider other)`
Detecta si un jugador se acercó al enemigo.

### 4. `void OnTriggerExit(Collider other)`
Detecta si un jugador se alejó al enemigo.

### 5. `void OnTriggerStay(Collider other)`
Detecta si un jugador sigue cerca del enemigo. Si es así, el enemigo mira al jugador y dispara a este.

### 6. `void mirarAJugador(GameObject jugador)`
Rota al objeto de juego del enemigo, de tal forma que apunte a la posición del jugador, pero lo adecue a la altura del enemigo.

### 7. `void dispararAJugador(GameObject jugador)`
Permite al generador de balas apuntar a la dirección exacta de donde se encuentra el jugador. Cada cierto tiempo va disparando una bala. Para ello, debe verificar que hay balas disponibles en el arreglo de balas generadas al inicio del juego.

### 8. `BalaEnemiga obtenerBalaDisponible()`
Mediante la técnica de Object Pooling, obtiene la bala más próxima disponible para ser utilizada.

### 9. `void reproducirAudioDisponible()`
Mediante la técnica de Object Pooling, reproduce el audio de disparo más próximo disponible.


## Usos y Consideraciones
1. Asignar este componente a una esfera con *sphere collider* y configurarlo como *trigger*.
2. La esfera debe ser objeto hijo del enemigo.
