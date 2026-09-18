# 15 - Herencia y polimorfismo

## Qué es la herencia

Ya se ha trabajado con clases que no tienen ninguna relación entre sí (`Persona`, `CuentaBancaria`, `Contador`...). En muchos dominios, sin embargo, varios tipos comparten datos y comportamiento, pero cada uno añade algo propio. Por ejemplo, un `Empleado` es una `Persona` (tiene nombre, edad, puede saludar), pero además tiene un salario y una lógica propia. Repetir `Nombre`, `Edad` y `Saludar()` en cada tipo que sea "un tipo de persona" duplicaría código sin necesidad.

La **herencia** permite que una clase (la **derivada**) reutilice los miembros de otra (la **base**), añadiendo o modificando solo lo que le es propio:

```csharp
public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }

    public void Saludar()
    {
        Console.WriteLine($"Hola, soy {Nombre} y tengo {Edad} años.");
    }
}

public class Empleado : Persona
{
    public decimal Salario { get; set; }
}
```

```csharp
Empleado empleado = new Empleado();
empleado.Nombre = "Ana";
empleado.Edad = 30;
empleado.Salario = 25000;
empleado.Saludar(); // "Hola, soy Ana y tengo 30 años." — heredado de Persona, sin reescribirlo
```

`Empleado` no redeclara `Nombre`, `Edad` ni `Saludar()` — los obtiene directamente de `Persona`. La sintaxis `class Empleado : Persona` se lee como "`Empleado` hereda de `Persona`", o "`Empleado` es una `Persona`".

## Herencia simple

C# solo permite que una clase herede de **una única** clase base directa:

```csharp
public class Empleado : Persona, OtraClase // error de compilación: no se puede heredar de dos clases
```

Esto es distinto de otros lenguajes que sí permiten herencia múltiple de clases. La forma en que C# resuelve la necesidad de que un tipo cumpla "varios contratos" a la vez no es heredando de varias clases, sino **implementando varias interfaces** — un mecanismo distinto, que se ve en su propio tema más adelante.

## Constructores en la jerarquía

Un constructor de la clase derivada no inicializa automáticamente los datos que pertenecen a la clase base — tiene que delegar explícitamente en un constructor de la base, con `base(...)`:

```csharp
public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }

    public Persona(string nombre, int edad)
    {
        Nombre = nombre;
        Edad = edad;
    }
}

public class Empleado : Persona
{
    public decimal Salario { get; set; }

    public Empleado(string nombre, int edad, decimal salario) : base(nombre, edad)
    {
        Salario = salario;
    }
}
```

```csharp
Empleado empleado = new Empleado("Ana", 30, 25000);
```

`: base(nombre, edad)` llama al constructor `Persona(string, int)` antes de ejecutar el cuerpo del constructor de `Empleado` — la clase base siempre termina de construirse primero, y solo entonces se ejecuta lo propio de la derivada.

