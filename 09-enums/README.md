# 09 - Enums

## Qué es un enum

Hay situaciones donde una variable solo puede tomar uno de un conjunto pequeño y conocido de valores relacionados — un estado de un pedido, un día de la semana, un rol de usuario. Representar eso con un `int` (`0` = pendiente, `1` = enviado...) o con un `string` (`"pendiente"`, `"enviado"`...) funciona, pero no evita errores: nada impide asignar `5` a una variable de estado que solo debería tener 4 valores posibles, ni escribir `"Pendiante"` con una falta y que el compilador no se entere.

Un **enum** (enumeración) resuelve esto declarando explícitamente el conjunto cerrado de valores válidos:

```csharp
enum EstadoPedido
{
    Pendiente,
    Enviado,
    Entregado,
    Cancelado
}
```

```csharp
EstadoPedido estado = EstadoPedido.Pendiente;
```

Ahora `estado` solo puede valer uno de esos cuatro nombres — el compilador impide asignar cualquier otra cosa, y no hace falta acordarse de cómo se escribe cada valor porque el propio IDE los sugiere.

## Qué hay por debajo

Cada miembro del enum tiene asociado, por debajo, un valor `int`. Por defecto, empieza en `0` y aumenta de uno en uno en el orden en que se declararon:

```csharp
// Pendiente = 0, Enviado = 1, Entregado = 2, Cancelado = 3
```

Se puede convertir explícitamente a su valor numérico con un **cast**:

```csharp
int valorNumerico = (int)EstadoPedido.Enviado; // 1
```

Y también se pueden asignar valores numéricos propios, en vez de dejar que el compilador los asigne automáticamente — útil cuando el número en sí tiene un significado externo, como un código de estado HTTP:

```csharp
enum CodigoHttp
{
    Ok = 200,
    NoEncontrado = 404,
    ErrorServidor = 500
}
```

## Uso

Un enum se compara igual que cualquier otro valor, y encaja de forma natural en un `switch` (tema 02):

```csharp
EstadoPedido estado = EstadoPedido.Enviado;

if (estado == EstadoPedido.Enviado)
{
    Console.WriteLine("El pedido va en camino");
}

switch (estado)
{
    case EstadoPedido.Pendiente:
        Console.WriteLine("Pendiente de procesar");
        break;
    case EstadoPedido.Enviado:
        Console.WriteLine("En camino");
        break;
    case EstadoPedido.Entregado:
        Console.WriteLine("Entregado");
        break;
    case EstadoPedido.Cancelado:
        Console.WriteLine("Cancelado");
        break;
}
```

## Mostrarlo por consola

```csharp
Console.WriteLine(estado); // "Enviado", no "1"
```

Aunque por debajo cada valor es un `int`, `Console.WriteLine` imprime el nombre del miembro, no el número — es más legible por defecto, sin tener que hacer nada especial para conseguirlo.

## Convertir texto a enum

Conectando con el tema de parsing (BCL): si el nombre de un valor llega como `string` (por ejemplo, leído por consola o desde un archivo), se puede convertir de vuelta a enum con `Enum.Parse`:

```csharp
EstadoPedido estado = Enum.Parse<EstadoPedido>("Enviado");
```

Si el texto no coincide con ningún miembro del enum (por ejemplo, `"enviado"` en minúscula, o una palabra que no existe), lanza una excepción — el manejo adecuado de ese fallo se retoma en el tema de excepciones. Igual que con `int.Parse`, existe una alternativa más segura, `Enum.TryParse`, que se deja para cuando se haya visto `out`, en Métodos II.

## Por qué no usar directamente un int o un string

La ventaja de un enum sobre un `int` "mágico" o un `string` suelto no es solo estética:

- El compilador impide asignar un valor que no está en la lista (con un `int`, nada evita poner `99` sin que tenga ningún significado).
- No hace falta memorizar qué número corresponde a qué estado — el nombre lo dice todo.
- Si en el futuro hace falta añadir un nuevo estado, se añade al enum en un único sitio, y cualquier `switch` que no lo contemple puede detectarse por el propio compilador con un aviso, en vez de descubrirse en producción.

## Convención de nombres

El tipo del enum y cada uno de sus miembros van en PascalCase, igual que cualquier otro tipo (ver `estandares-de-estilo.md`): `EstadoPedido`, `Pendiente`, `Enviado`. El nombre del enum suele ir en singular, describiendo qué representa cada valor individual, no el conjunto (`EstadoPedido`, no `EstadosPedido`).