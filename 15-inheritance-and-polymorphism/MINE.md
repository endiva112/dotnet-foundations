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
        Console.WriteLine($"Estoy comiendo");
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