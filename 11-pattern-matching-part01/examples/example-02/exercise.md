# Ejercicio 02 - Validador de entrada

Se te pide validar un número y un texto introducidos por consola.

Requisitos:

1. Pide al usuario un número entero por consola y conviértelo con `int.Parse`.
2. Usa `is` con los combinadores `and`/`or` (sin `&&` ni `||`) para comprobar si el número está fuera de un rango válido de `1` a `100` (es decir, si es menor que `1` **o** mayor que `100`), y muestra un mensaje distinto según esté dentro o fuera de rango.
3. Pide al usuario un texto por consola con `Console.ReadLine()` y guárdalo en una variable `string?`.
4. Usa `is null` (no `== null`) para comprobar si el texto introducido es `null`, y `is not null` en el caso contrario, mostrando un mensaje distinto en cada caso.
5. Si el texto no es `null`, usa `is` con `not` para comprobar que el texto no está vacío (`not ""`), mostrando un mensaje distinto si lo está.