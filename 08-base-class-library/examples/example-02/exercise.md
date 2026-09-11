# Ejercicio 02 - Normalizador de nombre de usuario

Se te pide leer un nombre de usuario por consola y limpiarlo antes de usarlo.

Requisitos:

1. Pide al usuario que introduzca su nombre con `Console.Write`, y lee la respuesta con `Console.ReadLine()`.
2. Comprueba si lo introducido está vacío, es solo espacios, o es `null`, usando el método adecuado para ello (recuerda el tema anterior). Si es así, muestra un mensaje de error y no sigas con el resto del ejercicio.
3. Si el contenido es válido, quita los espacios sobrantes al principio y al final.
4. Genera una versión en mayúsculas y otra en minúsculas del nombre ya limpio, y muestra ambas.
5. Si el nombre contiene un espacio (nombre y apellido, por ejemplo "Ana Garcia"), divide el texto en sus partes y muestra cada una por separado, numerada ("Parte 1: Ana", "Parte 2: Garcia"). Si no contiene ningún espacio, muestra un mensaje indicando que solo se introdujo una palabra.

Prueba el programa al menos tres veces: con un nombre normal, con una sola palabra, y dejando la entrada vacía (pulsando Enter sin escribir nada).