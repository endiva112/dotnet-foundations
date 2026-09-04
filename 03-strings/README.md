# 03 - Strings

Ya se ha usado `string` desde el tema 1, pero tiene particularidades propias que conviene ver antes de meterse en bucles, porque los ejercicios de bucles los dan por sabidos.

## Inmutabilidad

Un `string` en C# es inmutable: ninguna operación lo modifica, siempre se crea uno nuevo.

```csharp
string saludo = "Hola";
saludo.ToUpper(); // esto NO modifica saludo
Console.WriteLine(saludo); // sigue imprimiendo "Hola"

saludo = saludo.ToUpper(); // hay que reasignar para quedarse con el resultado
Console.WriteLine(saludo); // ahora sí, "HOLA"
```

Esto importa por rendimiento: concatenar muchas veces con `+=` dentro de un bucle crea un string nuevo en cada vuelta y descarta el anterior. Para pocas concatenaciones no se nota, pero es un hábito a vigilar. La herramienta pensada para concatenar mucho (`StringBuilder`) se ve más adelante, en el tema de librerías y métodos nativos.

## Concatenación vs Interpolación

```csharp
string nombre = "Ana";

string saludo1 = "Hola, " + nombre + ".";      // concatenación con +
string saludo2 = $"Hola, {nombre}.";           // interpolación
```

La interpolación (`$"..."`) es preferible casi siempre: se lee mejor y evita errores de espacios o paréntesis al mezclar texto con variables.

## Secuencias de escape

| Secuencia | Significado |
|---|---|
| `\n` | Salto de línea |
| `\t` | Tabulación |
| `\"` | Comilla doble literal dentro del string |
| `\\` | Barra invertida literal |

```csharp
Console.WriteLine("Línea 1\nLínea 2");
Console.WriteLine("Dijo: \"hola\"");
```

## Strings verbatim

Con `@` delante, el string se toma literal: los `\` no se interpretan como escape. Útil sobre todo para rutas de Windows.

```csharp
string ruta = @"C:\Users\Ana\Documentos";   // sin @, habría que escribir "C:\\Users\\Ana\\Documentos"
```

## Comparación de igualdad

```csharp
string a = "hola";
string b = "hola";

Console.WriteLine(a == b); // true — compara contenido, no referencia
```

Esto es distinto a lo que pasará más adelante con objetos de clases propias, donde `==` por defecto sí compara referencia. Se retoma en el tema de clases.

## Comprobar strings vacíos o nulos

```csharp
string entrada = "";

if (string.IsNullOrEmpty(entrada))
{
    Console.WriteLine("La entrada está vacía o es null.");
}

if (string.IsNullOrWhiteSpace(entrada))
{
    Console.WriteLine("La entrada está vacía, es null, o son solo espacios.");
}
```

`IsNullOrWhiteSpace` es la opción más segura para validar algo que viene de fuera (entrada de usuario, por ejemplo), porque `"   "` no es un string vacío, pero tampoco tiene contenido útil.

## Length e indexado

```csharp
string palabra = "Hola";

Console.WriteLine(palabra.Length);   // 4
Console.WriteLine(palabra[0]);       // 'H' — el indexado empieza en 0
Console.WriteLine(palabra[palabra.Length - 1]); // 'l' — último carácter
```

Cada posición de un string es un `char`. Esto es lo que hace falta para poder recorrer un string carácter a carácter en un bucle: `Length` da el número de vueltas, y el índice accede a cada carácter.