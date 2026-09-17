# Ejercicio 03 - Rango por defecto

Se te pide comprobar la diferencia entre `default` y un constructor sin parámetros personalizado.

Requisitos:

1. Sobre el `struct Rango` de los ejercicios anteriores, añade un constructor sin parámetros propio que fije `Minimo = 0` y `Maximo = 100` (en vez de dejar que ambos queden en `0`).
2. Crea una instancia con `Rango a = new Rango();` y muestra sus valores — deberían ser `0` y `100`.
3. Crea otra instancia con `Rango b = default;` y muestra sus valores — deberían ser `0` y `0`.
4. Explica en un comentario, con tus propias palabras, por qué `a` y `b` no tienen los mismos valores a pesar de que ambas líneas "parecen" crear un rango vacío.