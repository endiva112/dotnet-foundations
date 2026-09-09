# Ejercicio 02 - Estadísticas de notas

Se te pide calcular y mostrar la nota más alta de un grupo de alumnos, respetando el principio de una responsabilidad por método.

Dato de partida:

```csharp
int[] notas = { 6, 8, 4, 9, 7 };
```

Requisitos:

1. Crea un método `CalcularMaxima(int[] valores)` que devuelva únicamente el valor más alto del array, usando un bucle (tema 04) para recorrerlo — no vale usar ningún método que ya lo resuelva por ti (eso llegará más adelante, en el tema de librerías).
2. Crea un segundo método `DescribirNotaMaxima(int[] notas)` que llame a `CalcularMaxima` y devuelva un `string` con un mensaje, por ejemplo: `"La nota más alta es un 9."`
3. `CalcularMaxima` no debe construir ningún string ni imprimir nada — solo calcular y devolver el número. Toda la parte de "convertir esto en un mensaje legible" es responsabilidad exclusiva de `DescribirNotaMaxima`.
4. Desde el método principal, llama solo a `DescribirNotaMaxima` y muestra su resultado por consola.

Antes de terminar, prueba a llamar también a `CalcularMaxima` por separado y mostrar el número puro, para comprobar que de verdad puedes reutilizarlo sin pasar por el string.