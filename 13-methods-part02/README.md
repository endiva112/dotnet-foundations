# 13 - Métodos II

## Sobrecarga (overload)

Recordando el tema 06: en top-level statements, dos métodos con el mismo nombre chocan, porque son funciones locales y viven en el ámbito de una función, no de un tipo — la sobrecarga en C# solo se resuelve dentro del ámbito de un tipo. Ahora que existen las clases (tema 12), ese ámbito ya está disponible: dos métodos con el mismo nombre pueden coexistir en la misma clase, siempre que se diferencien en el número o el tipo de sus parámetros.

```csharp
public class Calculadora
{
    public int Sumar(int a, int b)
    {
        return a + b;
    }

    public double Sumar(double a, double b)
    {
        return a + b;
    }

    public int Sumar(int a, int b, int c)
    {
        return a + b + c;
    }
}
```

El compilador elige automáticamente qué versión llamar según los argumentos que se le pasen:

```csharp
Calculadora calc = new Calculadora();
calc.Sumar(2, 3);       // llama a la versión de dos int
calc.Sumar(2.5, 3.5);   // llama a la versión de double
calc.Sumar(1, 2, 3);    // llama a la versión de tres int
```

**El tipo de retorno no cuenta para diferenciar una sobrecarga.** Dos métodos con la misma firma de parámetros pero distinto tipo de retorno no compilan, aunque a primera vista parezcan "diferentes":

```csharp
public int Sumar(int a, int b) { ... }
public double Sumar(int a, int b) { ... } // error: ya existe un método con esta firma de parámetros
```

## Parámetros por referencia: `ref`

Por defecto, los parámetros se pasan por valor (tema 06): el método recibe una copia, y modificarla dentro del método no afecta a la variable original (salvo que sea un tipo por referencia, como un array, donde modificar su contenido sí se nota). `ref` cambia esto para tipos por valor: el método recibe la variable original, no una copia, y cualquier cambio dentro del método se refleja fuera de él.

```csharp
static void Duplicar(ref int numero)
{
    numero *= 2;
}
```

```csharp
int valor = 5;
Duplicar(ref valor);
Console.WriteLine(valor); // 10
```

`ref` tiene que escribirse tanto en la declaración del método como en la llamada — no es opcional en ninguno de los dos sitios, precisamente para que quien lee la llamada sepa, sin mirar el método, que esa variable puede cambiar. Además, la variable pasada con `ref` debe estar ya inicializada antes de la llamada — a diferencia de `out`, que se ve justo debajo.

Un caso de uso clásico es intercambiar el valor de dos variables:

```csharp
static void Intercambiar(ref int a, ref int b)
{
    int temporal = a;
    a = b;
    b = temporal;
}
```

## Parámetros de salida: `out`

`out` es parecido a `ref`, pero pensado para que un método **devuelva más de un valor** a través de sus parámetros, además de (o en vez de) su valor de retorno normal:

```csharp
static void DividirConResto(int dividendo, int divisor, out int cociente, out int resto)
{
    cociente = dividendo / divisor;
    resto = dividendo % divisor;
}
```

```csharp
DividirConResto(17, 5, out int cociente, out int resto);
Console.WriteLine($"{cociente} y sobran {resto}"); // "3 y sobran 2"
```

Diferencia clave con `ref`: una variable pasada con `out` **no necesita estar inicializada antes** de la llamada (de hecho, se puede declarar directamente en la propia llamada, como arriba con `out int cociente`) — pero el método está obligado a asignarle un valor antes de terminar, en todos los caminos posibles de su código, o no compila.

Esto es exactamente el mecanismo detrás del patrón `TryX` mencionado en temas anteriores (`int.TryParse`, `Enum.TryParse`):

```csharp
if (int.TryParse(entrada, out int numero))
{
    Console.WriteLine($"Se convirtió correctamente: {numero}");
}
else
{
    Console.WriteLine("La entrada no era un número válido");
}
```

`TryParse` devuelve un `bool` (si la conversión tuvo éxito o no) y, a través de `out`, el valor convertido si lo hubo. Es la alternativa segura a `Parse` que se mencionó en BCL y en enums, sin necesitar una excepción para señalar el fallo.

## Valores por defecto (parámetros opcionales)

Un parámetro puede tener un valor por defecto, que se usa si la llamada no proporciona ese argumento:

```csharp
static void Saludar(string nombre, string saludo = "Hola")
{
    Console.WriteLine($"{saludo}, {nombre}");
}
```

```csharp
Saludar("Ana");            // "Hola, Ana"
Saludar("Ana", "Buenas");  // "Buenas, Ana"
```

Los parámetros con valor por defecto tienen que ir **después** de todos los parámetros obligatorios en la firma del método — no se puede tener un parámetro opcional seguido de uno obligatorio. Combinado con argumentos por nombre (tema 06), se puede saltar un parámetro opcional intermedio sin proporcionarlo:

```csharp
static void Configurar(string nombre, int reintentos = 3, bool verbose = false)
{
    // ...
}

Configurar("proceso", verbose: true); // usa reintentos = 3 por defecto, salta directo a verbose
```

## `params`: número variable de argumentos

`params` permite que un método acepte cualquier cantidad de argumentos del mismo tipo, sin tener que declarar de antemano cuántos:

```csharp
static int Sumar(params int[] numeros)
{
    int total = 0;
    foreach (int n in numeros)
    {
        total += n;
    }
    return total;
}
```

```csharp
Sumar(1, 2, 3);       // 6
Sumar(1, 2, 3, 4, 5); // 15
Sumar();              // 0 — también es válido, cero argumentos
```

Dentro del método, `numeros` es simplemente un array — se recorre igual que cualquier otro (tema 05). Al llamar, no hace falta construir el array explícitamente (aunque también se puede: `Sumar(new int[] { 1, 2, 3 })` funciona igual). `params` solo puede usarse en el **último** parámetro de la firma, y solo puede haber uno por método.

## Recursión

Un método puede llamarse a sí mismo — esto se llama **recursión**, y es una alternativa a un bucle para ciertos problemas que se definen de forma natural en términos de "una versión más pequeña de sí mismos":

```csharp
static int Factorial(int n)
{
    if (n <= 1)
    {
        return 1; // caso base: aquí se detiene la recursión
    }
    return n * Factorial(n - 1); // llamada recursiva, con un problema más pequeño
}
```

```csharp
Console.WriteLine(Factorial(5)); // 120 (5 * 4 * 3 * 2 * 1)
```

El **caso base** (`if (n <= 1) return 1;`) es imprescindible: sin él, el método se llamaría a sí mismo indefinidamente. Cada llamada recursiva añade un nuevo marco a la pila (tema 01) — si nunca se llega al caso base, la pila se llena y el programa falla con un `StackOverflowException` (a diferencia de otras excepciones, esta ni siquiera se puede capturar de forma fiable, así que la prevención real está en asegurarse de que el caso base siempre se alcanza, no en manejar el fallo después).

Cualquier recursión se puede reescribir como un bucle, y viceversa — no hay ningún cálculo que solo se pueda hacer de una de las dos formas. Se prefiere recursión cuando el problema se describe de forma más natural así (recorrer estructuras jerárquicas, como árboles, es el ejemplo más común, y se retomará cuando aparezcan ese tipo de estructuras).