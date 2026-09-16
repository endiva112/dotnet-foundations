# Ejercicio 01 - Calculadora con sobrecarga

Se te pide ampliar una calculadora para que acepte distintos tipos y cantidades de datos usando sobrecarga.

Requisitos:

1. Crea una clase `Calculadora` con un método `Sumar(int a, int b)` que devuelva un `int`.
2. Añade una sobrecarga `Sumar(double a, double b)` que devuelva un `double`.
3. Añade una tercera sobrecarga `Sumar(int a, int b, int c)` que sume los tres y devuelva un `int`.
4. Desde el punto de entrada del programa, crea una instancia de `Calculadora` y llama a las tres sobrecargas con datos distintos, comprobando que el compilador elige la versión correcta según los argumentos.
5. Intenta añadir una cuarta sobrecarga `double Sumar(int a, int b)` (mismos parámetros que la primera, pero devolviendo `double`) y comprueba que no compila. Anota, en un comentario, el error exacto que da el compilador.