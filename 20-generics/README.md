# 20 - Generics

## Qué problema resuelven

En Pila y Heap se vio el coste real del boxing: guardar un tipo por valor donde se espera un tipo por referencia obliga a copiarlo entero al heap. Esto aparece de forma natural en cuanto se intenta escribir un tipo que guarde "cualquier cosa", sin saber de antemano de qué tipo será:

```csharp
public class Caja
{
    public object Valor { get; set; }
}
```

```csharp
Caja cajaEntero = new Caja { Valor = 5 }; // boxing: 5 se copia al heap para poder guardarlo como object
int numero = (int)cajaEntero.Valor;        // hace falta un cast para recuperarlo

Caja cajaTexto = new Caja { Valor = "hola" };
int error = (int)cajaTexto.Valor; // compila, pero lanza InvalidCastException en runtime — nada impedía guardar un string aquí
```

Esta `Caja` tiene dos problemas a la vez: boxing en cada valor por valor que guarda, y ningún tipo real que el compilador pueda comprobar — cualquier cosa encaja en `Valor`, y el error de mezclar tipos solo aparece en tiempo de ejecución, con un cast que falla.

Los **genéricos** resuelven ambos problemas: permiten escribir un tipo (o un método) que funciona con cualquier tipo, sin fijar cuál de antemano, pero dejando que el compilador sepa exactamente cuál es en cada uso concreto — sin boxing, y sin necesitar ningún cast.

## Clases genéricas

```csharp
public class Caja<T>
{
    public T Valor { get; set; }
}
```

`T` es un **parámetro de tipo** — un tipo que todavía no se decide al escribir la clase, solo al usarla:

```csharp
Caja<int> cajaEntero = new Caja<int> { Valor = 5 }; // sin boxing: T es int, directamente
int numero = cajaEntero.Valor;                        // sin cast: el compilador ya sabe que Valor es int

Caja<string> cajaTexto = new Caja<string> { Valor = "hola" };
```

```csharp
cajaEntero.Valor = "hola"; // error de compilación: Caja<int> solo acepta int en Valor
```

El nombre `T` no tiene nada de especial (es una convención, no una palabra reservada) — se podría llamar de cualquier forma, pero `T` (de *type*) es la convención estándar para un único parámetro de tipo genérico.

Esto no es nuevo del todo: `Nullable<T>` (NRT) e `IEquatable<T>` (Interfaces) ya se habían usado, sin explicar entonces qué era exactamente ese `<T>` — es justo este mecanismo. `Nullable<int>` es, conceptualmente, muy parecido a la `Caja<int>` de arriba: un tipo genérico que envuelve un valor de un tipo concreto, decidido en el momento de usarlo.

## Métodos genéricos

Un método puede ser genérico sin que la clase entera lo sea:

```csharp
public class Utilidades
{
    public static T Primero<T>(T[] elementos)
    {
        return elementos[0];
    }
}
```

```csharp
int[] numeros = { 1, 2, 3 };
int primerNumero = Utilidades.Primero(numeros); // T se infiere como int, sin escribirlo

string[] nombres = { "Ana", "Luis" };
string primerNombre = Utilidades.Primero(nombres); // T se infiere como string
```

El compilador casi siempre puede **inferir** `T` a partir de los argumentos que se le pasan, sin que haga falta escribirlo explícitamente en la llamada. Si hiciera falta ser explícito (por ejemplo, porque no hay ningún argumento del que inferirlo), se escribiría `Utilidades.Primero<int>(numeros)`, con el tipo entre `< >` justo después del nombre del método.

## Varios parámetros de tipo

Un tipo genérico puede tener más de un parámetro de tipo a la vez, separados por comas:

```csharp
public class Par<TClave, TValor>
{
    public TClave Clave { get; set; }
    public TValor Valor { get; set; }
}
```

```csharp
Par<string, int> edadPorNombre = new Par<string, int> { Clave = "Ana", Valor = 30 };
```

No se desarrolla más aquí — solo para reconocer la sintaxis cuando aparezca en Colecciones, con `Dictionary<TKey, TValue>`.

