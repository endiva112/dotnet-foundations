# 21 - Colecciones

## Por qué hace falta algo más que un array

Un array (tema de Arrays) tiene tamaño fijo, decidido en el momento de crearlo. Cualquier ejercicio que necesitara "añadir un elemento más" hasta ahora obligaba a crear un array nuevo, más grande, y copiar todo el contenido anterior — no había otra forma.

`List<T>` es, por debajo, exactamente eso ya resuelto: una clase genérica (tema de Generics) que envuelve un array y se encarga de crecerlo automáticamente cuando hace falta, con toda la seguridad de tipos que ya se vio en Generics — sin boxing, sin necesitar ningún cast al leer un elemento.

## `List<T>`

```csharp
List<int> numeros = new List<int> { 1, 2, 3 }; // inicializador de colección, igual de directo que con un array

numeros.Add(4);        // { 1, 2, 3, 4 }
numeros.Add(5);        // { 1, 2, 3, 4, 5 }
numeros.Remove(3);     // quita el primer 3 que encuentra: { 1, 2, 4, 5 }
numeros.RemoveAt(0);   // quita por posición: { 2, 4, 5 }
numeros.Insert(1, 99); // inserta en esa posición: { 2, 99, 4, 5 }

Console.WriteLine(numeros.Contains(99)); // true
Console.WriteLine(numeros.IndexOf(4));   // posición donde está el 4, o -1 si no está
Console.WriteLine(numeros.Count);        // 4 — no Length

numeros.Clear(); // vacía la lista por completo, sin dejar de ser la misma lista
```

`Count`, no `Length` — es una diferencia deliberada de nombre entre un array y una `List<T>`, no un descuido: `Length` (arrays) siempre existió pensado para un tamaño fijo, y `Count` (la mayoría de colecciones de la BCL) para un tamaño que puede cambiar. Son conceptualmente el mismo dato (cuántos elementos hay ahora mismo), con nombre distinto según el tipo de colección.

Indexado y recorrido funcionan exactamente igual que con un array:

```csharp
Console.WriteLine(numeros[0]); // acceso por índice, igual que un array

foreach (int numero in numeros)
{
    Console.WriteLine(numero);
}
```

## `List<T>.Sort()`

En Generics se vio la restricción `where T : IComparable<T>`, necesaria para poder comparar dos valores de tipo `T` genérico. `List<T>.Sort()`, sin argumentos, es exactamente esa restricción aplicada en la práctica:

```csharp
List<int> numeros = new List<int> { 5, 2, 8, 1 };
numeros.Sort(); // { 1, 2, 5, 8 } — int implementa IComparable<int>
```

```csharp
List<Persona> personas = new List<Persona> { /* ... */ };
personas.Sort(); // error en tiempo de ejecución: Persona no implementa IComparable<Persona>
```

El segundo caso compila (porque `Sort()` sin argumentos existe en cualquier `List<T>`, sea cual sea `T`), pero falla al ejecutarse con una `InvalidOperationException`, indicando que no se pudo comparar dos elementos porque no existe ninguna implementación de `IComparable` en `Persona`. Esto se soluciona implementando `IComparable<Persona>` en la propia clase (tema de Interfaces) — decidiendo, por ejemplo, que una `Persona` se compara con otra por edad.

## `Dictionary<TKey, TValue>`

Guarda pares clave-valor, con acceso directo por clave en vez de por posición:

```csharp
Dictionary<string, int> edades = new Dictionary<string, int>();

edades["Ana"] = 30;   // añade o sobrescribe, según si "Ana" ya existía
edades.Add("Luis", 25); // añade; si "Luis" ya existiera, lanza ArgumentException

Console.WriteLine(edades["Ana"]); // 30
```

La diferencia entre `[clave] = valor` y `Add(clave, valor)` es justo esa: el indexador sobrescribe silenciosamente si la clave ya existe, `Add` lanza una excepción si la clave ya está presente. Cuál usar depende de si esa situación (clave repetida) es un caso normal a ignorar o un error a detectar.

Comprobar si una clave existe, sin arriesgarse a una excepción por acceder a una que no está:

```csharp
if (edades.ContainsKey("Ana"))
{
    Console.WriteLine(edades["Ana"]);
}
```

