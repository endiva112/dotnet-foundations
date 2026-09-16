# Ejercicio 02 - Intercambio de valores y reparto de caramelos

Se te pide practicar `ref` y `out` con dos escenarios distintos.

Requisitos:

**Parte A — `ref`:**

1. Crea un método `static void Intercambiar(ref int a, ref int b)` que intercambie los valores de las dos variables recibidas.
2. Declara dos variables `int`, muéstralas, llama a `Intercambiar`, y vuelve a mostrarlas para comprobar que se han intercambiado.

**Parte B — `out`:**

3. Crea un método `static void RepartirCaramelos(int totalCaramelos, int totalNinos, out int caramelosPorNino, out int caramelosSobrantes)` que calcule cuántos caramelos le tocan a cada niño (división entera) y cuántos sobran (resto de la división).
4. Llama al método con al menos dos combinaciones distintas de valores, mostrando ambos resultados con interpolación de strings.
5. Usa `int.TryParse` sobre un texto que sepas que no es un número válido (por ejemplo, `"abc"`), capturando el resultado con `out`, y muestra si la conversión tuvo éxito o no según el `bool` que devuelve, sin usar `int.Parse` ni gestionar ninguna excepción.