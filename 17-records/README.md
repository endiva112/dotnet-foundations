# 17 - Records

## Qué es un record

Un `record` es una forma de declarar un tipo pensado para representar datos, con una diferencia de fondo frente a una `class` normal: la igualdad se basa en el **contenido**, no en la identidad del objeto. En Herencia y polimorfismo se vio que `object.Equals()` (heredado por cualquier `class`) compara si dos variables apuntan al mismo objeto en el heap — dos objetos con los mismos valores, pero creados por separado, no son iguales:

```csharp
public class PersonaClase
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
}
```

```csharp
PersonaClase p1 = new PersonaClase { Nombre = "Ana", Edad = 30 };
PersonaClase p2 = new PersonaClase { Nombre = "Ana", Edad = 30 };

Console.WriteLine(p1 == p2); // false — son dos objetos distintos, aunque tengan los mismos datos
```

Un `record` resuelve esto automáticamente, sin que haga falta escribir nada a mano:

```csharp
public record PersonaRecord
{
    public string Nombre { get; init; }
    public int Edad { get; init; }
}
```

```csharp
PersonaRecord r1 = new PersonaRecord { Nombre = "Ana", Edad = 30 };
PersonaRecord r2 = new PersonaRecord { Nombre = "Ana", Edad = 30 };

Console.WriteLine(r1 == r2); // true — mismo contenido, aunque sean dos objetos distintos
```

`record` sigue siendo, por debajo, un tipo por referencia que vive en el heap — no cambia esa parte. Lo que cambia es qué significa que dos records sean "iguales": el compilador genera automáticamente una comparación campo a campo, en vez de dejar la comparación por defecto de `object` (identidad).

Nótese `init` en vez de `set` en el ejemplo — es una pieza nueva de este tema, se explica en su propia sección más abajo.

## Sintaxis posicional

Escribir un record con propiedades explícitas, como arriba, funciona, pero para el caso más habitual (un conjunto de datos, sin lógica adicional) existe una forma mucho más compacta, llamada **sintaxis posicional**:

```csharp
public record Persona(string Nombre, int Edad);
```

Esta única línea es equivalente al ejemplo largo de la sección anterior — declara `Nombre` y `Edad` como propiedades públicas con `init`, y además genera un constructor que las recibe en ese mismo orden:

```csharp
Persona persona = new Persona("Ana", 30);
Console.WriteLine(persona.Nombre); // "Ana"
```

Además del constructor y las propiedades, el compilador genera automáticamente varios miembros más a partir de esta única línea: `Equals`, `GetHashCode`, un `ToString()` con un formato legible por defecto, y un método `Deconstruct` (se retoma en Deconstrucción y patrones posicionales, más abajo).

```csharp
Console.WriteLine(persona); // "Persona { Nombre = Ana, Edad = 30 }"
```

Este `ToString()` generado es considerablemente más útil que el de `object` (visto en Herencia, que solo daba el nombre del tipo) — y no ha hecho falta escribir ningún `override` para conseguirlo.

## El accesor `init`

Ya se conocían `get; set;` (mutable en cualquier momento) y `get; private set;` (mutable solo desde dentro de la propia clase, visto en Clases). `init` es un término intermedio: la propiedad se puede asignar durante la construcción del objeto (en el constructor, o con inicializador de objeto `{ Nombre = "Ana" }`), pero se vuelve de solo lectura en cuanto el objeto termina de construirse:

```csharp
Persona persona = new Persona("Ana", 30);
persona.Nombre = "Luis"; // error de compilación: la propiedad es de solo init, no se puede asignar después de construir el objeto
```

No es exclusivo de records — `init` es un accesor del lenguaje en general, y se podría usar en una `class` normal con propiedades explícitas. Pero es en los records donde encaja de forma natural: si la igualdad se basa en el contenido, tiene sentido que ese contenido no pueda cambiar por sorpresa después de haberse comparado o guardado en algún sitio. Un record no está obligado a ser inmutable (`init` se puede sustituir por `set` si de verdad hace falta mutabilidad), pero es la convención por defecto, y la sintaxis posicional siempre genera `init`, nunca `set`.

## Expresiones `with`

Si `Nombre` y `Edad` son de solo `init`, ¿cómo se obtiene una `Persona` con un dato distinto? No se modifica la existente — se crea una copia nueva, cambiando solo lo que haga falta, con una expresión `with`:

