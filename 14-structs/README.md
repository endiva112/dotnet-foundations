# 14 - Structs

## Qué es un struct

Un `struct` se declara casi igual que una clase — campos, propiedades, constructores, métodos:

```csharp
public struct Punto
{
    public int X { get; set; }
    public int Y { get; set; }

    public Punto(int x, int y)
    {
        X = x;
        Y = y;
    }

    public double Distancia(Punto otro)
    {
        int dx = X - otro.X;
        int dy = Y - otro.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}
```

```csharp
Punto p1 = new Punto(0, 0);
Punto p2 = new Punto(3, 4);
Console.WriteLine(p1.Distancia(p2)); // 5
```

Sintácticamente, hasta aquí no hay ninguna sorpresa. La diferencia real está en cómo se comporta, no en cómo se escribe.

## La diferencia real: tipo por valor, no por referencia

Ya se dejó anotado en el tema de NRT: un `struct` se comporta como `int` o `double`, no como `string` o un array. Se copia al asignarlo o pasarlo — nunca se comparte la referencia:

```csharp
Punto original = new Punto(1, 1);
Punto copia = original;

copia.X = 99;

Console.WriteLine(original.X); // 1 — original no cambió
Console.WriteLine(copia.X);    // 99
```

Con una clase, esto se comportaría completamente distinto — `copia` y `original` serían el mismo objeto, y cambiar uno cambiaría el otro (tal como se vio con arrays en el tema 05, y de nuevo con clases en el tema 12). Con un struct, `copia` es un valor independiente desde el momento de la asignación.

## El "gotcha" de mutar un struct sin darse cuenta

Esta diferencia tiene una consecuencia que sorprende la primera vez que se pisa, y conviene precisarla bien porque es fácil malinterpretarla. Lo único que es de solo lectura en la variable de un `foreach` es la variable en sí — no se puede reasignar a otro objeto o valor, ni con una clase ni con un struct:

```csharp
foreach (Punto p in puntos)
{
    p = new Punto(0, 0); // error de compilación (CS1656), tanto si Punto es struct como si fuera class
}
```

La diferencia aparece al escribir en una propiedad, no al reasignar la variable entera — y ahí es donde struct y class se comportan de forma opuesta:

```csharp
Punto[] puntos = new Punto[] { new Punto(1, 1), new Punto(2, 2) };

foreach (Punto p in puntos)
{
    p.X = 0; // error de compilación (CS1654)
}
```

Con una clase, la misma operación compila sin problema:

```csharp
public class Caja
{
    public int Valor { get; set; }
}

Caja[] cajas = new Caja[] { new Caja { Valor = 1 }, new Caja { Valor = 2 } };

foreach (Caja c in cajas)
{
    c.Valor = 10; // esto sí compila, y además modifica el objeto real
}
```

El motivo de fondo es el mismo de siempre (tipo por valor frente a tipo por referencia), pero aplicado con cuidado: `c` es una referencia — una dirección que apunta a un objeto en el heap. Escribir `c.Valor = 10` no toca esa dirección en absoluto, solo el contenido del objeto al que apunta; la variable `c` en sí no cambia. `p`, en cambio, cuando `Punto` es un struct, no es una dirección — es el valor completo, con `X` e `Y` viviendo físicamente dentro de la propia variable. Escribir `p.X = 0` sí es modificar la variable `p` misma, porque `X` es parte de su contenido, no algo aparte a lo que apunta. Y como la variable de un `foreach` es de solo lectura, el compilador lo bloquea.

Más sutil todavía: modificar el resultado de una propiedad o un método que devuelve un struct no modifica nada real, porque lo que se obtiene es una **copia temporal**, no el original:

```csharp
public class Rectangulo
{
    public Punto Origen { get; set; }
}

Rectangulo rect = new Rectangulo();
rect.Origen.X = 5; // error de compilación: no se puede modificar el valor de retorno de una propiedad, porque no es una variable
```

El compilador impide directamente este último caso (no compila), precisamente porque modificar esa copia sería una operación que no tendría ningún efecto real y probablemente no es lo que se pretendía — es preferible un error de compilación claro a un bug silencioso donde el cambio simplemente se pierde.

## Paso por valor en métodos

Conectando con Métodos II (tema 13): un struct pasado como parámetro se copia, igual que un `int`. Si un método necesita modificar el struct original, hace falta `ref`, exactamente el mismo mecanismo ya visto:

```csharp
static void Mover(ref Punto p, int dx, int dy)
{
    p.X += dx;
    p.Y += dy;
}
```

```csharp
Punto punto = new Punto(0, 0);
Mover(ref punto, 5, 5);
Console.WriteLine(punto.X); // 5 — se modificó el original, gracias a ref
```

Sin `ref`, el método recibiría una copia y cualquier cambio dentro de él se perdería al terminar, sin ningún error ni aviso — el struct original se quedaría exactamente igual que antes de la llamada.

## Valor por defecto y constructor sin parámetros

Todo struct tiene un estado "vacío" automático, con cada campo en su valor por defecto (tema 05):

```csharp
Punto vacio = default(Punto); // X = 0, Y = 0
Punto tambienVacio = new Punto(); // igual, mientras no exista un constructor propio sin parámetros
```

Desde versiones recientes de C#, también se puede escribir un constructor sin parámetros propio (algo que antes no estaba permitido en absoluto). Y aquí `default` y `new Punto()` **dejan de ser intercambiables**:

```csharp
public struct Punto
{
    public int X { get; set; }
    public int Y { get; set; }

    public Punto() // constructor sin parámetros, definido a mano
    {
        X = -1;
        Y = -1;
    }

    public Punto(int x, int y)
    {
        X = x;
        Y = y;
    }
}
```

```csharp
Punto a = new Punto();
Console.WriteLine(a.X); // -1 — se ejecutó el constructor personalizado

Punto b = default;
Console.WriteLine(b.X); // 0 — default nunca ejecuta ningún constructor, solo pone cada campo a su valor por defecto
```

La diferencia de fondo: `default` **nunca** ejecuta ningún constructor, sea el que sea — simplemente pone cada campo a su valor por defecto directamente. `new Punto()` sí ejecuta el constructor sin parámetros si existe uno definido, y solo cae en los valores por defecto de cada campo cuando no hay ningún constructor propio que lo sustituya.

## Structs no heredan, pero pueden implementar interfaces

Un `struct` no puede heredar de otro struct ni de una clase, y tampoco puede ser heredado — a diferencia de una clase, esto no es una limitación temporal del curso, es una restricción real del lenguaje. Lo único que un struct puede hacer en esta dirección es implementar interfaces, igual que una clase (tema de Interfaces, más adelante). El motivo de fondo se retoma cuando se compare directamente con clases, en el tema siguiente.

## Una advertencia sobre `==`

A diferencia de lo que se podría esperar, un struct **no** tiene el operador `==` definido por defecto:

```csharp
Punto a = new Punto(1, 1);
Punto b = new Punto(1, 1);

Console.WriteLine(a == b); // no compila, si Punto no define == explícitamente
```

Sí existe un `Equals(object)` heredado automáticamente, que proporciona igualdad por valor (compara el contenido, no la identidad) — pero su implementación por defecto no es la más eficiente, y los detalles de cómo lo consigue por debajo varían según el struct. Para código de producción, es habitual implementar la interfaz `IEquatable<T>` en el propio struct, ganando una comparación más rápida y explícita — algo que se retoma cuando se vea `record struct`, en el tema de Records, que resuelve esto de forma automática sin tener que escribirlo a mano.

## Cuándo usar struct en vez de class

No es una regla mecánica, sino una guía de diseño: un struct encaja bien para datos pequeños, de vida corta, que se comportan como "un valor" más que como "una entidad" — coordenadas, rangos, medidas, colores. Si los datos son grandes, se pasan mucho entre métodos sin `ref`, o representan algo con identidad propia que tiene sentido compartir (un `Cliente`, una `CuentaBancaria` del tema 12), una clase sigue siendo la opción correcta. Ante la duda, class es el valor por defecto razonable — struct se elige de forma consciente, no por costumbre.

## Resumen: struct frente a class

| | `struct` | `class` |
|---|---|---|
| Tipo | Por valor | Por referencia |
| Asignación / paso a método | Copia el contenido | Comparte la referencia |
| Herencia | No puede heredar ni ser heredado | Sí |
| Implementar interfaces | Sí | Sí |
| `==` por defecto | No compila sin definirlo | Compara referencias (identidad) |
| Dónde vive | Pila (o inline dentro de otro objeto) | Heap |