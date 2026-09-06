# 05 - Métodos

## Función vs método

En algunos lenguajes (C, Python, JavaScript...) se distingue entre **función** (un bloque de código reutilizable que puede existir de forma independiente) y **método** (un bloque de código similar, pero perteneciente a una clase u objeto).

En C#, los métodos se definen dentro de un tipo, como una clase o un `struct`. Todavía no hemos visto esos conceptos, así que por ahora podemos pensar simplemente en un **método como un bloque de código reutilizable que tiene un nombre y puede recibir datos y devolver un resultado**.

Por ejemplo:

```csharp
static int Sumar(int a, int b)
{
    return a + b;
}
```

En los programas pequeños de C# moderno también podemos escribir código directamente:

```csharp
Console.WriteLine("Hola mundo");
```

Sin declarar explícitamente una clase ni un método `Main`. Esto se debe a los **top-level statements**, una característica de C# que simplifica la estructura de programas pequeños. El compilador genera automáticamente la estructura necesaria para ejecutar ese código.

La relación entre métodos, clases y otros tipos de C# se verá con más detalle cuando lleguemos al tema de **clases**.

## Estructura básica

```csharp
static int Sumar(int a, int b)
{
    return a + b;
}

int resultado = Sumar(3, 4); // 7
```

* `static` indica que el método pertenece al tipo y no a una instancia. De momento todos nuestros métodos serán `static` porque todavía no hemos visto clases ni instancias. Esto se entenderá mejor en el tema de **clases**.
* El tipo antes del nombre (`int` en este caso) es el tipo de dato que devuelve el método. Si no devuelve ningún valor, se utiliza `void`.
* `Sumar` es el nombre del método.
* Lo que aparece entre paréntesis son sus parámetros.
* `return` termina la ejecución del método y devuelve un valor al código que lo llamó.

Para utilizar un método, se escribe su nombre y se le proporcionan los argumentos correspondientes:

```csharp
int resultado = Sumar(3, 4);
```

En este caso, `3` y `4` son los **argumentos**, mientras que `a` y `b` son los **parámetros** definidos por el método.

## Parámetros

```csharp
static void Saludar(string nombre, int edad)
{
    Console.WriteLine($"Hola {nombre}, tienes {edad} años.");
}

Saludar("Ana", 30);
```

Los **parámetros** son los datos que un método necesita recibir para realizar su trabajo.

Los valores concretos que proporcionamos al llamar al método se llaman **argumentos**:

```csharp
Saludar("Ana", 30);
```

Por defecto, los parámetros se pasan **por valor**: el método recibe una copia del valor.

Si el parámetro es un tipo por referencia (como una lista), la copia corresponde a la referencia, por lo que modificar el contenido del objeto sí se nota fuera del método. Sin embargo, reasignar el parámetro a otro objeto distinto no cambia la variable original.

También se pueden pasar parámetros por nombre, en cualquier orden:

```csharp
Saludar(edad: 30, nombre: "Ana");
```

Esto puede hacer más clara la llamada cuando un método tiene varios parámetros.

## Sobrecarga (overloading)

La **sobrecarga** consiste en tener varios métodos con el mismo nombre, pero con una lista de parámetros diferente.

La diferencia puede estar en:

* El número de parámetros.
* El tipo de los parámetros.
* La combinación de ambos.

El compilador decide qué método utilizar según los argumentos que proporcionemos.

```csharp
static int Sumar(int a, int b) => a + b;

static double Sumar(double a, double b) => a + b;

static int Sumar(int a, int b, int c) => a + b + c;
```

Esto funciona igual que en Java: los tres métodos están **sobrecargados** porque tienen el mismo nombre (`Sumar`), pero una lista de parámetros diferente.

Por ejemplo:

```csharp
Sumar(2, 3);        // utiliza Sumar(int, int)

Sumar(2.5, 3.5);    // utiliza Sumar(double, double)

Sumar(1, 2, 3);     // utiliza Sumar(int, int, int)
```

## Expresión de cuerpo (expression-bodied member)

El `=>` de estos ejemplos **no es una expresión lambda** — es una forma abreviada de escribir un método cuyo cuerpo es una única expresión (*expression-bodied member*).

