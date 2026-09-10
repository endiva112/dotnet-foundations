# 08 - BCL (Base Class Library)

## Qué es la BCL

La BCL (Base Class Library) es el conjunto de tipos que vienen incluidos con .NET, listos para usar sin instalar nada aparte: `Math`, `Random`, `string`, `Array`, `DateTime`, y muchísimos más. En los proyectos modernos de .NET no hace falta escribir `using System;` para acceder a la mayoría de ellos — el propio proyecto lo trae ya resuelto por defecto (esto se conoce como *implicit usings*; no hace falta entender el mecanismo todavía, solo saber que por eso estos tipos ya están disponibles sin ningún paso previo).

Este tema no pretende cubrir la BCL entera — es enorme, cubre desde matemáticas hasta redes hasta manejo de archivos. La idea es reunir las herramientas más comunes, las que se van a usar una y otra vez, y cerrar con cómo encontrar el resto cuando haga falta.

## Math

Todos los métodos de `Math` son **static** (tema 06) — se llaman directamente sobre el tipo, sin crear ningún objeto:

```csharp
Console.WriteLine(Math.Abs(-5));      // 5 — valor absoluto
Console.WriteLine(Math.Max(3, 7));    // 7 — el mayor de los dos
Console.WriteLine(Math.Min(3, 7));    // 3 — el menor de los dos
Console.WriteLine(Math.Pow(2, 3));    // 8 — 2 elevado a 3
Console.WriteLine(Math.Sqrt(16));     // 4 — raíz cuadrada
Console.WriteLine(Math.Round(4.567, 2)); // 4.57 — redondeo a 2 decimales
Console.WriteLine(Math.PI);           // 3.14159265358979
```

## Random

A diferencia de `Math`, `Random` no es static — hace falta crear una instancia antes de usarla, igual que ya hicisteis con `new int[5]` en el tema de arrays:

```csharp
Random aleatorio = new Random();

int numero = aleatorio.Next();          // cualquier entero
int dado = aleatorio.Next(1, 7);        // entre 1 (incluido) y 7 (excluido) → 1 a 6
double probabilidad = aleatorio.NextDouble(); // entre 0.0 y 1.0
```

De momento basta con tratar `new Random()` como "así se crea, así se usa", sin entrar en qué significa exactamente crear una instancia — eso se retoma en el tema de clases.

## Métodos de string

```csharp
string texto = "  Hola Mundo  ";

Console.WriteLine(texto.Trim());                  // "Hola Mundo" — quita espacios al inicio y final
Console.WriteLine(texto.ToUpper());                // "  HOLA MUNDO  "
Console.WriteLine(texto.ToLower());                // "  hola mundo  "
Console.WriteLine(texto.Contains("Mundo"));         // true
Console.WriteLine(texto.Trim().StartsWith("Hola")); // true
Console.WriteLine(texto.Trim().EndsWith("Mundo"));  // true
Console.WriteLine(texto.Replace("Mundo", "C#"));    // "  Hola C#  "
Console.WriteLine(texto.IndexOf("Mundo"));          // posición donde empieza "Mundo", o -1 si no está

string csv = "Ana,Luis,Marta";
string[] nombres = csv.Split(',');   // { "Ana", "Luis", "Marta" }

string fragmento = "programacion".Substring(0, 4); // "prog" — desde el índice 0, 4 caracteres
```

Conectando con el tema anterior (nullable reference types): comprobar si un `string?` está vacío o sin contenido es tan común que existen métodos dedicados, en vez de tener que escribir la comprobación a mano:

```csharp
string? entrada = "";

if (string.IsNullOrEmpty(entrada))       // true si es null O ""
{
    Console.WriteLine("No se introdujo nada");
}

if (string.IsNullOrWhiteSpace(entrada))  // true si es null, "", o solo espacios
{
    Console.WriteLine("No hay contenido real");
}
```

## Array (métodos estáticos)

Además de `Length` (tema 05), `Array` trae varios métodos estáticos útiles:

```csharp
int[] numeros = { 5, 2, 8, 1, 9 };

Array.Sort(numeros);     // ordena el array en el propio sitio: { 1, 2, 5, 8, 9 }
Array.Reverse(numeros);  // invierte el orden en el propio sitio: { 9, 8, 5, 2, 1 }

int posicion = Array.IndexOf(numeros, 5); // posición donde está el 5, o -1 si no está

Array.Clear(numeros); // pone todo el array a los valores por defecto del tipo (0 para int)
```

`Sort` y `Reverse` modifican el array original, no devuelven uno nuevo — es el mismo array, reordenado en el mismo espacio de memoria.

### Copiar un array de verdad