```csharp
Persona persona = new Persona("Ana", 30);
Persona personaMasVieja = persona with { Edad = 31 };

Console.WriteLine(persona.Edad);         // 30 — sin cambios
Console.WriteLine(personaMasVieja.Edad); // 31 — la copia nueva
```

`with` copia todas las propiedades del objeto original, y solo sobrescribe las que se indiquen explícitamente dentro de las llaves — el resto (`Nombre`, en este caso) se mantiene igual sin tener que repetirlo. Esta copia es superficial: si alguna propiedad fuera, en vez de un `string` o un `int`, un objeto mutable compartido, la copia seguiría apuntando al mismo objeto que el original. No hay ningún caso así todavía en el temario, así que no tiene consecuencias prácticas por ahora — se retoma si llega a hacer falta, una vez existan colecciones con las que darle un ejemplo real.

## Igualdad por contenido

Ya se ha visto que dos records con los mismos valores son `==`. Merece la pena ver el matiz de qué pasa cuando los tipos no coinciden exactamente:

```csharp
public record Empleado(string Nombre, int Edad);
```

```csharp
Persona persona = new Persona("Ana", 30);
Empleado empleado = new Empleado("Ana", 30);

// persona == empleado; // ni siquiera compila: son tipos sin relación entre sí
```

Y con herencia entre records (que se ve en detalle más abajo), dos records con los mismos valores pero de tipos distintos dentro de la misma jerarquía tampoco son iguales:

```csharp
public record Gerente(string Nombre, int Edad, int PersonasACargo) : Empleado(Nombre, Edad);
```

```csharp
Empleado empleado = new Empleado("Ana", 30);
Gerente gerente = new Gerente("Ana", 30, 5);

Empleado comoEmpleado = gerente; // upcasting, visto en Herencia

Console.WriteLine(empleado.Equals(comoEmpleado)); // false — aunque Nombre y Edad coincidan, el tipo real no es el mismo
```

La comparación generada automáticamente tiene en cuenta el **tipo real** del objeto, no solo los valores de las propiedades que el tipo de la variable deja ver — el mismo principio de "tipo de la variable frente a tipo real del objeto" que ya se vio en Herencia, aplicado aquí a la igualdad en vez de a qué miembros son alcanzables.

Todo esto (`Equals`, el operador `==`, `GetHashCode` consistente con ambos) se genera implementando por debajo una interfaz llamada `IEquatable<T>` — las interfaces todavía no se han visto formalmente, así que de momento basta con saber que existe ese mecanismo detrás; se retoma en el tema de Interfaces.

## Deconstrucción y patrones posicionales

En Pattern Matching II quedó pendiente el uso de patrones posicionales, porque dependían de un método `Deconstruct` que ningún tipo del temario tenía todavía. Un record generado con sintaxis posicional lo trae automáticamente, uno por cada propiedad, en el mismo orden en que se declararon.

Esto permite **deconstruir** un record en variables sueltas, de una sola vez:

```csharp
Persona persona = new Persona("Ana", 30);

var (nombre, edad) = persona;

Console.WriteLine(nombre); // "Ana"
Console.WriteLine(edad);   // 30
```

Y encaja directamente como patrón dentro de `is` o de una expresión `switch`, combinándose con lo ya visto en Pattern Matching II — incluyendo relacional y `when`:

```csharp
string Clasificar(Persona persona) => persona switch
{
    Persona(var nombre, < 18) => $"{nombre} es menor de edad",
    Persona(var nombre, >= 65) => $"{nombre} está jubilado",
    Persona(var nombre, _) => $"{nombre} es adulto"
};
```

Cada posición dentro de los paréntesis corresponde a una propiedad, en el mismo orden de la declaración posicional (`Nombre`, luego `Edad`) — `_` descarta esa posición sin darle nombre, igual que `_` ya funcionaba como patrón discard en un `switch` (Pattern Matching I).

## Herencia entre records

Un `record class` puede heredar de otro record, con una sintaxis que combina la posicional con `base(...)` (visto en Herencia):

```csharp
public record Empleado(string Nombre, int Edad);

public record Gerente(string Nombre, int Edad, int PersonasACargo) : Empleado(Nombre, Edad);
```

