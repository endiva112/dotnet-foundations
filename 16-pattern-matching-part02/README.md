# 16 - Pattern Matching II

## Patrones de tipo

En el tema de Herencia y polimorfismo, comprobar el tipo real de un objeto y convertirlo exigía dos pasos por separado:

```csharp
if (dispositivo is Smartphone)
{
    Smartphone telefono = (Smartphone)dispositivo;
    telefono.TomarFoto();
}
```

Un **patrón de tipo** hace ambas cosas en un solo paso: comprueba el tipo y, si coincide, declara una variable ya convertida a ese tipo, visible dentro del propio bloque:

```csharp
if (dispositivo is Smartphone telefono)
{
    telefono.TomarFoto();
}
```

`telefono` solo existe dentro del bloque donde la comprobación tuvo éxito — es el mismo análisis de flujo que ya se vio con `is not null` en el tema de NRT, aplicado ahora a un tipo concreto en vez de a la ausencia de valor. Si `dispositivo` no es realmente un `Smartphone`, la condición es `false` y `telefono` nunca llega a existir; no hay ningún cast expuesto que pueda lanzar una `InvalidCastException` en tiempo de ejecución, porque el propio patrón ya se ha encargado de comprobarlo antes de crear la variable.

Esto también funciona con `is not`:

```csharp
if (dispositivo is not Smartphone telefono)
{
    return;
}

telefono.TomarFoto(); // aquí sí existe: si el "is not" fue false, es que sí era un Smartphone
```

Y, como cualquier patrón, encaja igual de bien en una expresión `switch` (tema de Pattern Matching I):

```csharp
string Describir(Dispositivo dispositivo) => dispositivo switch
{
    Portatil p => $"Portátil de {p.TamanioPantalla} pulgadas",
    SmartphoneGamer g => $"Smartphone gaming a {g.TasaRefresco} Hz",
    Smartphone s => $"Smartphone con {s.CapacidadAlmacenamiento} GB",
    _ => "Dispositivo desconocido"
};
```

Nótese el orden: `SmartphoneGamer` se comprueba **antes** que `Smartphone`, aunque `SmartphoneGamer` también sea un `Smartphone` (hereda de él). Cada patrón se evalúa de arriba hacia abajo y se queda con el primero que coincida (regla ya vista en Pattern Matching I) — si `Smartphone s` fuera la primera línea, capturaría también a los `SmartphoneGamer`, y la rama de `SmartphoneGamer g` nunca se alcanzaría. Cuando se hace pattern matching sobre una jerarquía, los tipos más derivados van primero.

Además de `object`, un patrón de tipo también sirve para distinguir entre distintas implementaciones de una misma interfaz, aunque las interfaces todavía no se hayan visto en el temario — no hace falta nada más para usarlo con clases, que es donde se aplica aquí.

## Patrones de propiedad

Un patrón de tipo responde a "¿es de este tipo?". Un **patrón de propiedad** responde, además, a "¿y tiene esta propiedad con este valor?" — combinando ambas comprobaciones en una sola expresión, sin necesidad de acceder a la propiedad por separado después:

```csharp
if (dispositivo is Smartphone { CapacidadAlmacenamiento: 256 })
{
    Console.WriteLine("Smartphone de 256 GB");
}
```

Esto comprueba a la vez que `dispositivo` es un `Smartphone` y que su propiedad `CapacidadAlmacenamiento` vale exactamente `256`. Si hace falta también la variable ya convertida (no solo comprobar la condición), se declara igual que con un patrón de tipo simple, después de la llave:

```csharp
if (dispositivo is Smartphone { CapacidadAlmacenamiento: 256 } telefono)
{
    Console.WriteLine($"{telefono.Marca} tiene 256 GB");
}
```

Se pueden comprobar varias propiedades a la vez, separadas por comas dentro de las llaves:

```csharp
if (dispositivo is Smartphone { CapacidadAlmacenamiento: 256, Precio: < 300 })
{
    Console.WriteLine("Smartphone de 256 GB por menos de 300");
}
```

`Precio: < 300` combina un patrón de propiedad con un patrón relacional (tema de Pattern Matching I) — se retoma con más detalle en la siguiente sección.

### Patrones de propiedad anidados

Cuando una propiedad es a su vez un objeto con sus propias propiedades, el patrón puede anidarse, sin tener que acceder nivel a nivel:

```csharp
public class Pedido
{
    public Dispositivo Producto { get; set; }
    public EstadoPedido Estado { get; set; }
}
```

```csharp
if (pedido is { Estado: EstadoPedido.Enviado, Producto: Smartphone { CapacidadAlmacenamiento: 256 } })
{
    Console.WriteLine("Pedido enviado de un smartphone de 256 GB");
}
```

Nótese que aquí el patrón exterior (`{ Estado: ..., Producto: ... }`) no lleva ningún nombre de tipo delante de la primera llave — a diferencia de `Smartphone { ... }` de antes. Esto es válido siempre que el tipo de la variable ya sea conocido y no haga falta comprobarlo (`pedido` ya es de tipo `Pedido`, no `object`); si hiciera falta comprobar también el tipo de `pedido`, se escribiría `pedido is Pedido { Estado: ... }`, igual que con cualquier otro patrón de tipo.

