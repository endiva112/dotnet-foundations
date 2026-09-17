# Ejercicio 02 - Ajustar un rango

Se te pide modificar un `Rango` (el mismo struct del ejercicio 01) a través de un método, y comprobar los límites de la mutabilidad de un struct.

Requisitos:

1. Crea un método `static void Ampliar(Rango rango, int cantidad)` que aumente `Maximo` en `cantidad`, **sin** usar `ref`. Llámalo sobre una instancia y comprueba que el cambio **no** se refleja fuera del método — muestra el valor de `Maximo` antes y después de la llamada.
2. Ahora cambia la firma a `static void Ampliar(ref Rango rango, int cantidad)`, ajusta la llamada para usar `ref`, y comprueba que esta vez el cambio sí se refleja fuera del método.
3. Crea un array `Rango[] rangos` con al menos 3 elementos. Intenta modificar `Maximo` de un elemento dentro de un `foreach`, y comprueba que no compila. Anota el código de error.
4. Consigue el mismo resultado del punto 3 (modificar `Maximo` de cada elemento) usando un bucle `for` con índice en su lugar, y comprueba que esta vez sí funciona.