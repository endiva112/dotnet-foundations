# Ejercicio 01 - Temperatura del día

Se te pide registrar la temperatura de un sensor que a veces no da lectura.

Requisitos:

1. Declara una variable `double?` llamada `temperatura`, inicialízala a `null` (simulando que el sensor no respondió).
2. Intenta imprimir `temperatura` directamente con `Console.WriteLine`. Comprueba que esto compila y no lanza excepción — `null` se imprime como cadena vacía en este caso, no como el número 0. Razona por qué NO hace falta desenvolver el valor solo para imprimirlo.
3. Ahora intenta obtener el valor "de verdad" usando `.Value` sin comprobar antes si tiene contenido. Ejecuta el programa y observa la excepción que lanza.
4. Corrige el punto anterior usando `??` para dar un valor por defecto de `0.0` si `temperatura` es `null`, y muestra el resultado con interpolación.
5. Cambia `temperatura` a un valor real (por ejemplo `23.5`) y comprueba que ahora el `??` no interviene y se usa el valor real.