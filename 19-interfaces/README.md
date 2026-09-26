# 19 - Interfaces

## Qué es una interfaz

Una **interfaz** es un contrato: declara qué miembros debe tener un tipo, sin decir nada sobre cómo se implementan. Ninguna interfaz puede instanciarse directamente, y ninguno de sus miembros trae cuerpo, salvo la excepción que se ve más abajo en Miembros con implementación por defecto.

```csharp
public interface IValidable
{
    bool EsValido();
}
```

Cualquier clase que **implemente** esta interfaz se compromete a proporcionar ese método:

```csharp
public class Empleado : Persona, IValidable
{
    public decimal Salario { get; set; }

    public bool EsValido()
    {
        return Salario > 0;
    }
}
```

Nótese la sintaxis: `class Empleado : Persona, IValidable` — la misma `:` que ya se usaba para heredar de una clase (tema de Herencia) sirve también para implementar una interfaz, y ambas cosas se combinan en una sola lista separada por comas. Si `Empleado` no implementara `EsValido()`, no compilaría — el compilador exige que se cumpla el contrato completo, igual que exige que se sobrescriba un miembro `abstract` (tema de Herencia).

Por convención, el nombre de una interfaz empieza por `I` (`IValidable`, `IComparable`, `IEquatable<T>`...) — no es una regla del lenguaje, es una convención tan extendida en C# que romperla se lee como un error.

### Interfaz frente a clase abstracta

Ambas declaran algo que las derivadas están obligadas a implementar, pero resuelven problemas distintos:

| | Clase abstracta | Interfaz |
|---|---|---|
| Puede compartir código real entre derivadas | Sí | No (salvo el caso de Miembros con implementación por defecto) |
| Cuántas puede tener un mismo tipo | Una sola (herencia simple) | Cuantas hagan falta |
| Puede tener campos o estado propio | Sí | No |
| Puede tener constructor | Sí (`protected`, visto en Herencia) | No |

El "una sola clase base, pero cuantas interfaces hagan falta" es precisamente la solución que quedó anotada desde el principio de Herencia para el problema de que C# no permite heredar de dos clases a la vez: si un tipo necesita cumplir varios contratos distintos, no puede hacerlo con herencia, pero sí implementando varias interfaces:

```csharp
public class Empleado : Persona, IValidable, IIdentificable
{
    // ...
}
```

## Structs implementando interfaces

En Structs quedó anotado que un struct no puede heredar ni ser heredado, pero sí puede implementar interfaces — sin desarrollarlo más en su momento. El motivo de fondo es sencillo una vez se conoce cómo funciona una interfaz: implementar una interfaz no aporta ningún almacenamiento compartido ni ninguna identidad de tipo común entre struct y clase (a diferencia de heredar, que sí encadena estado y comportamiento entre tipos) — solo exige que el tipo tenga ciertos miembros con cierta firma. Nada de eso choca con ser un tipo por valor:

```csharp
public struct Punto : IValidable
{
    public int X { get; set; }
    public int Y { get; set; }

    public Punto(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool EsValido()
    {
        return X >= 0 && Y >= 0;
    }
}
```

`Punto` sigue siendo, en todo lo demás, exactamente el struct que ya se conocía (tipo por valor, se copia al asignar, vive en la pila o embebido dentro de otro objeto, tema de Pila y Heap) — implementar `IValidable` no cambia nada de eso.

## `IEquatable<T>`

Tanto en Structs como en Records quedó pendiente cómo se implementa `IEquatable<T>` a mano — la interfaz que hay detrás de la igualdad "rápida y explícita" que un `record`/`record struct` genera automáticamente. Retomando el struct `Punto` de Structs (sin ser un record, para verlo desde cero):

```csharp
public struct Punto : IEquatable<Punto>
{
    public int X { get; set; }
    public int Y { get; set; }

    public Punto(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Punto otro)
    {
        return X == otro.X && Y == otro.Y;
    }

    public override bool Equals(object obj)
    {
        return obj is Punto otro && Equals(otro);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Punto a, Punto b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(Punto a, Punto b)
    {
        return !a.Equals(b);
    }
}
```

`IEquatable<Punto>` añade `Equals(Punto otro)` — una versión que recibe directamente un `Punto`, sin pasar por `object`. Es más rápida que el `Equals(object)` heredado de `object` (visto en Herencia), porque evita el boxing que supondría comparar a través de `object` cuando ambos lados ya son, de hecho, del mismo tipo (boxing visto en Pila y Heap). Por eso Structs decía que la implementación por defecto "no es la más eficiente" — pasa por `object`, con ese coste extra; `IEquatable<T>` lo evita.

Nótese cuánto código hace falta escribir a mano para conseguir exactamente lo que un `record struct` (tema de Records) genera con una sola palabra clave: `Equals` tipado, `Equals(object)`, `GetHashCode` consistente con ambos, y los dos operadores. Ver esto de cerca es lo que deja claro por qué los records ahorran tanto.

## El patrón Null Object