`: Empleado(Nombre, Edad)` cumple el mismo papel que `: base(nombre, edad)` en una clase normal — delega en el constructor de la base, pasándole los parámetros posicionales que le corresponden. `with`, la deconstrucción y la igualdad por contenido siguen funcionando igual a través de la jerarquía, sin nada adicional que escribir.

## `record struct`

Todo lo anterior (`record`, o de forma más explícita `record class`) es un tipo por referencia. **`record struct`** aplica exactamente el mismo paquete de miembros generados (`Equals`, `GetHashCode`, `==`/`!=`, `ToString()`, `Deconstruct`) pero sobre un tipo por valor — resolviendo el pendiente que quedó explícito en Structs: *"para código de producción, es habitual implementar `IEquatable<T>`... algo que se retoma cuando se vea `record struct`, que resuelve esto de forma automática"*.

```csharp
public record struct Punto(int X, int Y);
```

```csharp
Punto p1 = new Punto(1, 1);
Punto p2 = new Punto(1, 1);

Console.WriteLine(p1 == p2); // true — a diferencia del struct normal de Structs, aquí == sí compila y compara por valor
```

Recordando Structs: un `struct` normal no tiene `==` definido por defecto, y hacía falta escribirlo a mano (o conformarse con el `Equals` heredado, menos eficiente). Un `record struct` lo genera automáticamente, igual que un `record class` — la diferencia entre ambos sigue siendo la de siempre entre `class` y `struct` (por referencia frente a por valor, vista en Structs), no algo nuevo de este tema.

A diferencia de `record class`, un `record struct` es **mutable por defecto** — genera propiedades con `set`, no `init`:

```csharp
Punto punto = new Punto(1, 1);
punto.X = 5; // válido: record struct usa set por defecto, no init
```

Si se quiere la misma inmutabilidad que un `record class` tiene por defecto, se añade `readonly`:

```csharp
public readonly record struct Punto(int X, int Y);
```

```csharp
Punto punto = new Punto(1, 1);
punto.X = 5; // ahora sí, error de compilación: las propiedades son de solo init
```

Cuál de las dos variantes conviene depende del mismo criterio ya visto en Structs (dato pequeño, de vida corta, que se comporta como un valor) — no hay una discusión nueva que añadir aquí, solo la opción de que ese struct también obtenga igualdad por valor gratis si se declara como record.

## Sobrecarga de operadores: `==` y `!=`

Structs dejó otro pendiente: cómo se define `==` a mano en un tipo que no lo trae por defecto. Un record lo genera automáticamente, pero conviene ver cómo se escribiría si no fuera un record — porque el mismo mecanismo, **sobrecarga de operadores**, sirve para cualquier tipo propio, no solo para resolver igualdad.

```csharp
public struct PuntoManual
{
    public int X { get; set; }
    public int Y { get; set; }

    public PuntoManual(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static bool operator ==(PuntoManual a, PuntoManual b)
    {
        return a.X == b.X && a.Y == b.Y;
    }

    public static bool operator !=(PuntoManual a, PuntoManual b)
    {
        return !(a == b);
    }
}
```

```csharp
PuntoManual p1 = new PuntoManual(1, 1);
PuntoManual p2 = new PuntoManual(1, 1);

Console.WriteLine(p1 == p2); // true — ahora sí compila, con la lógica que se ha definido
```

La sintaxis es `public static bool operator ==(TipoA a, TipoB b)` — siempre `static`, siempre recibiendo ambos operandos como parámetros, y devolviendo lo que corresponda al resultado de esa comparación (`bool`, en el caso de `==`). C# obliga a definir `==` y `!=` **siempre en pareja**: si se sobrecarga uno, hay que sobrecargar el otro, o el código no compila. Tiene sentido — sería contradictorio que un tipo supiera decir "estos dos son iguales" pero no fuera capaz de decir "estos dos no son iguales".

Existen otros operadores sobrecargables (`+`, `-`, `<`, `>`, entre otros), con la misma sintaxis general — no se desarrollan aquí porque nada en el temario los necesita todavía; el mecanismo ya queda claro con `==`/`!=`, que es el caso que sí tenía un pendiente real detrás.

Comparado con esto, la ventaja de un record queda más visible: todo este bloque (`operator ==`, `operator !=`, y además `GetHashCode` consistente con ambos, que aquí ni siquiera se ha escrito) se obtiene con una sola palabra clave, sin margen para el error de que ambos operadores queden inconsistentes entre sí.