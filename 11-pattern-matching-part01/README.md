# 11 - Pattern matching I

## El problema con el switch clásico como asignación

El `switch` clásico (tema 02) es una instrucción, no una expresión — no produce un valor, solo ejecuta código. Esto obliga a un patrón repetitivo cuando lo único que se quiere es asignar una variable según un caso:

```csharp
string mensaje;

switch (talla)
{
    case Talla.XS:
        mensaje = "Muy pequeña";
        break;
    case Talla.S:
        mensaje = "Pequeña";
        break;
    default:
        mensaje = "Desconocida";
        break;
}
```

Se necesita usar repetidamente las palabras clave (`case`, `break`, repetidas por cada caso) solo para decir "esto vale una cosa u otra según el valor".
C# moderno ofrece una forma más directa de expresar exactamente eso.

## `switch` como expresión

```csharp
string mensaje = talla switch
{
    Talla.XS => "Muy pequeña",
    Talla.S => "Pequeña",
    Talla.M => "Mediana",
    Talla.L => "Grande",
    Talla.XL => "Muy grande",
    _ => "Desconocida"
};
```

Diferencias clave respecto al `switch` clásico:

- El valor a comparar va **antes** de la palabra `switch`, no entre paréntesis después.
- Cada caso es `patrón => valor`, sin `case` ni `break` — es una expresión, así que el propio `switch` "vale" el resultado del caso que coincide, y se puede asignar directamente.
- `_` es el **patrón discard**: equivale al `default` del switch clásico, captura cualquier valor que no coincidiera con los casos anteriores.

Si no se cubre algún caso posible y tampoco hay `_`, el compilador avisa — igual que el switch clásico avisa si falta un caso de un enum, pero aquí es todavía más natural porque la expresión necesita devolver algún valor sí o sí.

## El operador `is`

`is` comprueba si un valor coincide con un patrón, y el resultado es un `bool` — se usa igual que cualquier otra condición, por ejemplo dentro de un `if`. La forma más simple es comparar con un valor exacto:

```csharp
if (talla is Talla.M)
{
    Console.WriteLine("Es talla mediana");
}
```

En este caso concreto, `talla is Talla.M` se comporta igual que `talla == Talla.M` — mismo resultado. La diferencia real aparece en cuanto el patrón deja de ser un simple valor exacto: un rango relacional, una combinación de condiciones, o (más adelante, con clases) un tipo. `is` es la palabra clave que da acceso a todas esas variantes; `==` solo sabe comparar igualdad exacta.

## Patrones relacionales

Dentro de una expresión `switch` (o de un `is`), se puede comparar directamente con operadores relacionales, sin repetir la variable en cada rama:

```csharp
string categoria = edad switch
{
    < 13 => "Niño",
    < 20 => "Adolescente",
    < 65 => "Adulto",
    _ => "Mayor"
};
```

Cada patrón se evalúa en orden, de arriba hacia abajo, y se queda con el primero que coincide — por eso `< 20` ya puede asumir que `edad` no es menor de 13 (ese caso se descartó en la línea anterior).

## Combinar patrones: `and`, `or`, `not`

```csharp
bool esValido = numero switch
{
    > 0 and < 100 => true,
    _ => false
};
```

`and`, `or` y `not` combinan patrones dentro de la misma rama, con el significado que su nombre sugiere. También funcionan con `is`, fuera de un `switch`:

```csharp
if (numero is > 0 and < 100)
{
    Console.WriteLine("Está en rango");
}
```

Esto es más directo que escribir `numero > 0 && numero < 100` — mismo resultado, sin repetir el nombre de la variable en cada mitad de la condición.

## `is null` / `is not null`

Conectando con NRT (tema 07): además de `== null` y `!= null`, existe la forma con patrones:

```csharp
if (valor is null)
{
    Console.WriteLine("No hay valor");
}

if (valor is not null)
{
    Console.WriteLine("Hay un valor");
}
```

Hacen exactamente lo mismo que `== null` / `!= null`. La razón por la que muchos estilos de código prefieren `is null` es sutil: `==` se puede sobrecargar (redefinir su comportamiento en un tipo propio, algo que se ve más adelante), así que en casos raros `algo == null` podría no comportarse como se espera si ese tipo redefinió el operador. `is null` comprueba la identidad real, sin pasar por esa sobrecarga. Para el código de este curso, ambas formas son intercambiables; se menciona `is null` porque es habitual encontrarla en código ajeno.

## Aplicándolo a lo ya visto

La expresión `switch` encaja de forma natural con enums (tema 10), sustituyendo el `switch` clásico cuando el único objetivo es obtener un valor:

```csharp
string Describir(NivelAcceso nivel) => nivel switch
{
    NivelAcceso.Invitado => "Acceso limitado",
    NivelAcceso.Usuario => "Acceso estándar",
    NivelAcceso.Administrador => "Acceso completo",
    _ => "Nivel desconocido"
};
```

Nótese que este método, al tener un cuerpo de una sola expresión, se escribe con `=>` (expression-bodied member, tema 06) — dos usos distintos del mismo símbolo `=>` en la misma línea, uno para el cuerpo del método y otro dentro del `switch`. No hay que confundirlos: el primero es sintaxis de método, el segundo es sintaxis de patrón.

## Otros usos no incluidos en este tema

Los patrones de tipo (`is Perro perro`) y los patrones de propiedad (`is { Estado: NivelAcceso.Administrador }`) necesitan clases y, sobre todo, herencia para tener sentido real — se retoman en Pattern Matching II.