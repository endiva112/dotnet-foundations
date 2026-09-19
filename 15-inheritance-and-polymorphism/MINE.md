# 15 - Herencia y polimorfismo

## Qué es la herencia

Ya se ha trabajado con clases que no tienen ninguna relación entre sí (`Persona`, `CuentaBancaria`, `Contador`...). En muchos dominios, sin embargo, existen varios tipos que comparten datos y comportamientos, aunque cada uno tenga características propias.

Por ejemplo, un `Ave` es un `Animal`. Ambos pueden compartir datos como `Peso` y `Edad`, además de comportamientos como `Comer()`. Sin embargo, un `Ave` puede tener características específicas que no tienen todos los animales, como `Raza` o el comportamiento `Volar()`.

Sin herencia, habría que repetir `Peso`, `Edad` y `Comer()` en cada clase que represente un animal. 

En resumen, la **herencia** permite que una clase (la **derivada**) herede los miembros de otra (la **base**) y añada sus propias características y comportamientos.

Con la intención de explicar este tema, expandiremos el ejemplo de `Ave` y `Animal` mientras se explican distintos conceptos

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

`: base(peso, edad)` llama al constructor `Animal(double, int)` antes de ejecutar el cuerpo del constructor de `Ave` — la clase base siempre termina de construirse primero, y solo entonces se ejecuta lo propio de la derivada.

Si `Animal` no tuviera ningún constructor propio, `base()` sería opcional (C# lo asume implícitamente, llamando al constructor vacío por defecto). Pero en cuanto `Animal` define un constructor con parámetros, su constructor vacío por defecto deja de existir. Eso significa que `Ave` está obligado a llamar explícitamente a `base(peso, edad)`, o a algún otro constructor de `Animal` que exista; si no lo hace, no compila.