## Combinar patrones de tipo y de propiedad con patrones relacionales y lógicos

Los patrones relacionales y lógicos (`<`, `>`, `and`, `or`, `not`) de Pattern Matching I no eran exclusivos de valores simples — se aplican igual de bien dentro de un patrón de propiedad, como ya se vio arriba con `Precio: < 300`:

```csharp
string Categorizar(Dispositivo dispositivo) => dispositivo switch
{
    Smartphone { Precio: < 200 } => "Smartphone económico",
    Smartphone { Precio: >= 200 and < 500 } => "Smartphone de gama media",
    Smartphone => "Smartphone premium",
    Portatil { TamanioPantalla: > 15 } => "Portátil grande",
    Portatil => "Portátil compacto",
    _ => "Dispositivo desconocido"
};
```

`Smartphone` sin ninguna llave detrás (la tercera línea) sigue siendo un patrón de tipo válido por sí solo — captura cualquier `Smartphone` que no haya coincidido con los dos casos anteriores, sin necesitar una variable ni ninguna propiedad concreta. Es el mismo patrón de tipo de la primera sección, simplemente sin declarar variable porque aquí no hace falta usarla.

`and`/`or`/`not` también pueden combinar patrones de tipo completos entre sí, no solo condiciones relacionales:

```csharp
if (dispositivo is Portatil or SmartphoneGamer)
{
    Console.WriteLine("Este dispositivo tiene más potencia de la media");
}
```

## La cláusula `when`

Todo lo anterior compara contra valores fijos o rangos dentro del propio patrón. Cuando la condición depende de una comparación entre dos propiedades del mismo objeto, o de cualquier lógica que no encaje como patrón, una cláusula `when` añade una condición adicional a un caso ya emparejado por tipo, dentro de una expresión `switch`:

```csharp
string Evaluar(Smartphone telefono) => telefono switch
{
    SmartphoneGamer g when g.TasaRefresco >= 240 => "Gaming de alta gama",
    SmartphoneGamer => "Gaming estándar",
    Smartphone s when s.CapacidadAlmacenamiento < 64 => "Almacenamiento insuficiente",
    _ => "Smartphone estándar"
};
```

El patrón (`SmartphoneGamer g`) se evalúa primero; si coincide, `when` comprueba la condición adicional (`g.TasaRefresco >= 240`) usando la variable que el propio patrón acaba de declarar. Solo si ambas partes son ciertas se toma esa rama — si el tipo coincide pero `when` es falso, la búsqueda continúa con la siguiente línea, exactamente igual que si el patrón no hubiera coincidido en absoluto.

`when` no sustituye a los patrones relacionales ya vistos (`Precio: < 300` sigue siendo preferible cuando la condición es sobre una única propiedad, por ser más compacto) — su utilidad aparece cuando la condición no cabe dentro de la sintaxis de un patrón, como comparar dos propiedades entre sí o llamar a un método.

## Patrones de tipo y de propiedad en el `switch` clásico

Aunque hasta ahora todos los ejemplos usaron la expresión `switch` (Pattern Matching I), los mismos patrones de tipo y de propiedad funcionan igual dentro de un `switch` clásico (tema 02), en cada `case`:

```csharp
switch (dispositivo)
{
    case SmartphoneGamer g:
        Console.WriteLine($"Gaming a {g.TasaRefresco} Hz");
        break;
    case Smartphone { CapacidadAlmacenamiento: < 64 }:
        Console.WriteLine("Poco almacenamiento");
        break;
    default:
        Console.WriteLine("Otro dispositivo");
        break;
}
```

No es una sintaxis distinta a la ya vista — es la misma sintaxis de patrón, dentro de un `case` en vez de dentro de una rama de `switch` expression.

## Resolviendo el ejemplo de la granja

En Herencia y polimorfismo, para poder mostrar la idea de polimorfismo sin adelantar sintaxis no explicada todavía, se dejó este código con el aviso de que no hacía falta entenderlo:

```csharp
foreach (object animal in listaDeAnimales)
{
    if (animal is Ave ave) ave.Comer();
    else if (animal is Pez pez) pez.Comer();
    else if (animal is Mamifero mamifero) mamifero.Comer();
}
```

Con lo visto en este tema, ya no queda nada pendiente ahí: `is Ave ave` es exactamente un patrón de tipo, como cualquiera de los de este tema — comprueba si `animal` es un `Ave` y, si lo es, declara `ave` ya convertida, lista para usarse en la misma línea. La única razón por la que el ejemplo original encadenaba varios `if`/`else if` en vez de una expresión `switch` es que ahí el objetivo era **ejecutar una acción** (`Comer()`) en cada rama, no producir un valor — y `if`/`else if` sigue siendo la forma natural de hacerlo cuando no hace falta el resultado como expresión. Si el objetivo hubiera sido obtener, por ejemplo, un texto describiendo a cada animal, una expresión `switch` con patrones de tipo habría sido la opción más directa, tal como se ha visto en este tema.