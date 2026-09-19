# 15 - Herencia y polimorfismo

## Qué es la herencia

Ya se ha trabajado con clases que no tienen ninguna relación entre sí (`Persona`, `CuentaBancaria`, `Contador`...). En muchos dominios, sin embargo, existen varios tipos que comparten datos y comportamientos, aunque cada uno tenga características propias.

Por ejemplo, un `Ave` es un `Animal`. Ambos pueden compartir datos como `Peso` y `Edad`, además de comportamientos como `Comer()`. Sin embargo, un `Ave` puede tener características específicas que no tienen todos los animales, como `Raza` o el comportamiento `Volar()`.

Sin herencia, habría que repetir `Peso`, `Edad` y `Comer()` en cada clase que represente un animal.

En resumen, la **herencia** permite que una clase (la **derivada**) herede los miembros de otra (la **base**) y añada sus propias características y comportamientos.

Con la intención de explicar este tema, expandiremos el ejemplo de `Ave` y `Animal` mientras se explican distintos conceptos:

```csharp
//Partimos de estas 2 clases, actualmente no hay relación entre ellas.
public class Animal {}

public class Ave {}
```

## Herencia simple

La sintaxis para heredar una clase de otra es simple:

```csharp
public class Animal {}

//Ave heredará los atributos y métodos de la clase Animal
public class Ave : Animal
{

}
```

La sintaxis `class Derivada : Base` indica que `Derivada` **hereda de** `Base`. Es decir, lo que aparece después de `:` es la **clase base**:

```csharp
public class Ave : Animal {}
```

En este caso, `Ave` es la clase **derivada** y `Animal` es la clase **base**.

A partir de este momento, `Ave` es un tipo de `Animal`: una instancia de `Ave` también puede utilizarse allí donde se espere un `Animal`. Profundizaremos sobre esta idea en breve cuando se hable del **polimorfismo**.

C# solo permite que una clase tenga **una única clase base directa**:

```csharp
public class Ave : Animal, SerVivo {} // Error de compilación
```

Por tanto, una clase no puede heredar directamente de dos clases diferentes, cosa que sí se permite en otros lenguajes. Si un desarrollador desea que una clase cumpla varios contratos, C# permite hacerlo mediante el uso de **interfaces**, las cuales se explicarán en detalle en el tema de Interfaces.

## Constructores en la jerarquía

Vamos a partir de una clase `Animal`, con los siguientes datos:

```csharp
public class Animal
{
    //propiedades de Animal
    public double Peso { get; set; }
    public int Edad { get; set; }

    //constructor de Animal
    public Animal(double peso, int edad)
    {
        Peso = peso;
        Edad = edad;
    }

    //métodos de Animal
    public void Comer()
    {
        Console.WriteLine($"Estoy comiendo. Ahora peso {Peso} Kg");
    }
}
```

Un constructor de la clase **derivada** no inicializa automáticamente los datos que pertenecen a la clase **base** — tiene que delegar explícitamente en un constructor de la base, con `base(...)`:

```csharp
public class Ave : Animal
{
    //propiedades de Ave
    public string Raza { get; set; }

    //constructor de Ave, :base requerido!!!!
    public Ave(double peso, int edad, string raza) : base(peso, edad)
    {
        Raza = raza;
    }

    //métodos de Ave
    public void Volar()
    {
        Console.WriteLine($"Estoy volando!!!");
    }
}
```

Y la manera de instanciar a un Ave sería la siguiente:

```csharp
Ave miPajarito = new Ave(0.22, 2, "Golondrina");
```

`: base(peso, edad)` llama al constructor `Animal(double, int)` antes de ejecutar el cuerpo del constructor de `Ave` — la clase base siempre termina de construirse primero, y solo entonces se ejecuta lo propio de la derivada. Esto tiene sentido porque la derivada puede necesitar que los datos de la base ya existan antes de añadir los suyos.

