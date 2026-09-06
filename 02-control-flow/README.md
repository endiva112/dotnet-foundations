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

## Ámbito de las variables (scope)

Antes de ver los tipos de estructuras de control, debemos comprender un concepto importante, el **scope**, o ámbito de variable.
Toda variable declarada dentro de un bloque (`{ }`) — un `if`, un bucle, un `switch`... — deja de existir al cerrarse esa llave. Esto no es exclusivo de C#, pero conviene mencionarlo para evitar posibles dudas y errores de compilación:

```csharp
if (edad < 18)
{
    string mensaje = "Menor de edad";
    Console.WriteLine(mensaje);
}

Console.WriteLine(mensaje); // error: "mensaje" no existe aquí, murió al cerrar el if
```

Si el valor hace falta fuera del bloque, la variable ha de ser declarada fuera de dicho bloque, aunque el valor se le de dentro:

```csharp
string mensaje;

if (edad < 18)
{
    mensaje = "Menor de edad";
}
else
{
    mensaje = "Adulto";
}

Console.WriteLine(mensaje); // aquí sí existe
```

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

Forma corta de un `if/else` que, a diferencia de `if/else`, es una **expresión**: no ejecuta una acción, produce un valor que se puede asignar, pasar como argumento o interpolar directamente.

```csharp
string estado = (edad < 18) ? "menor" : "adulto";
```

Se lee como: *condición* `?` *valor si es verdadera* `:` *valor si es falsa*.

Es equivalente a:

```csharp
string estado;
if (edad < 18)
{
    estado = "menor";
}
else
{
    estado = "adulto";
}
```

Ambos lados del `?:` deben producir un valor del mismo tipo (o de tipos compatibles entre sí) — no se puede devolver un `string` en un lado y un `int` en el otro.

Se puede anidar (`condicion1 ? valor1 : condicion2 ? valor2 : valor3`), pero se vuelve difícil de leer muy rápido. Como norma práctica: si hace falta más de un nivel, mejor usar `if/else if/else` normal.

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
    case 3: //Esto es un ejemplo de valores emparejados, el resultado será miércoles sin importar si el valor es 3 o 4.
    case 4:
        nombreDia = "Miércoles";
        break;
    default:
        nombreDia = "Desconocido";
        break;
}
```

Cada `case` necesita `break` (o `return`), si no el compilador da error — a diferencia de otros lenguajes, C# no permite el "fall-through" implícito de un case a otro.

La versión moderna y más compacta (`switch` como expresión, patrones con `is`, etc.) se ve más adelante, una vez este `switch` clásico esté asimilado.