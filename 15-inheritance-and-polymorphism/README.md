


### Tipo de la variable frente a tipo real del objeto

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










## `new` como ocultación de miembro

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






## `sealed`

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





## La clase `object`

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