# 06 - Métodos I

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

sin declarar explícitamente una clase ni un método `Main`. Esto se debe a los **top-level statements**, una característica de C# que simplifica la estructura de programas pequeños. El compilador genera automáticamente la estructura necesaria para ejecutar ese código.

La relación entre métodos, clases y otros tipos de C# se verá con más detalle cuando lleguemos al tema de **clases**.

## Para qué sirve un método

Un método existe para no tener que reescribir el mismo bloque de código cada vez que lo necesitamos. En vez de copiar y pegar diez veces la misma lógica, se define una sola vez con un nombre, y se llama a ese nombre desde donde haga falta.

Esto no es solo comodidad al escribir: si más adelante hay que corregir o cambiar esa lógica, se cambia en un único sitio, en vez de tener que buscar y actualizar diez copias distintas.

## Estructura básica

```csharp
static int Sumar(int a, int b)
{
    return a + b;
}

int resultado = Sumar(3, 4); // 7
```

- `static` indica que el método pertenece al tipo y no a una instancia. De momento todos nuestros métodos serán `static` porque todavía no hemos visto clases ni instancias. Esto se entenderá mejor en el tema de **clases**.
- El tipo antes del nombre (`int` en este caso) es el tipo de dato que devuelve el método. Si no devuelve ningún valor, se utiliza `void`.
- `Sumar` es el nombre del método.
- Lo que aparece entre paréntesis son sus parámetros.
- `return` termina la ejecución del método y devuelve un valor al código que lo llamó.

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

## Orden de declaración

En un lenguaje interpretado, ejecutado línea a línea de arriba a abajo, llamar a algo que todavía no se ha "leído" daría error. En C#, al ser un lenguaje compilado, el compilador procesa el archivo entero antes de ejecutar nada, así que puede llamarse a un método declarado más abajo en el código sin ningún problema:

```csharp
Console.WriteLine(Sumar(2, 3)); // esto funciona aunque Sumar se declare después

static int Sumar(int a, int b)
{
    return a + b;
}
```

Esto no es una particularidad exclusiva de C#, pero conviene tenerlo claro cuanto antes: el orden en el que los métodos aparecen escritos en el archivo no determina el orden en el que se pueden llamar.

## Expresión de cuerpo (expression-bodied member)

Cuando el cuerpo de un método es una única expresión, se puede escribir de forma abreviada con `=>` en vez de `{ return ...; }`:

```csharp
static int Sumar(int a, int b) => a + b;

// equivale a:
static int Sumar(int a, int b)
{
    return a + b;
}
```

Esto solo funciona si el cuerpo es una sola expresión. En cuanto un método necesita una variable local, un bucle, varias instrucciones seguidas, etc., hay que volver a la sintaxis con `{ }` — no es una cuestión de estilo, el compilador no admite `=>` para nada que no sea una única expresión.

Ojo: este `=>` no es una expresión lambda, aunque el símbolo sea idéntico. Una lambda es una función anónima que se puede guardar en una variable o pasar como argumento a otro método — un concepto bastante distinto, que se ve más adelante en su propio tema.

```csharp
Func<int, int> Duplicar = x => x * 2; // esto SÍ es una lambda, no un método
```

El contexto determina qué significa `=>`: en la firma de un método (como en `Sumar` arriba) es solo una forma breve de escribir su cuerpo.

## Una responsabilidad por método

Aunque un desarrollador pueda crear un método que haga cualquier cosa que desee, lo más indicado es que un método se encargue de hacer una única cosa. Si para describir qué hace necesitas un "y" ("calcula la potencia **y** la división"), por ejemplo, probablemente debería ser dos métodos, no uno.

```csharp
// Esto es un ejemplo de un mal método: mezcla dos cálculos sin relación en un único método
static string CalcularPotenciaYDivision(int a, int b)
{
    int potencia = (int)Math.Pow(a, b);
    double division = (double)a / b;
    return $"Potencia: {potencia}, división: {division}";
}
```

Este método es difícil de nombrar bien (justo porque hace dos cosas), obliga a calcular ambas aunque solo necesites una, y si algún día hay que probarlo por separado (tema de testing), no se puede aislar cada cálculo.

Se debe mencionar, que un método puede realizar decenas de instrucciones, no tiene por que hacer una única cuenta, pero este conjunto de instrucciones deben servir para lograr obtener un único resultado esperado siempre, no un conjunto de operaciones separadas que dan resultados inconexos.

Un caso muy común de esto es mezclar **cálculo** con **presentación**: calcular un dato y, en el mismo método, devolverlo ya convertido en un texto para mostrar. Es mejor separarlo en dos:

```csharp
static double CalcularMedia(int[] valores)
{
    int suma = 0;
    foreach (int valor in valores)
    {
        suma += valor;
    }
    return (double)suma / valores.Length;
}

static string DescribirMediaEdades(int[] edades)
{
    double media = CalcularMedia(edades);
    return $"La edad media es de {media}.";
}
```

`CalcularMedia` es reutilizable para cualquier media (edades, precios, notas...) y se puede usar en más cálculos sin tener que "desmontar" un string. `DescribirMediaEdades` solo se ocupa de convertir ese número en un mensaje legible. Si mañana necesitas la media de precios, reutilizas `CalcularMedia` directamente; con el método mezclado de antes, tendrías que copiar y adaptar todo el bloque.

## Lo que falta

Este tema deja fuera, a propósito, varios mecanismos que sí existen en C# pero que necesitan más contexto para presentarse bien: sobrecarga, `ref`/`out`, y valores por defecto. Se verán en el tema **Métodos II**.