Si `Animal` no tuviera ningún constructor propio, `base()` sería opcional (C# lo asume implícitamente, llamando al constructor vacío por defecto). Pero en cuanto `Animal` define un constructor con parámetros, su constructor vacío por defecto deja de existir. Eso significa que `Ave` está obligado a llamar explícitamente a `base(peso, edad)`, o a algún otro constructor de `Animal` que exista; si no lo hace, no compila.

### Herencia multinivel

La cadena de herencia no se limita a un único nivel — una clase derivada puede a su vez ser la base de otra:

```csharp
public class Golondrina : Ave
{
    public int KilometrosVolados { get; set; }

    public Golondrina(double peso, int edad, string raza, int kilometrosVolados)
        : base(peso, edad, raza)
    {
        KilometrosVolados = kilometrosVolados;
    }
}
```

`base(...)` aquí llama al constructor de `Ave`, que a su vez llama al de `Animal` — la cadena se resuelve nivel a nivel, de la derivada más lejana hacia la base más raíz, cada una terminando de construirse antes de pasar a la siguiente. Los cuerpos de los constructores, sin embargo, se ejecutan en el orden contrario: primero el de `Animal`, luego el de `Ave`, y por último el de `Golondrina` — cada nivel confía en que todo lo que hereda ya está inicializado antes de añadir lo suyo.

## Modificadores de acceso: `protected` e `internal`

En el tema de Clases, se vieron los modificadores de acceso `public` y `private`. Ha de tenerse en cuenta que estos modificadores pueden aplicarse a cualquier tipo de miembro, tanto a **métodos** como a **campos** o **propiedades** (estos 2 son ligeramente distintos, leer tema de clases si cuesta distinguirlos).

`public` → El miembro es accesible desde cualquier lugar, **incluso fuera del proyecto**.

`private` → El miembro solo es accesible desde dentro de la clase que lo implementa.

```csharp
public class Animal
{
    public double Peso { get; protected set; } // cualquiera puede leerlo, solo Animal y sus derivadas pueden modificarlo

    //Hagamos a Edad privada temporalmente para explicar estos conceptos
    private int Edad; // solo es modificable desde la clase Animal
}

public class Ave : Animal
{
    public void ModificarParametros(double nuevoPeso)
    {
        Peso = nuevoPeso; // funciona: el set es protected, y Ave hereda de Animal
        Edad = 30; // error de compilación: Edad es private en Animal
    }
}
```

Un modificador delante de `get` o `set` restringe solo ese acceso concreto — el resto de la propiedad conserva la visibilidad que tenga declarada. Esto es una extensión directa de lo ya visto en Clases (`private set` para impedir cualquier asignación externa); aquí simplemente se cambia `private` por `protected`, permitiendo que las derivadas sí puedan asignar, aunque el resto del mundo no.

```csharp
Ave miPajarito = new Ave();
Console.WriteLine(miPajarito.Peso); // funciona: el get es público
miPajarito.Peso = 2.5; // error de compilación: el set es protected
```

Los nuevos modificadores son:

`protected` → El miembro es accesible únicamente desde la clase que lo implementa y cualquiera de sus derivadas.

`internal` → El miembro es accesible desde cualquier sección del proyecto, pero no desde otro proyecto que lo referencie como dependencia.

```csharp
internal class Configuracion
{
    // visible en todo este proyecto, invisible desde fuera de él
}
```

En este momento del curso, las diferencias entre `internal` y `public` no son visibles, pero se volverá relevante en cuanto se llegue a trabajar con más de un proyecto (por ejemplo, una librería separada del programa que la consume), algo que se retoma en Proyectos .NET II. Existen además combinaciones de estos cuatro modificadores (`protected internal`, `private protected`), que se dejan fuera por la misma razón: su utilidad solo se aprecia con varios proyectos de por medio.

Resumen de lo cubierto hasta ahora:

| Modificador | Visible desde |
|---|---|
| `public` | Cualquier código, de cualquier proyecto |
| `internal` | Cualquier código, dentro del mismo proyecto |
| `protected` | La propia clase y sus derivadas, en cualquier proyecto |
| `private` | Solo la propia clase |

## Sobrescritura (*overriding*) de métodos

Heredar un método tal cual, como `Comer()` en el primer ejemplo, es útil pero limitado: ¿qué pasa si `Animal` necesita expresar qué tipo de alimento concreto come? Se podría declarar un método nuevo con otro nombre, pero eso rompe la idea de que ambos tipos "saben comer" de forma intercambiable. La solución para este problema se conoce como **sobrescritura**; la cual permite que la clase derivada **reemplace** la implementación heredada, conservando el mismo nombre:

Para ello C# hace uso de la palabra reservada `virtual`, la cual marca el método como "reemplazable" en la clase base. Y la palabra reservada  `override` en la derivada, lo que indica explícitamente que se está reemplazando la implementación heredada, no declarando un método nuevo sin relación.

```csharp
//Para explicar mejor el ejemplo, se han eliminado constructores y algunos parametros para que el código sea más simple de comprender.
public class Animal
{
    public double Peso { get; set; }

    public virtual void Comer()
    {
        Console.WriteLine($"Estoy comiendo. Ahora peso {Peso} Kg");
    }
}

public class Ave : Animal
{
    public override void Comer()
    {
        Console.WriteLine($"Estoy comiendo semillas y pequeños insectos. Ahora peso {Peso} Kg");
    }
}
```

Esto logra el comportamiento siguiente:

```csharp
Animal criatura1 = new Animal { Peso = 2.0 };
Ave criatura2 = new Ave { Peso = 3.0 };

criatura1.Comer(); // "Estoy comiendo. Ahora peso 2 Kg"
criatura2.Comer(); // "Estoy comiendo semillas y pequeños insectos. Ahora peso 3 Kg"
```

## Sobrescritura (*overriding*) de propiedades

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

El resultado:

```csharp
Figura figura1 = new Figura();
Circulo figura2 = new Circulo { Radio = 2 };

Console.WriteLine(figura1.Area); // "0"
Console.WriteLine(figura2.Area); // "12,566370614359172"
```

## Polimorfismo

En las secciones anteriores se ha usado la palabra sin definirla del todo. **Polimorfismo** (del griego, "muchas formas") es la capacidad de tratar objetos de distintos tipos de forma uniforme, a través de un tipo común, dejando que sea cada objeto quien decida cómo responder.

Imagina una granja con `Animal`, `Ave` y `Golondrina` — pero también, en el futuro, podría haber un `Mamifero` o un `Pez`. Cada uno come de forma distinta: un `Ave` come semillas e insectos, un `Pez` podría comer algas, un `Mamifero` podría pastar. Sin polimorfismo, cualquier código que quisiera "dar de comer a todos los animales de la granja" tendría que conocer de antemano cada tipo concreto que existe, y decidir a mano qué hacer con cada uno — algo como:

```csharp
foreach (object animal in listaDeAnimales)
{
    if (animal is Ave ave) ave.Comer();
    else if (animal is Pez pez) pez.Comer();
    else if (animal is Mamifero mamifero) mamifero.Comer();
    // ...y así por cada tipo nuevo que se añada en el futuro
}
```

Este código se rompe (o, más bien, se queda incompleto) cada vez que aparece un tipo nuevo de animal en la granja. Gracias al polimorfismo, con `Comer()` marcado `virtual` en `Animal` y con `override` en cada derivada, ese mismo recorrido se reduce a esto:

```csharp
foreach (Animal animal in listaDeAnimales)
{
    animal.Comer(); // cada uno ejecuta su propia versión, sin que este código sepa cuál es cuál
}
```

Este código funciona igual de bien hoy, con `Animal`, `Ave` y `Golondrina`, que el día de mañana si se añade `Mamifero` o `Pez` — sin tocar ni una línea de este `foreach`. Cada objeto "sabe" cómo comer a su manera, y el código que los recorre no necesita enterarse de los detalles: solo necesita saber que, sea lo que sea, es un `Animal`, y por tanto puede comer.

Esta es la promesa central del polimorfismo: escribir código contra el tipo base, y que siga funcionando correctamente sin cambios a medida que aparecen nuevas clases derivadas.














//SOBRA
La parte que hace esto polimorfismo de verdad: **la versión que se ejecuta depende del tipo real del objeto, no del tipo de la variable usada para llamarlo**.

```csharp
Persona persona1 = new Persona { Nombre = "Ana" };
Persona persona2 = new Empleado { Nombre = "Luis" }; // variable de tipo Persona, objeto real de tipo Empleado

persona1.Saludar(); // "Hola, soy Ana."
persona2.Saludar(); // "Hola, soy Luis y trabajo aquí." — se ejecuta el override, no la versión de Persona
```

Aunque `persona2` está declarada como `Persona`, el objeto al que apunta es realmente un `Empleado` — y es ese tipo real, no el de la variable, el que decide qué versión de `Saludar()` se ejecuta. Esto es lo que permite tratar una colección de `Persona` (una vez se vea `List<T>`, en el tema de colecciones) que en realidad contenga una mezcla de `Persona` y `Empleado`, y que cada uno salude a su manera sin que el código que los recorre necesite saber de qué tipo concreto es cada uno.
//END SOBRA







//Esto no tiene sentido sin explicar polimorfismo
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
