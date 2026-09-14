# Ejercicio 01 - Clasificador de edad y temperatura

Se te pide reescribir dos clasificaciones usando `switch` como expresión.

Requisitos:

1. Declara un `int edad` con el valor que quieras. Usa un `switch` expression con patrones relacionales para asignar a una variable `string categoria` el valor `"Niño"` (menos de 13), `"Adolescente"` (menos de 20), `"Adulto"` (menos de 65), o `"Mayor"` (el resto). Muestra el resultado.
2. Declara un `double temperatura` con el valor que quieras. Usa otro `switch` expression con patrones relacionales para clasificarla en `"Frío"` (menos de 10), `"Templado"` (menos de 25), o `"Caluroso"` (el resto). Muestra el resultado.
3. Prueba ambos con al menos tres valores distintos cada uno, comprobando que caen en la categoría esperada, incluidos los valores justo en el límite entre dos categorías (por ejemplo, exactamente `13` o exactamente `25`).
4. Encapsula cada clasificación en su propio método (una responsabilidad por método, tema 06) que reciba el valor y devuelva el `string` correspondiente, usando expression-bodied member (`=>`) para el cuerpo del método, ya que consiste en una única expresión.