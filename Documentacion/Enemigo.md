# Documentación de Enemigo.cs
Componente agregada a los prefabricados de enemigos que se encarga de almacenar la vida del enemigo y reproducir sus respectivos audios.

## Variables Públicas
### 1. `vida` (`[SerializeField] private float vida`)
Representa la cantidad de vida del enemigo.

### 2. `fuente_audio` (`[SerializeField] private AudioSource fuente_audio`)
Objeto que permite reproducir los sonidos del enemigo.

### 2. `audios_enemigo` (`[SerializeField] private AudioSource[] audios_enemigo`)
Audios del enemigo.

### 4. `tiempo_espera_audio` (`[SerializeField] private float tiempo_espera_audio = 3.2f`)
Intervalo de tiempo para reproducir cada audio del enemigo.

## Variables Privadas
### 1. `vida` (`[SerializeField] tiempo_espera_audio_actual = 0f`)
Tiempo actual para determinar cuando reproducir el siguiente audio.

## Métodos

### 1. `void Update()`
Detecta cada frame cuando se debe reproducir el siguiente audio aleatorio del enemigo.

### 2. `void OnCollisionEnter()`
Detecta si una bala del jugador colisionó con este enemigo para restarle vida o "matarlo". O bien, detecta si, por alguna razón, se cae al vacío.

## Usos y Consideraciones
1. Asignar al objeto de juego el tag "Enemigo".