# 12 - Clases

## Qué es una clase

Hasta ahora, los datos y el comportamiento han vivido separados: un array de `int` por un lado, un método que opera sobre él por otro, sin ninguna relación explícita entre ambos más que pasarlos como parámetro. Una **clase** permite agrupar datos y el comportamiento que actúa sobre ellos en una sola unidad — una plantilla a partir de la cual se crean **objetos**.

```csharp
public class Persona
{
    public string Nombre;
    public int Edad;

    public void Saludar()
    {
        Console.WriteLine($"Hola, soy {Nombre} y tengo {Edad} años.");
    }
}
```

Esta clase no es un dato en sí — es la plantilla. Para tener datos reales hace falta crear una **instancia**:

```csharp
Persona persona = new Persona();
persona.Nombre = "Ana";
persona.Edad = 30;
persona.Saludar(); // "Hola, soy Ana y tengo 30 años."
```

`new Persona()` crea un objeto nuevo en el heap (tema 01 — igual que un array, una clase es un tipo por referencia) y devuelve una referencia a él. Se pueden crear tantas instancias como haga falta, cada una con sus propios valores:

```csharp
Persona otraPersona = new Persona();
otraPersona.Nombre = "Luis";
otraPersona.Edad = 25;

persona.Saludar();      // Ana
otraPersona.Saludar();  // Luis
```

Siguiendo el tema de organización de proyectos (tema 09), cada clase se declara habitualmente en su propio archivo (`Persona.cs`).

## Constructores

Asignar cada campo a mano después de crear el objeto, como en el ejemplo de arriba, es tedioso y permite dejar el objeto a medio construir (¿qué pasa si alguien olvida asignar `Edad`?). Un **constructor** resuelve esto: es un método especial que se ejecuta automáticamente al crear la instancia, y que obliga a proporcionar los datos necesarios desde el principio:

```csharp
public class Persona
{
    public string Nombre;
    public int Edad;

    public Persona(string nombre, int edad)
    {
        Nombre = nombre;
        Edad = edad;
    }

    public void Saludar()
    {
        Console.WriteLine($"Hola, soy {Nombre} y tengo {Edad} años.");
    }
}
```

```csharp
Persona persona = new Persona("Ana", 30);
```

Un constructor tiene el mismo nombre que la clase, no tiene tipo de retorno (ni siquiera `void`), y se llama automáticamente al escribir `new Persona(...)`. Sin ningún constructor propio, C# proporciona uno vacío por defecto (el que se usó en el primer ejemplo, `new Persona()`); en cuanto se define un constructor propio, ese constructor vacío por defecto deja de existir — si también hiciera falta, habría que escribirlo explícitamente.

## `this`

Dentro de un constructor o de un método de la clase, `this` se refiere a la instancia actual. No siempre hace falta (en el ejemplo de arriba, `Nombre = nombre;` ya sabe que `Nombre` es el campo de la clase), pero es imprescindible cuando el parámetro tiene el mismo nombre que el campo:

```csharp
public Persona(string nombre, int edad)
{
    this.Nombre = nombre; // this.Nombre es el campo; nombre (parámetro) es lo que llegó
    this.Edad = edad;
}
```

Sin `this`, `Nombre = nombre;` seguiría funcionando igual mientras el campo y el parámetro tengan nombres distintos (como en el primer ejemplo del constructor) — pero si ambos se llamaran igual (`string Nombre; ... (string Nombre)`), sin `this` no habría forma de distinguir cuál es cuál.

## Propiedades

Declarar los campos como `public string Nombre;` directamente, tal como se hizo arriba, permite que cualquier código externo asigne cualquier valor sin ningún control:

```csharp
persona.Edad = -50; // compila perfectamente, aunque no tenga sentido
```

Una **propiedad** envuelve el acceso a un valor con un `get` (cómo se lee) y un `set` (cómo se escribe), permitiendo controlar ese acceso sin cambiar cómo se usa desde fuera:

```csharp
public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; set; }
}
```

