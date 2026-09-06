# Ejercicio 02 - ref y out

## Parte A - Intercambiar valores (ref)

Se te pide un método `Intercambiar` que reciba dos números por `ref` y cambie sus valores entre sí (el primero pasa a valer lo que valía el segundo, y viceversa).

```csharp
static void Intercambiar(ref int a, ref int b)
{
    // tu código aquí
}

int x = 3;
int y = 8;

Intercambiar(ref x, ref y);

Console.WriteLine($"x = {x}, y = {y}"); // debe mostrar: x = 8, y = 3
```

## Parte B - Validar una edad (out)

Se te pide un método `TryValidarEdad` que compruebe si una edad es válida (entre 0 y 120, ambos incluidos) y, a través de un parámetro `out`, devuelva un mensaje explicando el resultado.

```csharp
static bool TryValidarEdad(int edad, out string mensaje)
{
    // tu código aquí:
    // - si la edad es válida (0 a 120), mensaje = "Edad válida.", devuelve true
    // - si no es válida, mensaje = "Edad fuera de rango.", devuelve false
}
```

Llama al método con al menos tres edades distintas (una válida, una negativa, una excesivamente alta) y muestra el resultado usando el patrón visto en el tema, marcando de forma distinta el caso de éxito y el de fallo:

```csharp
if (TryValidarEdad(edadDePrueba, out string resultado))
{
    Console.WriteLine($"OK: {resultado}");
}
else
{
    Console.WriteLine($"ERROR: {resultado}");
}
```