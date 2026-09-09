# Ejercicio 02 - Buscador de empleados

Se te pide simular una búsqueda que puede no encontrar resultado, y trabajar con ese resultado de varias formas.

Dato de partida:

```csharp
string[] empleados = { "Ana", "Luis", "Marta" };
```

Requisitos:

1. Crea un método `string? BuscarEmpleado(string[] nombres, string buscado)` que recorra el array y devuelva el nombre si lo encuentra, o `null` si no está.
2. Llama al método dos veces: una con un nombre que sí existe y otra con uno que no.
3. Para cada llamada, comprueba con un `if` si el resultado es `null`. Muestra un mensaje distinto según el caso ("Encontrado: ..." o "No se encontró a nadie con ese nombre"). Dentro del `if` donde ya se comprobó que no es `null`, usa el resultado directamente (por ejemplo, `resultado.Length`) sin ningún warning — comprueba que el análisis de flujo reconoce la comprobación.
4. Ahora, sin usar el `if` anterior, construye una variable `apodoAMostrar` a partir del resultado de la búsqueda que no encontró nada, usando `??` para que valga `"Sin apodo"` si es `null`.
5. Declara `string? ciudad = "Sevilla";` y usa `??=` para asignarle `"Desconocida"` solo si fuera `null`. Comprueba mostrando `ciudad` antes y después que, al tener ya valor, no cambia.
6. Declara `string? telefono = null;` y usa `?.` para acceder a `telefono.Length` de forma segura, guardando el resultado en una variable `int?`. Muestra ese resultado y comprueba que no lanza excepción aunque `telefono` sea `null`.