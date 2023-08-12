# Documentación de Bala.cs
Componente asignado para cada una de las balas disparadas por algún enemigo. Permite desactivarla pasado un tiempo desde que fue disparada.

## Variables Privadas
### 1. `tiempo_vida` (`private float tiempo_vida = 1.7f`)
Indica cuanto tiempo debe estar activa la bala luego de ser disparada.

### 2. `tiempo_vida_actual` (`private float tiempo_vida_actual`)
Tiempo de vida de la bala en el frame actual.


## Métodos

### 1. `void Update()`
Detecta cada frame del programa si ya transcurrió el tiempo de vida de esta bala para desactivarla en la escena.

### 2. `void OnCollisionEnter()`
Detecta si la bala colisionó con algún objeto. Excluye al enemigo que disparó la bala.

## Usos y Consideraciones
1. Asignar al objeto de juego el tag "Bala Enemiga".