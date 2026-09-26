# 23 - StringBuilder

## Por qué existe

En Strings ya quedó anotado, sin desarrollarlo: *"concatenar muchas veces con `+=` dentro de un bucle crea un string nuevo en cada vuelta y descarta el anterior"*. Con Pila y Heap ya visto, se puede explicar el coste real detrás de esa frase: un `string` es inmutable (Strings), así que cada concatenación no modifica el string existente — crea uno completamente nuevo en el heap, con el contenido combinado, y abandona el anterior a su suerte (hasta que el Garbage Collector lo recolecte, tema de Pila y Heap).

```csharp
string resultado = "";

for (int i = 0; i < 1000; i++)
{
    resultado += i + ", "; // cada vuelta crea un string nuevo en el heap y descarta el anterior
}
```

Con pocas concatenaciones (unas cuantas, fijas, conocidas de antemano) esto no se nota en absoluto y no hay ningún motivo para complicarse. El problema aparece cuando el número de concatenaciones depende de un bucle, y ese bucle puede tener muchas vueltas — mil strings intermedios creados y descartados, uno por vuelta, solo para llegar al último que interesa de verdad.

`StringBuilder` resuelve esto con una estructura mutable por debajo (un buffer que crece según hace falta, en vez de crear un string nuevo en cada cambio) — construir el resultado entero y convertirlo a `string` una sola vez, al final.

## Creación y uso básico

```csharp
using System.Text;

StringBuilder sb = new StringBuilder();

sb.Append("Hola");
sb.Append(", ");
sb.Append("mundo");

Console.WriteLine(sb.ToString()); // "Hola, mundo"
```

Un `StringBuilder` no es un `string` — es un objeto aparte, mutable, con su propio buffer interno. `Append` modifica ese buffer en el sitio, sin crear nada nuevo en cada llamada (a diferencia de `+=` con strings). `ToString()` es el paso explícito para obtener, al final, el `string` inmutable de siempre con todo el contenido acumulado.

`AppendLine` funciona igual que `Append`, añadiendo además un salto de línea al final:

```csharp
StringBuilder sb = new StringBuilder();
sb.AppendLine("Primera línea");
sb.AppendLine("Segunda línea");

Console.WriteLine(sb.ToString());
// Primera línea
// Segunda línea
```

## Encadenar llamadas

```csharp
StringBuilder sb = new StringBuilder();
sb.Append("Hola").Append(", ").Append("mundo").AppendLine("!");
```

Esto funciona porque `Append` (y `AppendLine`) no devuelven `void` — devuelven el propio `StringBuilder` sobre el que se acaban de llamar. Como el resultado de `sb.Append("Hola")` es el mismo `sb`, se puede llamar a `.Append(", ")` directamente sobre ese resultado, y así sucesivamente. No es una sintaxis especial del lenguaje — es la misma regla de siempre (un método puede devolver cualquier tipo, incluido el tipo desde el que se llama) aplicada de forma deliberada para permitir encadenar llamadas.

## Otros métodos

```csharp
StringBuilder sb = new StringBuilder("Hola mundo");

sb.Insert(5, " cruel"); // "Hola cruel mundo" — inserta en esa posición
sb.Remove(5, 6);         // "Hola mundo" — quita 6 caracteres a partir de la posición 5
sb.Replace("mundo", "C#"); // "Hola C#"

Console.WriteLine(sb.ToString());

sb.Clear(); // vacía el buffer por completo, sin dejar de ser el mismo StringBuilder
```

`sb.Replace(...)` modifica el propio buffer en el sitio — a diferencia de `string.Replace(...)` (BCL), que no puede modificar nada (los strings son inmutables) y por eso devuelve un string nuevo con el cambio ya aplicado. Mismo nombre de método, comportamiento distinto de fondo, coherente con qué tipo de dato es cada uno.

## `ToCharArray()` y el camino inverso

Ya se vio en Strings que cada posición de un string es un `char`, accesible con `texto[i]`. `ToCharArray()` obtiene todos esos caracteres de una vez, como un array (tema de Arrays) normal y corriente:

```csharp
string texto = "Hola";
char[] caracteres = texto.ToCharArray(); // { 'H', 'o', 'l', 'a' }

foreach (char c in caracteres)
{
    Console.WriteLine(c);
}
```

El camino inverso, de `char[]` a `string`, usa un constructor de `string` que recibe directamente un array de caracteres:

```csharp
char[] letras = { 'A', 'd', 'i', 'ó', 's' };
string palabra = new string(letras); // "Adiós"
```

## `string.Join`

El caso contrario a `Split` (ya visto en BCL, que separaba un string en un array por un carácter): `string.Join` combina los elementos de una colección en un único string, con un separador entre cada uno.

```csharp
List<string> nombres = new List<string> { "Ana", "Luis", "Marta" };

string resultado = string.Join(", ", nombres); // "Ana, Luis, Marta"
```

Funciona igual con un array que con una `List<T>` (o cualquier otra colección de las vistas en Colecciones), y con cualquier tipo de elemento, no solo `string` — cada elemento se convierte a texto con su propio `ToString()` (tema de Herencia) antes de unirlos:

```csharp
List<int> numeros = new List<int> { 1, 2, 3 };
string resultado = string.Join(" - ", numeros); // "1 - 2 - 3"
```

## Cuándo usar cada cosa

- **Concatenación o interpolación normal** (`+`, `$"..."`): un puñado de piezas fijas y conocidas de antemano — el caso de toda la vida, sigue siendo la opción más clara cuando no hay ningún bucle de por medio.
- **`StringBuilder`**: el número de concatenaciones depende de un bucle, o no se conoce de antemano cuántas van a ser — el caso donde `+=` repetido tiene un coste real que evitar.
- **`string.Join`**: lo que ya se tiene es una colección completa (array, `List<T>`...) y el objetivo es un único string con un separador entre cada elemento — más directo que recorrerla a mano con un `StringBuilder` y un `Append` por vuelta.