Recordando el tema 05: un array es un tipo por referencia. Esto significa que `copia = original;` **no copia los valores**, copia la referencia — después de esa línea, `copia` y `original` son dos nombres para el mismo array en el heap. Modificar uno modifica el otro, porque no hay dos arrays, hay uno solo:

```csharp
int[] original = { 1, 2, 3 };
int[] copia = original;

copia[0] = 99;
Console.WriteLine(original[0]); // 99 — es el mismo array, no una copia
```

Esto no siempre es un error — hay casos donde compartir la misma referencia es exactamente lo que se quiere (por ejemplo, que dos partes de un programa trabajen siempre sobre los mismos datos actualizados). El problema es cuando se hace sin querer, esperando tener dos arrays independientes y sin darse cuenta de que en realidad es uno solo.

Cuando de verdad se necesitan dos arrays independientes, con los mismos valores pero en espacios de memoria distintos, existe `Array.Copy`:

```csharp
int[] original = { 1, 2, 3 };
int[] copia = new int[3];
Array.Copy(original, copia, original.Length);

copia[0] = 99;
Console.WriteLine(original[0]); // 1 — independiente de copia
```

También existe `original.Clone()`, que hace algo parecido, pero tiene más matiz del que aparenta (es una copia "superficial", relevante sobre todo cuando el array contiene objetos en vez de tipos por valor) — se retoma más adelante, cuando haya objetos de por medio y el matiz tenga sentido real. De momento, `Array.Copy` cubre lo que hace falta.

## Entrada por consola

Hasta ahora todos los ejercicios partían de datos fijos en el código. `Console.ReadLine()` permite leer lo que el usuario escribe:

```csharp
Console.Write("Introduce tu nombre: ");
string? nombre = Console.ReadLine();

Console.WriteLine($"Hola, {nombre}");
```

Importante: `Console.ReadLine()` devuelve `string?`, no `string` — puede dar `null` en situaciones concretas (por ejemplo, si la entrada estándar se cierra inesperadamente). Es un ejemplo real, no inventado, de por qué NRT (tema 07) importa en la práctica: cualquier dato que venga de fuera del programa (usuario, archivo, red...) es un candidato natural a ser tratado como nullable.

## Parsing: convertir texto a número

Lo que devuelve `Console.ReadLine()` es siempre texto, aunque el usuario escriba números. Para trabajar con ese valor como número, hace falta convertirlo:

```csharp
string? entrada = Console.ReadLine();
int numero = int.Parse(entrada!); // ! porque asumimos que sí hay contenido; si está vacío o no es un número, lanza excepción

double decimal_ = double.Parse("19.99");
```

`int.Parse` / `double.Parse` son la forma estándar para esto. Existe también una alternativa más segura, `TryParse`, que evita la excepción — se deja para Métodos II, porque su forma de uso necesita `out`, que todavía no habéis visto.

También existe `Convert` (`Convert.ToInt32(...)`, etc.), que aparece con frecuencia en código de otras personas. Se comporta distinto en casos raros (por ejemplo, `Convert.ToInt32(null)` da `0` en vez de lanzar excepción, mientras que `Parse` con `null` sí falla) — no hace falta usarlo activamente, pero conviene reconocerlo si aparece en código ajeno.

## DateTime

Para trabajar con fechas y horas:

```csharp
DateTime ahora = DateTime.Now;
Console.WriteLine(ahora); // fecha y hora actuales

DateTime cumpleanos = new DateTime(2000, 5, 15); // año, mes, día

TimeSpan diferencia = ahora - cumpleanos; // resta entre dos fechas
Console.WriteLine($"Han pasado {diferencia.Days} días");
```

Esto es solo lo mínimo para poder usar una fecha en un ejercicio cuando haga falta — formateo, zonas horarias y demás se dejan fuera a propósito, por ser un tema con mucha más profundidad de la que hace falta ahora mismo.

## Cómo encontrar el resto

La BCL tiene muchísimos más tipos y métodos de los que caben en un tema — memorizarla no es el objetivo, y tampoco es lo que hace un desarrollador con experiencia en el día a día. Lo útil es saber que algo probablemente ya existe hecho, y cómo encontrarlo:

- **IntelliSense**: en VS Code con C# Dev Kit, escribir `.` después de una variable muestra automáticamente los métodos disponibles para ese tipo, con una descripción al pasar el ratón por encima.
- **Documentación oficial**: [Microsoft Learn](https://learn.microsoft.com/dotnet/api/) tiene la referencia completa de la BCL, con ejemplos de uso para cada método.

Cuando en un ejercicio futuro haga falta algo que no está en este tema, la expectativa no es saberlo de memoria, sino saber buscarlo.