# Ejercicio 01 - Rango numérico

Se te pide modelar un rango numérico simple y comprobar su semántica de valor.

Requisitos:

1. Crea un `struct Rango` con dos propiedades `Minimo` y `Maximo` (`int`, con `get`/`set`), y un constructor que reciba ambos valores.
2. Crea dos instancias de `Rango` con los mismos valores (por ejemplo, ambas `Minimo = 1, Maximo = 10`), asignando la segunda a partir de la primera (`Rango b = a;`).
3. Modifica `Maximo` en una de las dos instancias, y muestra ambas por consola para comprobar que son independientes — cambiar una no afecta a la otra.
4. Intenta comparar las dos instancias con `==` y comprueba que no compila. Anota en un comentario el mensaje de error exacto.
5. Compara las dos instancias con `.Equals()` en su lugar, y muestra el resultado por consola.