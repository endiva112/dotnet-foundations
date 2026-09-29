# 25 - Tipos y parsing

## Repaso ampliado de tipos numéricos

En el tema 1 solo se vio la familia de tipos numéricos más habitual (`int`, `long`, `double`, `float`, `decimal`). Existen otros, menos frecuentes pero reales, con rangos más ajustados o sin signo:

| Tipo | Rango | Uso habitual |
|---|---|---|
| `sbyte` | -128 a 127 | Muy poco común; aparece sobre todo en interoperabilidad de bajo nivel |
| `byte` | 0 a 255 | Datos binarios, colores (canal individual RGB), lectura de archivos byte a byte |
| `short` | -32.768 a 32.767 | Poco común hoy en día; ahorro de memoria en estructuras muy grandes con muchos valores pequeños |
| `ushort` | 0 a 65.535 | Aún menos común que `short`; mismo caso de uso, sin necesitar negativos |
| `int` | ±2.147 millones aprox. | El entero por defecto — se usa salvo motivo concreto para otra cosa |
| `uint` | 0 a 4.294 millones aprox. | Poco común en código de aplicación; aparece en interoperabilidad con APIs que lo exigen |
| `long` | ±9,2 trillones aprox. | Cuando `int` se queda corto (contadores muy grandes, timestamps en ticks) |
| `ulong` | 0 a 18,4 trillones aprox. | Igual que `uint`, pero para long — poco común fuera de casos muy concretos |

En la práctica, `int` cubre la inmensa mayoría de casos, y `long` el resto — el resto de la tabla se reconoce más que se usa activamente, salvo que el propio dominio del problema imponga un rango exacto (por ejemplo, `byte` para un canal de color, que por definición nunca pasa de 255).

## Conversión implícita y explícita

Ya se ha usado, sin nombrarlo formalmente, en varios temas: asignar un `int` a una variable `double` no pide ningún permiso especial, pero al revés sí.

```csharp
int entero = 5;
double decimal_ = entero; // conversión implícita — no hace falta ningún cast

double otroDecimal = 5.9;
int otroEntero = (int)otroDecimal; // conversión explícita — hace falta el cast, y se pierde el .9
```

La diferencia de fondo es si la conversión puede perder información:

- **Ampliación** (*widening*): el tipo de destino puede representar cualquier valor que el tipo de origen pueda tener, sin ningún riesgo de pérdida — `int → double`, `int → long`, `short → int`. El compilador lo permite de forma **implícita**, sin pedir un cast, porque no hay nada que pueda salir mal.
- **Restricción** (*narrowing*): el tipo de destino no puede garantizar que quepa todo lo que el de origen pueda tener — `double → int` (se pierden los decimales), `long → int` (un valor de `long` puede ser demasiado grande para `int`). El compilador exige un cast **explícito**, precisamente para que quien escribe el código confirme, a propósito, que asume ese riesgo.

```csharp
long numeroGrande = 5_000_000_000; // más grande de lo que un int puede representar
int truncado = (int)numeroGrande;   // compila, pero el resultado es un valor incorrecto (overflow), no una excepción
```

Con tipos numéricos primitivos, una conversión explícita que no cabe no lanza ninguna excepción por sí sola — el valor simplemente se trunca o da la vuelta (*overflow*), de forma silenciosa. Esto es distinto de un `InvalidCastException` (visto en Herencia, para conversiones entre tipos de referencia) — ahí sí hay una excepción, porque ahí lo que falla es que el objeto no es, en absoluto, del tipo pedido; aquí el valor sí es un número, solo que no cabe en el tipo de destino.

## Coerción frente a conversión

Estos dos términos aparecen a veces como si fueran cosas distintas, y conviene aclarar la relación real entre ellos antes de que genere confusión: en C#, **coerción** no es un mecanismo aparte de la conversión — es, sencillamente, otro nombre para la conversión **implícita** en concreto (la que el compilador hace por cuenta propia, sin que se le pida explícitamente). "Conversión", como término general, cubre tanto la implícita como la explícita (el cast). No hay que memorizar una distinción formal más allá de esta: si se ve "coerción" en algún sitio, se está hablando de una conversión implícita, no de un mecanismo distinto.

## Los cuatro modos de convertir texto a número

Ya han ido apareciendo por separado, en distintos temas — aquí se ponen uno al lado del otro, porque no resuelven exactamente el mismo problema.

```csharp
string texto = "42";

int a = int.Parse(texto);                      // BCL: lanza FormatException si el texto no es un número válido
int b = Convert.ToInt32(texto);                  // BCL: similar a Parse, con diferencias en casos raros (null, otros tipos)
bool exito = int.TryParse(texto, out int c);     // Métodos II: no lanza excepción, devuelve false si falla
```

Y, aparte de estos tres (que convierten un `string` en número), está el cast, que convierte entre tipos **ya numéricos**, no desde texto:

```csharp
double numero = 42.9;
int d = (int)numero; // esto no es parsing: numero ya era un número, no texto
```

Resumen de cuándo usar cada uno:

| Forma | Entrada | Si falla | Cuándo usarla |
|---|---|---|---|
| `(Tipo)variable` | Otro tipo numérico | Trunca/overflow en silencio (sin excepción) | Convertir entre tipos numéricos ya conocidos, nunca desde texto |
| `Tipo.Parse(texto)` | `string` | Lanza excepción | El texto viene de una fuente en la que confías, y un formato inválido es un error real que debe pararlo todo |
| `Tipo.TryParse(texto, out valor)` | `string` | Devuelve `false`, no lanza nada | El texto viene de fuera del programa (usuario, archivo, red) y un formato inválido es una situación normal a manejar, no una excepción |
| `Convert.ToTipo(valor)` | `string` u otros tipos, incluido `null` | Lanza excepción (salvo con `null`, que da `0`/equivalente) | Casos donde puede llegar `null` y se prefiere un valor por defecto a una excepción; también común en código ya existente |

En código nuevo, la elección casi siempre está entre `Parse` (cuando un formato inválido debe ser un error visible) y `TryParse` (cuando no lo es) — `Convert` se reconoce más por aparecer en código ajeno que por elegirse activamente para código propio.

## `CultureInfo` en el parsing

Ya se vio en Formato que `,` o `.` como separador decimal depende de la configuración regional de la máquina (`CultureInfo`), y que esto puede convertir un `Parse` que funciona en un ordenador en un `FormatException` en otro. Aplicado a este tema: cualquiera de las formas de la tabla anterior que acepte un `string` (`Parse`, `TryParse`, `Convert`) puede recibir también un `CultureInfo` como argumento adicional, para no depender de la configuración de la máquina donde se ejecute:

```csharp
using System.Globalization;

double numero = double.Parse("42.9", CultureInfo.InvariantCulture); // siempre con punto, sin importar la máquina
```

La misma recomendación de Formato aplica aquí: cuando el texto que se parsea no es algo que una persona escribió a mano pensando en su propio idioma (un dato leído de un archivo, una respuesta de una API, un valor guardado por el propio programa), `CultureInfo.InvariantCulture` es la opción por defecto más segura — evita que el comportamiento del programa cambie según en qué máquina se ejecute.