Esto funciona, pero comprueba y accede por separado — dos búsquedas en vez de una. El mismo patrón `TryX` ya visto en `int.TryParse`/`Enum.TryParse` (Métodos II, Enums) existe también aquí, en una sola búsqueda:

```csharp
if (edades.TryGetValue("Ana", out int edad))
{
    Console.WriteLine(edad);
}
```

Recorrer un diccionario entero da, en cada vuelta, un `KeyValuePair<TKey, TValue>` — un par con `.Key` y `.Value`:

```csharp
foreach (KeyValuePair<string, int> par in edades)
{
    Console.WriteLine($"{par.Key}: {par.Value}");
}
```

Si solo hace falta uno de los dos lados, `.Keys` y `.Values` recorren solo eso:

```csharp
foreach (string nombre in edades.Keys)
{
    Console.WriteLine(nombre);
}
```

## `HashSet<T>`

Garantiza algo que ni un array ni una `List<T>` garantizan por sí solos: que no haya elementos repetidos.

```csharp
HashSet<string> nombres = new HashSet<string>();

bool seAnadio1 = nombres.Add("Ana");  // true — se añadió de verdad
bool seAnadio2 = nombres.Add("Ana");  // false — "Ana" ya estaba, no se añade otra vez

Console.WriteLine(nombres.Count); // 1
```

`HashSet<T>.Add` devuelve `bool` (si realmente se añadió algo nuevo o no), a diferencia de `List<T>.Add`, que no devuelve nada — tiene sentido, porque en una `List<T>` añadir siempre tiene éxito (los duplicados están permitidos), mientras que en un `HashSet<T>` puede que la operación no haga nada si el valor ya estaba.

## Por qué el mismo `foreach` funciona sobre los tres

Desde el principio del curso, `foreach` ha funcionado sobre arrays sin que se explicara el porqué a fondo — el compilador trata los arrays como un caso especial desde el principio. Que el mismo `foreach`, con la misma sintaxis, funcione también sobre `List<T>`, `Dictionary<TKey, TValue>` y `HashSet<T>` no es coincidencia: los tres implementan una interfaz de la BCL llamada `IEnumerable<T>` (tema de Interfaces) — es exactamente lo que `foreach` necesita por debajo para poder recorrer cualquier tipo, no solo arrays. Cualquier tipo propio que implemente `IEnumerable<T>` también podría recorrerse con `foreach`, aunque escribir esa implementación a mano queda fuera del alcance de este tema.

## `List<T>` con polimorfismo

En los ejercicios de Herencia se resolvió el inventario de dispositivos con un array (`Dispositivo[]`), porque en aquel momento `List<T>` todavía no existía en el temario. Con `List<T>` ya disponible, esa misma situación se escribiría de forma más natural así:

```csharp
List<Dispositivo> dispositivos = new List<Dispositivo>
{
    new Portatil("Asus", 500m, 15),
    new Smartphone("Poco", 200m, 128)
};

dispositivos.Add(new SmartphoneGamer("Huawei", 600m, 256, 165)); // esto sí era imposible con el array de tamaño fijo

foreach (Dispositivo dispositivo in dispositivos)
{
    dispositivo.Encender(); // polimorfismo, exactamente igual que con el array
}
```

El polimorfismo en sí no cambia nada (sigue siendo el mismo mecanismo de Herencia), pero `List<T>` añade justo lo que un array no podía dar: seguir añadiendo dispositivos nuevos al inventario después de haberlo creado, sin tener que fijar de antemano cuántos iba a haber.

## Convertir entre array y `List<T>`

```csharp
List<int> lista = new List<int> { 1, 2, 3 };
int[] array = lista.ToArray();

int[] arrayOriginal = { 4, 5, 6 };
List<int> listaDesdeArray = new List<int>(arrayOriginal);
```

`ToArray()` copia el contenido actual de la lista a un array nuevo de tamaño fijo. El constructor de `List<T>` que recibe un array (o cualquier otra colección) hace el camino inverso, copiando su contenido a una lista nueva. En ambos casos se trata de una copia — modificar una de las dos estructuras después de la conversión no afecta a la otra.