```csharp
static int Sumar(int a, int b) => a + b;

// equivale a:
static int Sumar(int a, int b)
{
    return a + b;
}
```

El mismo símbolo también aparece en las **expresiones lambda**, con un significado distinto: allí representa una función anónima.

```csharp
Func<int, int> Duplicar = x => x * 2;
```

El contexto determina qué significa `=>`. Las expresiones lambda se ven más adelante en su propio tema.

## `ref` y `out`

Permiten que un método reciba una variable del código que lo llama **por referencia**, en lugar de trabajar únicamente con una copia de su valor.

Esto permite que el método pueda modificar directamente la variable original o, en el caso de `out`, proporcionar un valor a través de ella.

No existen en Java de la misma forma. En Java, cuando necesitamos devolver varios resultados, normalmente recurrimos a crear un objeto que los contenga, utilizar una colección, etc.

### `ref` — el valor ya debe existir antes de llamar

Con `ref`, la variable tiene que estar inicializada antes de llamar al método.

```csharp
static void Duplicar(ref int numero)
{
    numero *= 2;
}

int valor = 5;

Duplicar(ref valor);

Console.WriteLine(valor); // 10
```

El método puede modificar directamente la variable `valor`.

### `out` — el método proporciona el valor

Con `out`, la variable no necesita estar inicializada antes de llamar al método. El método está obligado a asignarle un valor antes de terminar.

Es útil cuando un método necesita comunicar dos cosas a la vez: si una operación fue posible y, en caso de éxito, cuál fue el resultado.

Un patrón habitual es devolver un `bool` indicando si la operación tuvo éxito y utilizar `out` para proporcionar el resultado:

```csharp
static bool IntentarDividir(int a, int b, out int resultado)
{
    if (b == 0)
    {
        resultado = 0;
        return false;
    }

    resultado = a / b;
    return true;
}

if (IntentarDividir(10, 2, out int division))
{
    Console.WriteLine($"Resultado: {division}");
}
else
{
    Console.WriteLine("No se puede dividir entre 0.");
}
```

Aquí:

* `return true` indica que la operación se pudo realizar.
* `return false` indica que no se pudo realizar.
* `out int division` permite obtener el resultado de la operación.

La diferencia clave es:

* `ref` → la variable **debe tener un valor antes** de llamar al método y el método puede modificarla.
* `out` → la variable **no necesita tener un valor antes** y el método está obligado a asignarle uno.

## Valores por defecto

Un parámetro puede tener un **valor por defecto**, que se utiliza si no se proporciona ese argumento al llamar al método.

Esto puede evitar algunas sobrecargas que en Java tendríamos que escribir manualmente.

```csharp
static void MostrarMensaje(string texto, int repeticiones = 1)
{
    for (int i = 0; i < repeticiones; i++)
    {
        Console.WriteLine(texto);
    }
}

MostrarMensaje("Hola");           // usa repeticiones = 1

MostrarMensaje("Hola", 3);        // usa repeticiones = 3
```

En la primera llamada no proporcionamos `repeticiones`, así que se utiliza su valor por defecto: `1`.

Los parámetros con valor por defecto deben aparecer después de los parámetros que no tienen valor por defecto:

```csharp
// Correcto
static void Ejemplo(string texto, int repeticiones = 1)
{
}
```

```csharp
// Incorrecto
static void Ejemplo(int repeticiones = 1, string texto)
{
}
```

## Cuándo usar cada mecanismo

| Necesidad                                                                               | Mecanismo                         |
| --------------------------------------------------------------------------------------- | --------------------------------- |
| Un método hace variantes de lo mismo con distinto tipo o número de datos de entrada     | Sobrecarga                        |
| Un parámetro casi siempre tiene el mismo valor, pero a veces hace falta cambiarlo       | Valor por defecto                 |
| El método necesita modificar una variable que ya existe fuera                           | `ref`                             |
| El método necesita proporcionar un resultado adicional mediante un parámetro            | `out`                             |
| El método tiene una única expresión como cuerpo y queremos escribirlo de forma compacta | `=>` / *expression-bodied method* |
