# Halo-El-Anillo-Sagrado
**Equipo:** #1

**Integrantes:**
- Alvar Velázquez de León Lavarrios
- Fernando De la Rosa Flores

**Semestre:** 2024-1

**Torre de Niveles:** No Aplica

## Introducción
Proyecto Final en parejas para el curso de Unity Básico de SODVI, que consiste en el desarrollo de un juego de disparos en primera persona (FPS) con temática de la saga Halo. Como objetivo se espera la creación de un juego funcional generado en Unity 3D.

## Descripción del Proyecto
Este proyecto consiste en un juego de disparos con temática y mecánicas similares a las de la saga de juegos conocida como Halo, en donde el jugador deberá explorar el anillo, eliminar a los pelotones del Covenant y recuperar los índices de activación para evitar que el enemigo active el anillo y destruya la vida en el universo.

El prefabricado que conforma al jugador lo diseñamos de tal manera que la vista de jugador únicamente pueda ver los brazos y la sombra del cuerpo, pero no el cuerpo en sí. Esto lo logramos añadiendo dos modelos 3D del Jefe Mastro, cada uno con su respectiva configuración en el componente *Mesh Renderer*: uno para el cuerpo sólido (y que la cámara no puede ver) y otro para la sombra del cuerpo.

![Imagen 1](/src/Imagen1.png)

Con base a la mecánica de los juegos de Halo, para evitar que el jugador pierda luego de recibir varios disparos enemigos, programamos la barra de vida y escudo correspondientes para que estos se recarguen luego un tiempo determinado. Además, en la interfaz de usuario (UI) implementamos la información correspondiente al arma y a los coleccionables del nivel.

![Imagen 2](/src/Imagen2.png)

Para agregarle dinamismo y variedad al juego, decidimos agregar dos tipos de enemigos: Elite y Grunt (ambos enemigos conocidos de la saga Halo). Ambos tienen un comportamiemto de mirar y disparar cada cierto tiempo al jugador impidiendo que este complete el nivel. El movimiento del enemigo lo programamos de tal manera que se moviese en 5 direcciones diferentes (reposo, izquierda, derecha, adelante y atrás) de manera aleatoria cada cierto tiempo, para dar la impresión de que cada enemigo está realizando un movimiento "inteligente" y no únicamente se quede quieto en una posición en el escenario. La acción de disparos se pudo realizar con la ayuda de una esfera y marcándola como activador (trigger) para que, cuando el jugador esté dentro de la esfera, el enemigo dispare a este.

![Imagen 3](/src/Imagen3.png)

Respecto al sistema de disparos, tanto para el jugador como para los enemigos, implementamos la generación de cada una de las balas con una técnica llamada *"Object Pooling"* (que consiste en cargar una determinada candidad de objetos al inicio de la ejecución del programa) en lugar de crear y destruir a cada rato *GameObjects* en la escena. Decidimos hacerlo de esta forma ya que, en teoría, es una forma de optimización para el CPU.

![Imagen 4](/src/Imagen4.png)

Como objeto de tipo *Terrain* que creamos es relativamente grande, y hay una cantidad considerable de enemigos, para que el jugador no se quede sin munición a mitad de la partida, agregamos un nuevo objeto que consiste en una caja de mmunición: cada vez que el jugador toca una de estas, agrega munición al inventario de balas máximas. Para facilidad ddel usuario, las cajas de munición están colocadas en zonas de sencilla ubicación (cerca de vehículos) para que no pierda tiempo explorando.

![Imagen 5](/src/Imagen5.png)

Para el objetivo principal del juego decidimos que, dentro de la escena del nivel, el jugador recupere 4 objetos coleccionables (estos vienen representados en un item de la saga conocido como "Índice de Activación"). Una vez que el jugador toque los 4 coleccionables, pasará a la escena de victoria la cual representa que se completó el nivel.

![Imagen 6](/src/Imagen6.png)


Por cuestiones de simplicidad (y quizás de sentido común) decidimos colocar los 4 coleccionables en zonas donde es muy probable que el jugador explore (mini-anillos de Halo y balizas Forerruner) para que no pierda mucho tiempo explorando la sección de naturaleza de naturaleza (árboles, colinas, etc.).

![Imagen 7](/src/Imagen7.png)
![Imagen 8](/src/Imagen8.png)

## Conclusiones

- **Alvar Velázquez de León Lavarrios**
Derivado de la realización de este proyecto, puedo decir que logramos desarrollar un juego funcional 3D con mecánica de disparos y que sea entretenido tomando como inspiración una de las sagas más icónicas en dicho género. Aprendimos a importar y configurar modelos 3D al proyecto, logramos realizar la implementación de los prefabricados, configurar el sistema de colisiones, programar la lógica de cada una de las características, las físicas de cada cuerpo dinámico, diseñar la interfaz de usuario y agregar la parte artística del mismo. Todo lo anterior mencionado en un único proyecto elaborado sobre el motor de juegos llamado Unity 3D.

- **Fernando De la Rosa Flores**
Gracias al desarrolo de este proyecto hemos lograo el aprendizaje de distintas funciones de Unity en este caso enfocado a un videojuego en 3D, para ello decidimos que fuera lo mas dinamico posible, para entretener al usuario que es lo mas importante en estos casos. Logramos incluir distintas partes importantes como el uso de vida, daño, cambio de escenas, colisiones, implementaciones de audio, luz y un sin fin de efectos más. Nos inspiramos del juego original para hacer un homenaje en nuestro primer proyecto 3D. Esperando que guste y se pase un buen rato jugandolo.