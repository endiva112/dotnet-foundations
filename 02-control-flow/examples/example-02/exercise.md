# Ejercicio 02 - Menú del día

Se te pide mostrar el nombre del día de la semana y si es fin de semana o no.

Dato de partida:

```csharp
int diaSemana = 6; // 1 = Lunes ... 7 = Domingo
```

Requisitos:

1. Usa un `switch` clásico para traducir el número (`1`-`7`) a su nombre (`"Lunes"`, `"Martes"`, ... `"Domingo"`). Si el número no está entre 1 y 7, usa el `default` para asignar `"Día inválido"`.
2. Usa el operador ternario para decidir si es fin de semana (sábado o domingo) o no, y guarda el resultado en una variable de tipo `string` (por ejemplo `"Es fin de semana"` / `"Es día laborable"`).
3. Muestra el resultado con `Console.WriteLine` e interpolación de strings, algo como:

```
Hoy es Sábado. Es fin de semana.
```

Reutiliza lo visto en el tema de variables: tipa correctamente cada dato y usa interpolación (`$"..."`) para el mensaje final.