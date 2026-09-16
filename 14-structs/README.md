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

Esta diferencia tiene una consecuencia que sorprende la primera vez que se pisa. Ya se vio con arrays que la variable de un `foreach` es de solo lectura — con structs, la razón de fondo es la misma:

```csharp
Punto[] puntos = new Punto[] { new Punto(1, 1), new Punto(2, 2) };

foreach (Punto p in puntos)
{
    p.X = 0; // error de compilación (CS1656)
}
```

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
Punto tambienVacio = new Punto(); // igual, si no se ha definido un constructor propio sin parámetros
```

Desde versiones recientes de C#, también se puede escribir un constructor sin parámetros propio (algo que antes no estaba permitido en absoluto):

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

## Structs no heredan, pero pueden implementar interfaces

Un `struct` no puede heredar de otro struct ni de una clase, y tampoco puede ser heredado — a diferencia de una clase, esto no es una limitación temporal del curso, es una restricción real del lenguaje. Lo único que un struct puede hacer en esta dirección es implementar interfaces, igual que una clase (tema de Interfaces, más adelante). El motivo de fondo se retoma cuando se compare directamente con clases, en el tema siguiente.

## Una advertencia sobre `==`

A diferencia de lo que se podría esperar, un struct **no** tiene el operador `==` definido por defecto:

```csharp
Punto a = new Punto(1, 1);
Punto b = new Punto(1, 1);

Console.WriteLine(a == b); // no compila, si Punto no define == explícitamente
```

Sí existe un `Equals(object)` heredado automáticamente, que funciona pero es más lento de lo que parece (compara campo a campo usando reflexión por debajo). Definir `==` a mano es posible pero se deja fuera de este tema; el porqué de esta limitación, y una alternativa que la resuelve de forma automática, se retoma cuando se vea `record struct`, en el tema de Records.

## Cuándo usar struct en vez de class

No es una regla mecánica, sino una guía de diseño: un struct encaja bien para datos pequeños, de vida corta, que se comportan como "un valor" más que como "una entidad" — coordenadas, rangos, medidas, colores. Si los datos son grandes, se pasan mucho entre métodos sin `ref`, o representan algo con identidad propia que tiene sentido compartir (un `Cliente`, una `CuentaBancaria` del tema 12), una clase sigue siendo la opción correcta. Ante la duda, class es el valor por defecto razonable — struct se elige de forma consciente, no por costumbre.