Si `Persona` no tuviera ningún constructor propio, `base()` sería opcional (C# lo asume implícitamente, llamando al constructor vacío por defecto). Pero en cuanto `Persona` define un constructor con parámetros —tal como ocurrió en el tema de Clases—, su constructor vacío por defecto deja de existir. Eso significa que `Empleado` está obligado a llamar explícitamente a `base(nombre, edad)`, o a algún otro constructor de `Persona` que exista; si no lo hace, no compila.

### Herencia multinivel

La cadena de herencia no se limita a un único nivel — una clase derivada puede a su vez ser la base de otra:

```csharp
public class Gerente : Empleado
{
    public int PersonasACargo { get; set; }

    public Gerente(string nombre, int edad, decimal salario, int personasACargo)
        : base(nombre, edad, salario)
    {
        PersonasACargo = personasACargo;
    }
}
```

`base(...)` aquí llama al constructor de `Empleado`, que a su vez llama al de `Persona` — la cadena se resuelve nivel a nivel, de la derivada más lejana hacia la base más raíz, cada una terminando de construirse antes de pasar a la siguiente.

## Modificadores de acceso: `protected` e `internal`

Con solo `public` y `private` (tema de Clases), un miembro o es visible para todo el mundo o solo para la propia clase — no hay término medio para "visible para esta clase y sus derivadas, pero no para cualquier otro código". `protected` cubre exactamente eso:

```csharp
public class Persona
{
    protected string DatoInterno; // visible en Persona y en cualquier clase derivada, no fuera

    private string SoloPersona; // ni siquiera Empleado puede acceder a esto
}

public class Empleado : Persona
{
    public void Metodo()
    {
        DatoInterno = "algo"; // funciona, Empleado hereda de Persona
        SoloPersona = "algo"; // error de compilación: SoloPersona es private en Persona
    }
}
```

`internal`, por su parte, restringe la visibilidad a nivel de proyecto: un miembro (o incluso una clase entera) marcado `internal` es visible desde cualquier sitio dentro del mismo proyecto, pero no desde otro proyecto que lo referencie como dependencia.

```csharp
internal class Configuracion
{
    // visible en todo este proyecto, invisible desde fuera de él
}
```

Con un único proyecto en el temario hasta ahora, la diferencia entre `internal` y `public` no se nota en la práctica — se volverá relevante en cuanto exista más de un proyecto (por ejemplo, una librería separada del programa que la consume), algo que se retoma en Proyectos .NET II. Existen además combinaciones de estos cuatro modificadores (`protected internal`, `private protected`), que se dejan fuera por la misma razón: su utilidad solo se aprecia con varios proyectos de por medio.

Resumen de lo cubierto hasta ahora:

| Modificador | Visible desde |
|---|---|
| `public` | Cualquier código, de cualquier proyecto |
| `internal` | Cualquier código, dentro del mismo proyecto |
| `protected` | La propia clase y sus derivadas, en cualquier proyecto |
| `private` | Solo la propia clase |

## `virtual` y `override`: el mecanismo de polimorfismo

Heredar un método tal cual, como `Saludar()` en el primer ejemplo, es útil pero limitado: ¿qué pasa si `Empleado` necesita saludar de forma distinta, mencionando su puesto? Podría declarar un método nuevo con otro nombre, pero eso rompe la idea de que ambos tipos "saben saludar" de forma intercambiable. La solución es permitir que la clase derivada **reemplace** la implementación heredada, conservando el mismo nombre:

```csharp
public class Persona
{
    public string Nombre { get; set; }

    public virtual void Saludar()
    {
        Console.WriteLine($"Hola, soy {Nombre}.");
    }
}

public class Empleado : Persona
{
    public override void Saludar()
    {
        Console.WriteLine($"Hola, soy {Nombre} y trabajo aquí.");
    }
}
```

`virtual` en la base marca el método como "reemplazable"; sin `virtual`, una clase derivada no puede darle una implementación propia con el mismo nombre y firma (el intento más cercano sin `virtual` es `new`, que se explica más abajo, y no es lo mismo). `override` en la derivada indica explícitamente que se está reemplazando esa implementación, no declarando un método nuevo sin relación.

La parte que hace esto polimorfismo de verdad: **la versión que se ejecuta depende del tipo real del objeto, no del tipo de la variable usada para llamarlo**.

```csharp
Persona persona1 = new Persona { Nombre = "Ana" };
Persona persona2 = new Empleado { Nombre = "Luis" }; // variable de tipo Persona, objeto real de tipo Empleado

persona1.Saludar(); // "Hola, soy Ana."
persona2.Saludar(); // "Hola, soy Luis y trabajo aquí." — se ejecuta el override, no la versión de Persona
```

Aunque `persona2` está declarada como `Persona`, el objeto al que apunta es realmente un `Empleado` — y es ese tipo real, no el de la variable, el que decide qué versión de `Saludar()` se ejecuta. Esto es lo que permite tratar una colección de `Persona` (una vez se vea `List<T>`, en el tema de colecciones) que en realidad contenga una mezcla de `Persona` y `Empleado`, y que cada uno salude a su manera sin que el código que los recorre necesite saber de qué tipo concreto es cada uno.

### El tipo de la variable limita lo que se puede usar, no lo que el objeto tiene

`virtual`/`override` es la excepción, no la regla: para todo lo demás, es el **tipo de la variable** el que decide qué se puede hacer con un objeto, sin que importe lo que el objeto en sí contenga realmente. Se ve claro con un miembro que **no** es un override de nada — uno que existe únicamente en la clase derivada:

```csharp
public class Empleado : Persona
{
    public string Empresa { get; set; }

    public void MencionarEmpresa()
    {
        Console.WriteLine($"Trabajo en {Empresa}.");
    }
}
```

```csharp
Persona persona = new Empleado { Nombre = "Luis", Empresa = "Acme" };

persona.MencionarEmpresa(); // error de compilación: Persona no tiene MencionarEmpresa()
```

El objeto al que apunta `persona` es, en tiempo de ejecución, un `Empleado` completo — con su campo `Empresa` y su método `MencionarEmpresa()` intactos, nada de eso desaparece. Pero el compilador no decide qué se puede escribir mirando el objeto real (eso ni lo sabe todavía, solo existe al ejecutar el programa) — lo decide mirando el tipo de la variable, y `Persona` no declara `MencionarEmpresa()`. Da igual que el objeto detrás sí lo tenga: a través de una variable `Persona`, ese miembro no es alcanzable.

Para llegar a él hace falta convertir explícitamente `persona` de vuelta a `Empleado`:

```csharp
Empleado empleado = (Empleado)persona;
empleado.MencionarEmpresa(); // ahora sí
```

Esto se llama **downcasting**, y tiene más matices de los que este ejemplo deja ver (qué pasa si el objeto real no fuera un `Empleado`, por ejemplo) — se explica con detalle en la sección **Upcasting y downcasting**, más abajo en este mismo tema. De momento basta con quedarse con la idea de fondo: **qué objeto es realmente**, en el heap, no cambia nunca por cómo se declare una variable; pero **qué se puede hacer con él a través de esa variable concreta** lo decide el compilador, mirando únicamente su tipo declarado.

### `virtual`/`override` en propiedades

El mismo mecanismo aplica a propiedades, no solo a métodos — tiene sentido, dado que una propiedad no es más que un `get`/`set` con sintaxis particular (tema de Clases):

```csharp
public class Figura
{
    public virtual double Area => 0;
}

public class Circulo : Figura
{
    public double Radio { get; set; }

    public override double Area => Math.PI * Radio * Radio;
}
```

```csharp
Figura figura = new Circulo { Radio = 2 };
Console.WriteLine(figura.Area); // usa el override de Circulo, aunque la variable sea de tipo Figura
```

## `abstract`: clases que no se pueden instanciar

El ejemplo de `Figura` de arriba deja ver un problema: `Figura.Area => 0` no representa el área de ninguna figura real — es un valor sin sentido propio, que solo existe para que algo compile. Cuando una clase base solo tiene sentido como plantilla para sus derivadas, y no como objeto por sí misma, se marca como `abstract`:

```csharp
public abstract class Figura
{
    public abstract double Area { get; }
}

public class Circulo : Figura
{
    public double Radio { get; set; }

    public override double Area => Math.PI * Radio * Radio;
}
```

```csharp
Figura figura = new Figura();  // error de compilación: no se puede instanciar una clase abstracta
Figura circulo = new Circulo(); // esto sí, Circulo no es abstracta
```

Un miembro `abstract` no tiene cuerpo — no implementa nada, solo declara que toda clase derivada no abstracta está obligada a proporcionar un `override`. Si `Circulo` no implementara `Area`, no compilaría. Una clase con al menos un miembro `abstract` debe ser ella misma `abstract`; no puede tener miembros sin implementación y a la vez pretender ser instanciable directamente.

Una clase `abstract` sí puede tener miembros normales (no abstractos), con implementación completa, junto a los abstractos — no todo tiene que quedar pendiente de la derivada.

## `base.Metodo()`: extender en vez de reemplazar

Un `override` no está obligado a descartar por completo la implementación de la base — puede invocarla explícitamente con `base.Metodo()` y añadir algo más alrededor:

```csharp
public class Persona
{
    public virtual void Saludar()
    {
        Console.WriteLine("Hola.");
    }
}

public class Empleado : Persona
{
    public override void Saludar()
    {
        base.Saludar(); // ejecuta la versión de Persona primero
        Console.WriteLine("Trabajo aquí.");
    }
}
```

```csharp
new Empleado().Saludar();
// Hola.
// Trabajo aquí.
```

Esto es distinto de no hacer `override` en absoluto: aquí sí se reemplaza el método, pero la nueva implementación decide conservar y reutilizar la lógica original como parte de la suya, en vez de duplicarla escribiéndola de nuevo.

## `new` como ocultación de miembro: una trampa a evitar

Es posible declarar en una derivada un miembro con el mismo nombre que uno de la base **sin** `virtual`/`override`, usando `new`:

```csharp
public class Persona
{
    public void Saludar()
    {
        Console.WriteLine("Hola, soy una persona.");
    }
}

public class Empleado : Persona
{
    public new void Saludar()
    {
        Console.WriteLine("Hola, soy un empleado.");
    }
}
```

```csharp
Persona persona = new Empleado();
persona.Saludar(); // "Hola, soy una persona." — usa la versión de Persona, NO la de Empleado
```

A diferencia de `override`, aquí no hay polimorfismo: qué versión se ejecuta depende del tipo de la **variable**, no del tipo real del objeto — justo lo contrario de lo que se vio con `virtual`/`override`. `new` no reemplaza el método heredado, simplemente oculta su nombre cuando se accede a través del tipo derivado. Es un error común, sobre todo si se está acostumbrado a un lenguaje donde este matiz no existe: si la intención es polimorfismo, la combinación correcta es siempre `virtual` en la base y `override` en la derivada, nunca `new`.

## `sealed`: cerrar la cadena

`sealed` en una clase impide que se siga heredando de ella:

```csharp
public sealed class Gerente : Empleado
{
    // ninguna clase puede heredar de Gerente
}
```

También se puede aplicar a un método `override` concreto, para impedir que una subclase posterior lo vuelva a sobreescribir:

```csharp
public override sealed void Saludar()
{
    // ninguna clase derivada de esta puede volver a hacer override de Saludar
}
```

Se usa cuando se quiere garantizar que un tipo o un comportamiento concreto queda fijado, sin posibilidad de modificarse más abajo en la cadena.

## `object`: la base de todo

Toda clase en C#, aunque no lo declare explícitamente, hereda de `object` — es la raíz de la que parte cualquier jerarquía. Esto explica algo que ya se ha visto sin mencionarlo: `Console.WriteLine(objeto)` siempre puede imprimir algo, aunque sea una clase propia sin ningún código especial, porque `object` ya define un método `ToString()` que toda clase hereda.

```csharp
public class Persona
{
    public string Nombre { get; set; }
}

Persona persona = new Persona { Nombre = "Ana" };
Console.WriteLine(persona); // "Persona" — el nombre completo del tipo, no muy útil
```

La implementación por defecto de `ToString()` no es demasiado informativa — solo da el nombre del tipo. Como `ToString()` es `virtual` en `object`, se puede hacer `override` para dar una representación más útil:

```csharp
public class Persona
{
    public string Nombre { get; set; }

    public override string ToString()
    {
        return $"Persona: {Nombre}";
    }
}

Console.WriteLine(persona); // "Persona: Ana"
```

`object` también define `Equals(object)` y `GetHashCode()`, con el mismo patrón (`virtual` en la base, se puede hacer `override`). Su comportamiento por defecto compara identidad (si son el mismo objeto en memoria, no si tienen el mismo contenido) — igual que se vio con structs y `==` en el tema anterior. Hacer un `override` correcto de ambos exige mantenerlos consistentes entre sí, con reglas propias que no se profundizan todavía; se retoma en el tema de Records, donde se generan automáticamente y ese contexto aclara mejor por qué escribirlos a mano tiene matices delicados.

## Upcasting y downcasting

Antes quedó pendiente el detalle de por qué `(Empleado)persona` no siempre es seguro, con el ejemplo de `MencionarEmpresa()`. Retomando ese hilo: la conversión de `Empleado` (derivada) hacia `Persona` (base) es automática y siempre segura — es justo lo que ya se hizo sin nombrarlo, al escribir `Persona persona = new Empleado {...}`. Esto se llama **upcasting**:

```csharp
Empleado empleado = new Empleado();
Persona persona = empleado; // upcasting implícito, siempre seguro
```

Es seguro porque un `Empleado` **es** una `Persona` — nunca puede fallar en tiempo de ejecución.

Para volver a alcanzar `MencionarEmpresa()` hace falta el camino inverso, llamado **downcasting**: un cast explícito que le dice al compilador "trata esto como lo que realmente es".

```csharp
Empleado empleado = (Empleado)persona;
empleado.MencionarEmpresa(); // ahora sí — empleado está declarada como Empleado
```

A diferencia del upcasting, el downcasting no es automático ni está garantizado — puede fallar en tiempo de ejecución si el objeto real no es del tipo al que se intenta convertir:

```csharp
Persona persona = new Persona(); // objeto real: Persona, no Empleado
Empleado empleado = (Empleado)persona; // compila, pero lanza InvalidCastException en runtime
```

El compilador no puede saber, solo mirando el tipo de la variable (`Persona`), si el objeto al que apunta en tiempo de ejecución es realmente un `Empleado` — eso solo se sabe al ejecutar. De ahí surge la necesidad de comprobar el tipo real antes de castear, algo que hasta aquí solo se puede hacer con `is` en su forma más básica (tema de Pattern Matching I) combinado con un cast:

```csharp
if (persona is Empleado)
{
    Empleado emp = (Empleado)persona;
    // ...
}
```

Esto funciona, pero es repetitivo — comprobar el tipo y luego castear por separado. Pattern Matching II, el tema siguiente, resuelve exactamente esta repetición con patrones de tipo (`is Empleado emp`, comprobación y cast en un solo paso) y patrones de propiedad, que solo tienen sentido real ahora que existe una jerarquía de clases sobre la que aplicarlos.