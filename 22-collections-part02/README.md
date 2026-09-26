# 22 - Colecciones II

## `Stack<T>`

Ya se explicó en Pila y Heap qué es una pila (LIFO: lo último en entrar es lo primero en salir) hablando de cómo el propio programa gestiona sus llamadas a métodos. `Stack<T>` es esa misma idea, pero como una colección normal que se puede usar directamente en el código:

```csharp
Stack<int> pila = new Stack<int>();

pila.Push(1); // apila un valor
pila.Push(2);
pila.Push(3);

Console.WriteLine(pila.Peek()); // 3 — mira el de arriba, sin quitarlo

Console.WriteLine(pila.Pop()); // 3 — lo quita y lo devuelve
Console.WriteLine(pila.Pop()); // 2
Console.WriteLine(pila.Count); // 1 — queda el 1
```

`Push` añade arriba del todo, `Pop` quita y devuelve el de arriba, `Peek` devuelve el de arriba sin quitarlo. Llamar a `Pop()` o `Peek()` sobre una pila vacía lanza una `InvalidOperationException` — conviene comprobar `Count > 0` antes, o usar `TryPop`/`TryPeek` (mismo patrón `TryX` ya visto varias veces).

### Recursión hecha explícita

En Métodos II se escribió `Factorial` de forma recursiva:

```csharp
static int Factorial(int n)
{
    if (n <= 1)
    {
        return 1;
    }
    return n * Factorial(n - 1);
}
```

Y en Pila y Heap se explicó que cada llamada recursiva apila un marco nuevo, con sus propias variables, hasta llegar al caso base — momento en el que esos marcos se van retirando en el orden inverso al que se apilaron. La misma idea se puede escribir de forma iterativa, con un `Stack<T>` haciendo explícito lo que antes hacía el propio mecanismo de llamadas del programa:

```csharp
static int FactorialIterativo(int n)
{
    Stack<int> pendientes = new Stack<int>();

    while (n > 1)
    {
        pendientes.Push(n);
        n--;
    }

    int resultado = 1;
    while (pendientes.Count > 0)
    {
        resultado *= pendientes.Pop();
    }

    return resultado;
}
```

```csharp
Console.WriteLine(FactorialIterativo(5)); // 120 — mismo resultado que la versión recursiva
```

El primer bucle apila `5, 4, 3, 2` (en ese orden); el segundo los va sacando en orden inverso (`2, 3, 4, 5`) multiplicándolos. El resultado es idéntico al de la versión recursiva, pero aquí la "pila de trabajo pendiente" es una estructura de datos visible y manipulable directamente, en vez de marcos de llamada gestionados por el propio runtime.

## `Queue<T>`

Mismo repertorio de operaciones que `Stack<T>>`, pero con el orden opuesto: FIFO (*first in, first out* — lo primero en entrar es lo primero en salir), como una cola de verdad.

```csharp
Queue<string> tickets = new Queue<string>();

tickets.Enqueue("Ticket 1");
tickets.Enqueue("Ticket 2");
tickets.Enqueue("Ticket 3");

Console.WriteLine(tickets.Peek()); // "Ticket 1" — mira el primero en entrar, sin quitarlo

Console.WriteLine(tickets.Dequeue()); // "Ticket 1" — lo quita y lo devuelve
Console.WriteLine(tickets.Dequeue()); // "Ticket 2"
Console.WriteLine(tickets.Count);     // 1 — queda "Ticket 3"
```

`Enqueue` añade al final, `Dequeue` quita y devuelve el primero, `Peek` mira el primero sin quitarlo — los mismos nombres de concepto que `Stack<T>` (añadir, quitar-y-devolver, mirar-sin-quitar), aplicados al extremo contrario.

Este es exactamente el caso donde `List<T>` sería la herramienta equivocada: simular esta misma cola con una lista obligaría a hacer `lista.RemoveAt(0)` cada vez que se atiende un ticket, lo cual tiene que desplazar todos los elementos restantes una posición hacia atrás — cuantos más tickets en cola, más caro se vuelve cada `Dequeue`. `Queue<T>` está pensada específicamente para que quitar el primer elemento sea una operación barata, sin ese desplazamiento.

## `IReadOnlyList<T>`

En Clases se vio que exponer un campo `private` a través de una propiedad con `private set` evita que código externo lo reasigne sin control. Con una `List<T>` como propiedad, ese mismo cuidado se queda corto:

```csharp
public class Carrito
{
    public List<string> Productos { get; private set; } = new List<string>();
}
```

```csharp
Carrito carrito = new Carrito();
carrito.Productos.Add("Teclado");    // "private set" no lo impide: no se está reasignando Productos, solo llamando a un método sobre la lista que ya tiene
carrito.Productos.Clear();           // tampoco lo impide, por el mismo motivo
```

`private set` solo protege contra **reasignar** la propiedad entera (`carrito.Productos = new List<string>();` sí fallaría). No protege el contenido de la lista, porque `Add`/`Clear`/`Remove` no reasignan nada — modifican el objeto al que la propiedad ya apunta, y eso sigue siendo `public` en la práctica, sin importar el `private set`.

`IReadOnlyList<T>` resuelve esto: es una interfaz (tema de Interfaces) que `List<T>` también implementa, con solo la parte de lectura (indexado, `Count`, recorrido con `foreach`) y ninguno de los métodos que modifican la colección.

```csharp
public class Carrito
{
    private List<string> productos = new List<string>();

    public IReadOnlyList<string> Productos => productos;

    public void Anadir(string producto)
    {
        productos.Add(producto);
    }
}
```

```csharp
Carrito carrito = new Carrito();
carrito.Anadir("Teclado");

Console.WriteLine(carrito.Productos.Count);  // funciona: leer está permitido
Console.WriteLine(carrito.Productos[0]);     // funciona: indexado también está permitido

carrito.Productos.Add("Ratón"); // error de compilación: IReadOnlyList<T> no tiene Add
```

El campo `productos` sigue siendo una `List<T>` de verdad por dentro (`Anadir` puede seguir llamando a `Add` sobre él, porque desde dentro de la clase se ve como lo que realmente es) — lo único que cambia es qué tipo se expone hacia fuera. Quien use `Carrito` puede leer y recorrer la lista de productos con total libertad, pero solo puede añadir uno a través de `Anadir(...)`, que es el único punto por el que la clase decide controlar cómo se hace.

## Otras colecciones, de pasada

Existen otras estructuras de la BCL con casos de uso más concretos, que no se desarrollan aquí por no tener, de momento, ningún ejercicio real que las necesite:

- **`LinkedList<T>`**: una lista donde cada elemento conoce al siguiente y al anterior, en vez de vivir en posiciones contiguas de memoria (a diferencia de `List<T>`, que por debajo es un array). Esto hace que insertar o quitar un elemento por el medio sea barato, sin tener que desplazar el resto — pero en código de aplicación normal casi nunca es la primera opción frente a `List<T>`, y solo compensa el cambio en escenarios muy concretos con muchísimas inserciones/borrados intermedios.
- **`SortedDictionary<TKey, TValue>` / `SortedList<TKey, TValue>`**: variantes de diccionario que mantienen sus claves ordenadas automáticamente. Existen para cuando hace falta recorrer un diccionario siempre en orden de clave, sin tener que ordenarlo aparte cada vez.