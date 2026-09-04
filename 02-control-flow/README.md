# 02 - Estructuras de control

Un programa no ejecuta las instrucciones siempre en línea recta: las estructuras de control deciden qué bloque de código se ejecuta según una condición.

## Operadores de comparación

| Operador | Significado |
|---|---|
| `==` | Igual a |
| `!=` | Distinto de |
| `>` | Mayor que |
| `<` | Menor que |
| `>=` | Mayor o igual que |
| `<=` | Menor o igual que |

Todos devuelven un `bool`.

## Operadores lógicos

| Operador | Significado |
|---|---|
| `&&` | Y (AND) — verdadero solo si ambos lados son verdaderos |
| `\|\|` | O (OR) — verdadero si al menos un lado es verdadero |
| `!` | Negación (NOT) — invierte el valor |

`&&` y `||` tienen cortocircuito: si con el primer operando ya se sabe el resultado, el segundo ni se evalúa. Ejemplo: en `false && ObtenerDatos()`, `ObtenerDatos()` nunca se llega a ejecutar.

## if / else if / else

```csharp
int edad = 20;

if (edad < 18)
{
    Console.WriteLine("Menor de edad");
}
else if (edad < 65)
{
    Console.WriteLine("Adulto");
}
else
{
    Console.WriteLine("Jubilado");
}
```

## Operador ternario

Forma corta de un `if/else` que devuelve un valor:

```csharp
string estado = (edad < 18) ? "menor" : "adulto";
```

## switch clásico

```csharp
int diaSemana = 3;
string nombreDia;

switch (diaSemana)
{
    case 1:
        nombreDia = "Lunes";
        break;
    case 2:
        nombreDia = "Martes";
        break;
    case 3:
        nombreDia = "Miércoles";
        break;
    default:
        nombreDia = "Desconocido";
        break;
}
```

Cada `case` necesita `break` (o `return`), si no el compilador da error — a diferencia de otros lenguajes, C# no permite el "fall-through" implícito de un case a otro.

La versión moderna y más compacta (`switch` como expresión, patrones con `is`, etc.) se ve más adelante, una vez este `switch` clásico esté asimilado.