Esto se llama **auto-propiedad** (auto-property): el compilador genera por debajo un campo oculto y el `get`/`set` más simple posible, así que a efectos de uso se comporta igual que el campo público de antes (`persona.Nombre = "Ana";`, `persona.Nombre` para leerlo). La diferencia es que una propiedad puede restringir ese acceso más adelante sin romper nada de lo que ya la usa:

```csharp
public class Persona
{
    public string Nombre { get; set; }
    public int Edad { get; private set; } // solo se puede modificar desde dentro de la clase
}
```

Con `private set`, cualquier código fuera de la clase puede leer `persona.Edad`, pero no asignarlo directamente — solo la propia clase puede cambiarlo (por ejemplo, desde un método `Cumpleanos()` que incremente la edad de forma controlada). Esta es la razón por la que, en C#, se prefiere declarar propiedades en vez de campos públicos sueltos: empezar con una propiedad simple deja la puerta abierta a añadir control más adelante sin tener que cambiar el resto del código que ya la usa.

## Miembros estáticos frente a miembros de instancia

Ya se usó `static` en métodos sueltos (tema 06) y en toda la BCL (`Math.Abs`, tema 08). Dentro de una clase, la distinción se vuelve más visible: un miembro **static** pertenece a la clase en sí, uno solo para todas las instancias; un miembro normal (de instancia) pertenece a cada objeto por separado, con su propia copia:

```csharp
public class Contador
{
    public static int TotalInstancias = 0;
    public int Id;

    public Contador()
    {
        TotalInstancias++;
        Id = TotalInstancias;
    }
}
```

```csharp
Contador c1 = new Contador();
Contador c2 = new Contador();

Console.WriteLine(c1.Id);              // 1
Console.WriteLine(c2.Id);              // 2
Console.WriteLine(Contador.TotalInstancias); // 2 — compartido, no por instancia
```

`Id` es distinto en cada objeto; `TotalInstancias` es el mismo valor compartido por todos, y se accede a través del nombre de la clase (`Contador.TotalInstancias`), no de una instancia concreta.

## Modificadores de acceso

`public` (usado en todos los ejemplos anteriores) permite que cualquier código externo acceda al miembro. `private` lo restringe al interior de la propia clase:

```csharp
public class CuentaBancaria
{
    private double saldo;

    public CuentaBancaria(double saldoInicial)
    {
        saldo = saldoInicial;
    }

    public void Depositar(double cantidad)
    {
        saldo += cantidad;
    }

    public double ConsultarSaldo()
    {
        return saldo;
    }
}
```

```csharp
CuentaBancaria cuenta = new CuentaBancaria(100);
cuenta.saldo = 1000000; // error de compilación: saldo es private
cuenta.Depositar(50);   // así sí, a través del método público
```

Este patrón —campo `private`, acceso controlado a través de métodos o propiedades `public`— es la base de la **encapsulación**: los detalles internos de cómo se guarda o valida un dato quedan ocultos, y solo se expone la forma controlada de interactuar con él. Existen más modificadores (`protected`, `internal`) que se retoman cuando tengan sentido real, en el tema de herencia.

## Por qué esto habilita la sobrecarga

Recordando el tema 06: en top-level statements, dos métodos con el mismo nombre chocan, porque son funciones locales y viven en el ámbito de una función, no de un tipo — la sobrecarga en C# solo se resuelve dentro del ámbito de un tipo. Una clase es exactamente ese "ámbito de tipo" que faltaba. A partir de aquí, dos métodos de una misma clase con el mismo nombre y distintos parámetros ya no chocan — eso es precisamente lo que se explora en el tema siguiente, Métodos II.

## Convención de nombres

Clases en PascalCase (`Persona`, `CuentaBancaria`), igual que cualquier otro tipo. Propiedades en PascalCase (`Nombre`, `Edad`) — a diferencia de las variables locales, que van en camelCase. Campos `private`, cuando hacen falta, suelen ir en camelCase (`saldo`, `nombre`); es habitual también verlos con un guion bajo delante (`_saldo`) en bases de código de otros equipos, para diferenciarlos a simple vista de las variables locales — este curso no impone esa convención, pero conviene reconocerla si aparece en código ajeno.