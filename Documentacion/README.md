# Documentación del Proyecto

A continuación, se muestra un listado de las scripts programadas en C# que conforman el funcionamiento del proyecto, con uan breve descripción de la función principal que realizan:

1. [JugadorRigidBody.cs](/Documentacion/JugadorRigidBody.md): controla los movimientos básicos del jugador (caminar y saltar). Lleva el control de las animaciones, audios, y colisiones con los diferentes objetos interactuables del nivel. También controla la barra de vida y escudo del jugador.

2. [GeneradorBalas.cs](/Documentacion/GeneradorBalas.md): genera las balas disparadas por el jugador y las dispara hacia donde este esté mirando.

3. [Bala.cs](/Documentacion/Bala.md): representa a una bala disparada por el jugador, permite a un objeto de bala desactivarse luego de un tiempo para que pueda ser utilizada nuevamente por el jugador.

4. [Enemigo.cs](/Documentacion/Enemigo.md): lleva el control de la vida del enemigo, así como de las colisiones con los objetos en escena y reproducir sus audios característicos.

5. [EnemigoZonaDeteccion.cs](/Documentacion/EnemigoZonaDeteccion.md): controla los movimientos del enemigo en el escenario, así como permitirle disparar hacia el jugador.

6. [BalaEnemiga.cs](/Documentacion/BalaEnemiga.md):representa a una bala disparada por el enemigo, permite a un objeto de bala desactivarse luego de un tiempo para que pueda ser utilizada nuevamente por el enemigo.

7. [BOTONES.cs](/Documentacion/BOTONES.md): script que se encarga de la carga entre escenas y de cerrar el programa.
