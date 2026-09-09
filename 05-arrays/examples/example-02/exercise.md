# Ejercicio 02 - Clasificador de temperaturas

Se te pide clasificar una semana de temperaturas.

Dato de partida:

```csharp
double[] temperaturas = { 5.5, 14.0, 22.3, 30.1, 18.7, 9.2, 25.0 };
```

Requisitos:

1. Recorre el array con `for` (necesitas el índice para numerar el día).
2. Para cada temperatura, usa `if / else if / else` (tema 02) para clasificarla:
   - Menos de 10 → `"Frío"`
   - Entre 10 y 24.9 → `"Templado"`
   - 25 o más → `"Caluroso"`
3. Muestra cada resultado con interpolación de strings, con este formato:

```
Día 0: 5.5° - Frío
Día 1: 14° - Templado
...
```

4. Al terminar el bucle, muestra cuántos de los 7 días fueron "Caluroso" (puedes usar un contador que incrementes dentro del bucle).

Este ejercicio reutiliza arrays + `for` (tema 04) + `if/else if/else` (tema 02) + interpolación (tema 03), todo junto.