# 05 - Arrays

Un array es una colección de elementos del mismo tipo, con un tamaño fijo decidido en el momento de crearlo.

## Creación

Dos formas habituales:

```csharp
int[] numeros = { 1, 2, 3, 4, 5 };   // literal: se conoce el contenido de antemano

int[] otrosNumeros = new int[5];     // tamaño fijo, contenido con valores por defecto
```

Con `new int[5]` se reserva espacio para 5 elementos, pero no se indica su contenido — se rellenan automáticamente con el valor por defecto del tipo:

| Tipo | Valor por defecto |
|---|---|
| `int`, `double`, `long`... (numéricos) | `0` |
| `bool` | `false` |
| `char` | `'\0'` |
| Cualquier tipo por referencia (`string`, objetos...) | `null` |

## Tamaño fijo

Un array no puede crecer ni encoger una vez creado. Si necesitas añadir o quitar elementos dinámicamente, un array no es la herramienta — para eso existe `List<T>`, que se ve en el tema de colecciones. De momento, si el número de elementos es fijo y conocido, el array es la opción correcta y más ligera.

## Indexado

Igual que en un `string` (tema 03), cada posición se accede por índice, empezando en `0`.

```csharp
int[] numeros = { 10, 20, 30 };

Console.WriteLine(numeros[0]); // 10
Console.WriteLine(numeros[2]); // 30

numeros[1] = 99; // se puede modificar un elemento por índice
```

Acceder a un índice que no existe (por ejemplo `numeros[10]` en un array de 3 elementos) compila sin problema, pero falla en tiempo de ejecución. El porqué exacto y cómo manejarlo se ve en el tema de excepciones — de momento basta con saber que hay que tener cuidado con los límites del array.

## Length

```csharp
int[] numeros = { 10, 20, 30 };
Console.WriteLine(numeros.Length); // 3
```

`Length` da el número total de elementos. El último índice válido siempre es `Length - 1`, nunca `Length`.

## Recorrido

Con `for`, cuando se necesita el índice:

```csharp
for (int i = 0; i < numeros.Length; i++)
{
    Console.WriteLine(numeros[i]);
}
```

Con `foreach`, cuando solo hace falta el valor de cada elemento, sin su posición:

```csharp
foreach (int numero in numeros)
{
    Console.WriteLine(numero);
}
```

## Arrays multidimensionales

C# permite arrays de más de una dimensión, útiles para representar tablas o matrices:

```csharp
int[,] tablero = new int[3, 3]; // 3 filas x 3 columnas, todo a 0

tablero[0, 0] = 1;
tablero[1, 1] = 5;

Console.WriteLine(tablero[1, 1]); // 5
```

Esto se deja aquí solo como referencia rápida de que existen. Recorrerlos bien (bucles anidados, filas y columnas) y usarlos en ejercicios reales se retoma más adelante, cuando el resto de herramientas del lenguaje esté más asentado — intentarlo ahora mezclaría demasiadas cosas nuevas a la vez.