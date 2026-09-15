# Ejercicio 03 - Registro de estudiantes

Se te pide modelar estudiantes que reciben un identificador único automáticamente al crearse.

Requisitos:

1. Crea una clase `Estudiante` con una propiedad `Nombre` (`string`) y una propiedad `Id` (`int`) con `get` público y `set` `private`.
2. Añade un campo `static` `private` que lleve la cuenta de cuántos estudiantes se han creado en total.
3. En el constructor (que recibe `Nombre`), incrementa ese contador estático y asigna el nuevo valor a la propiedad `Id` de la instancia — de forma que el primer estudiante creado tenga `Id = 1`, el segundo `Id = 2`, y así sucesivamente, sin que nadie tenga que indicarlo manualmente.
4. Añade un método `static` `public` llamado `ObtenerTotalEstudiantes()` que devuelva el valor del contador.
5. Desde el punto de entrada del programa, crea al menos 4 instancias distintas de `Estudiante`, y muestra el `Nombre` e `Id` de cada una.
6. Muestra también el resultado de `Estudiante.ObtenerTotalEstudiantes()` (llamado sobre la clase, no sobre ninguna instancia) y comprueba que coincide con el número de estudiantes creados.