## Restricciones (`where`)

Sin ninguna restricción, dentro de un método genérico no se puede hacer casi nada con un valor de tipo `T`, más allá de guardarlo, devolverlo o compararlo con `==`/`Equals` heredado de `object`. Por ejemplo, esto no compila:

```csharp
public static T Maximo<T>(T a, T b)
{
    return a > b ? a : b; // error de compilación: el compilador no sabe si T tiene el operador >
}
```

El compilador no puede asumir que **cualquier** `T` que alguien decida usar tenga `>` definido — porque, en general, no lo tiene (una `Persona` no sabe compararse con `>`, por ejemplo). Una **restricción** (`where T : ...`) le dice al compilador qué puede asumir sobre `T`, a cambio de reducir qué tipos concretos se aceptan:

```csharp
public static T Maximo<T>(T a, T b) where T : IComparable<T>
{
    return a.CompareTo(b) > 0 ? a : b;
}
```

```csharp
int mayor = Maximo(5, 3);                  // int implementa IComparable<int>
string mayorTexto = Maximo("ana", "luis"); // string implementa IComparable<string>
```

`where T : IComparable<T>` exige que cualquier tipo usado como `T` implemente esa interfaz (tema de Interfaces) — y como `CompareTo` sí está garantizado por el contrato de `IComparable<T>`, el compilador ya puede permitir la llamada. Si se intenta `Maximo(persona1, persona2)` sin que `Persona` implemente `IComparable<Persona>`, no compila — el error aparece al escribir el código, no al ejecutarlo.

Otras restricciones habituales:

```csharp
public class Repositorio<T> where T : class // T debe ser un tipo por referencia
{
    // ...
}

public struct Coordenada<T> where T : struct // T debe ser un tipo por valor
{
    // ...
}

public static T Crear<T>() where T : new() // T debe tener un constructor público sin parámetros
{
    return new T(); // sin esta restricción, "new T()" no compilaría: el compilador no sabría si T tiene ese constructor
}
```

Se pueden combinar varias restricciones sobre el mismo `T` (por ejemplo, `where T : class, new()`), aunque no es algo que haga falta explorar a fondo aquí — el objetivo de esta sección es que la sintaxis y el motivo de fondo (decirle al compilador qué puede asumir) queden claros, no memorizar cada combinación posible.

## `default(T)`

Ya se vio en Arrays que cada tipo tiene un valor por defecto (`0` para tipos numéricos, `false` para `bool`, `null` para tipos por referencia). Dentro de un método genérico, sin restricciones, no se sabe de antemano si `T` va a ser un tipo por valor o por referencia — así que no se puede escribir directamente `null` ni `0` como valor por defecto genérico. `default(T)` (o simplemente `default`, cuando el tipo se infiere del contexto) resuelve esto:

```csharp
public static T ObtenerOPorDefecto<T>(T[] elementos, int indice)
{
    if (indice < 0 || indice >= elementos.Length)
    {
        return default; // 0 si T es int, null si T es string, etc. — según lo que T resulte ser en cada uso
    }

    return elementos[indice];
}
```

`default` da `0` (o el equivalente para cada tipo numérico), `false` para `bool`, `null` para cualquier tipo por referencia, y el struct "vacío" con cada campo a su valor por defecto para cualquier otro struct (tal como se vio en Structs) — el que corresponda a `T` en cada uso concreto de este método.

## Genéricos en lo que ya viene

`List<T>`, que se ve en el tema siguiente, no es ningún mecanismo especial del lenguaje — es exactamente una clase genérica como las de este tema, con un parámetro de tipo `T` para decidir qué guarda, y por debajo (entre otras cosas) un array que crece dinámicamente según hace falta. Todo lo visto aquí —sin boxing, sin cast, con el compilador comprobando el tipo— es lo que hace que `List<int>` guarde enteros de verdad, sin boxing, y que `List<Persona>` guarde referencias a `Persona`, sin tener que castear nada al leer un elemento.