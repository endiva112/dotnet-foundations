# Ejercicio 01 - Semáforo

Se te pide simular el comportamiento de un semáforo.

Requisitos:

1. Declara un enum `Semaforo` con los valores `Rojo`, `Amarillo` y `Verde`, en ese orden.
2. Declara una variable de tipo `Semaforo` con el valor `Rojo`.
3. Usa un `switch` clásico (tema 02) sobre esa variable para mostrar un mensaje distinto según el valor: `"Detente"` para `Rojo`, `"Prepárate"` para `Amarillo`, `"Avanza"` para `Verde`.
4. Muestra por consola la variable directamente con `Console.WriteLine`, y comprueba qué se imprime (el nombre, no un número).
5. Convierte esa misma variable a su valor numérico subyacente con un cast, y muéstralo también.
6. Cambia la variable a `Verde` y repite los puntos 3, 4 y 5 para comprobar que el resultado cambia en consecuencia.