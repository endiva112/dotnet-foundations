


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