Desde el propio alcance de Herencia quedó anotado este pendiente. El problema que resuelve: cuando un método puede devolver `null` (tema de NRT), cualquier código que lo use tiene que comprobarlo antes de llamar a nada sobre el resultado:

```csharp
public interface INotificador
{
    void Enviar(string mensaje);
}

public class NotificadorEmail : INotificador
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine($"Email enviado: {mensaje}");
    }
}
```

```csharp
INotificador? notificador = ObtenerNotificadorDelUsuario(); // puede devolver null si el usuario no configuró ninguno

if (notificador is not null)
{
    notificador.Enviar("Bienvenido");
}
```

Esta comprobación es correcta, pero si `Enviar(...)` se llama en muchos sitios distintos del programa, el mismo `if (notificador is not null)` se repite en todos ellos. El **patrón Null Object** propone una alternativa: en vez de devolver `null`, devolver una implementación real de la interfaz que simplemente no hace nada:

```csharp
public class SinNotificador : INotificador
{
    public void Enviar(string mensaje)
    {
        // no hace nada a propósito
    }
}
```

```csharp
INotificador notificador = ObtenerNotificadorDelUsuario(); // ya nunca devuelve null

notificador.Enviar("Bienvenido"); // siempre seguro, sin comprobar nada antes
```

`ObtenerNotificadorDelUsuario()` cambia para devolver `new SinNotificador()` en vez de `null` cuando no hay nada configurado. El resto del programa queda simplificado: nunca hace falta comprobar si `notificador` es `null`, porque siempre es un `INotificador` real — uno de ellos, simplemente, no hace nada al recibir la llamada. El coste es tener una clase más en el proyecto; el beneficio es no repetir la misma comprobación de `null` en cada sitio donde se use.

## Miembros con implementación por defecto

La regla general de este tema es que una interfaz no trae cuerpo en sus miembros. Desde C# 8, existe una excepción: una interfaz puede declarar un método con una implementación por defecto, que las clases que la implementan heredan automáticamente si no la sobrescriben:

```csharp
public interface INotificador
{
    void Enviar(string mensaje);

    void EnviarUrgente(string mensaje)
    {
        Enviar($"URGENTE: {mensaje}");
    }
}
```

```csharp
public class NotificadorEmail : INotificador
{
    public void Enviar(string mensaje)
    {
        Console.WriteLine($"Email enviado: {mensaje}");
    }

    // no hace falta implementar EnviarUrgente — se hereda la versión por defecto
}
```

```csharp
INotificador notificador = new NotificadorEmail();
notificador.EnviarUrgente("El servidor está caído"); // usa la implementación por defecto de la interfaz
```

Esto no convierte una interfaz en una clase abstracta con otro nombre — sigue sin poder tener campos ni estado propio, ni constructor, y sigue permitiendo implementar varias a la vez sin ningún conflicto de herencia múltiple. Su caso de uso principal no es "ahorrar código" como primera intención, sino permitir que una interfaz ya publicada y usada por otros (por ejemplo, en una librería) añada un miembro nuevo sin romper a quien ya la implementaba — antes de esta característica, añadir un método a una interfaz existente obligaba a todas sus implementaciones a actualizarse o dejaban de compilar; con un cuerpo por defecto, siguen compilando sin cambios.

## Polimorfismo a través de una interfaz

En Herencia, el polimorfismo se apoyaba en un tipo base común (`Animal`, `Figura`). Una interfaz cumple exactamente el mismo papel de "tipo común" para tratar objetos de forma uniforme, con una diferencia importante: los tipos que la implementan no tienen por qué compartir ninguna relación de herencia entre sí.

```csharp
public class NotificadorEmail : INotificador
{
    public void Enviar(string mensaje) => Console.WriteLine($"Email: {mensaje}");
}

public class NotificadorSms : INotificador
{
    public void Enviar(string mensaje) => Console.WriteLine($"SMS: {mensaje}");
}
```

```csharp
INotificador[] notificadores = { new NotificadorEmail(), new NotificadorSms(), new SinNotificador() };

foreach (INotificador notificador in notificadores)
{
    notificador.Enviar("Aviso"); // cada uno responde a su manera, sin que este código sepa cuál es cuál
}
```

`NotificadorEmail` y `NotificadorSms` no tienen ninguna relación entre sí — ni una hereda de la otra, ni ambas heredan de una clase común más allá de `object`. Lo único que comparten es implementar `INotificador`, y eso basta para que el mismo `foreach` funcione igual con cualquiera de las dos, exactamente con el mismo razonamiento que ya se vio en Herencia con una jerarquía de clases.

## Interfaces genéricas

`IEquatable<Punto>`, usado más arriba, ya es una **interfaz genérica** — recibe un tipo entre `< >`, igual que `Nullable<T>` (NRT) o `List<T>` (nombrado, aunque no visto todavía, en varios temas). No hace falta más que reconocerlo por ahora: qué significa exactamente ese `<T>` y cómo se escribe una interfaz o clase genérica propia se ve en el tema siguiente, Generics — aquí basta con saber que una interfaz puede parametrizarse igual que cualquier otro tipo.