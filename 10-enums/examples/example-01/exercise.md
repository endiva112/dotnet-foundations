# Ejercicio 01 - Talla de una prenda

Se te pide representar la talla de una prenda de ropa.

Requisitos:

1. Declara un enum `Talla` con los valores `XS`, `S`, `M`, `L` y `XL`, en ese orden. Puedes resolver este ejercicio como file-based app o como proyecto real (tema 09) — si eliges proyecto real, declara el enum en su propio archivo.
2. Declara una variable de tipo `Talla` con el valor `M`.
3. Usa un `switch` clásico sobre esa variable para mostrar un mensaje distinto según el valor: `"Muy pequeña"` para `XS`, `"Pequeña"` para `S`, `"Mediana"` para `M`, `"Grande"` para `L`, `"Muy grande"` para `XL`.
4. Muestra por consola la variable directamente con `Console.WriteLine`, y comprueba qué se imprime.
5. Convierte esa misma variable a su valor numérico subyacente con un cast, y muéstralo también.
6. Cambia la variable a `XL` y repite los puntos 3, 4 y 5 para comprobar que el resultado cambia en consecuencia.