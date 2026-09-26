# 18 - Heap(Montículo) vs Stack(Pila)

Este es un tema de refuerzo: no introduce ninguna palabra clave nueva de C#. Reúne y explica a fondo algo que se ha usado constantemente desde el principio del curso (tipos por valor, tipos por referencia, `class`, `struct`, arrays) pero que nunca se había desarrollado con el detalle suficiente como para sentirse seguro explicándolo. El objetivo no es memoria como teoría abstracta — es entender por qué el código que ya se ha escrito se comporta como se comporta.

## Qué son, físicamente

Un programa en ejecución tiene, entre otras cosas, dos regiones de memoria con reglas de funcionamiento completamente distintas.

**La pila (stack)** es una región de memoria muy simple: solo se puede añadir o quitar por un extremo, como una pila de platos. Lo último que se apila es siempre lo primero en retirarse (*LIFO*, *last in, first out*). No hace falta decidir dónde encaja cada dato nuevo — siempre va justo encima del último. Por esta misma razón es extremadamente rápida: reservar espacio es simplemente mover un puntero hacia arriba, y liberarlo es moverlo hacia abajo.

**El heap** es una región de memoria mucho más flexible, y también más desordenada: un dato puede crearse en cualquier momento y liberarse en cualquier otro, sin ningún orden que respetar. Encontrar espacio libre exige más trabajo que en la pila, y por eso es algo más lento. Nada libera el heap automáticamente por sí solo al terminar un bloque de código — de eso se encarga el **recolector de basura** (*Garbage Collector*, o GC), que se explica más abajo.

```
PILA                         HEAP
┌─────────────┐              ┌──────────────────────┐
│ x = 5        │              │  objeto Persona #1   │
├─────────────┤              │  Nombre: "Ana"        │
│ a = 10       │              │  Edad: 30              │
├─────────────┤              ├──────────────────────┤
│ referencia ──┼─────────────▶│  objeto Persona #2   │
└─────────────┘              │  Nombre: "Luis"        │
  crece hacia arriba          │  Edad: 25               │
  con cada método             └──────────────────────┘
                               sin orden fijo
```

## Qué pasa al llamar a un método

Cada vez que se llama a un método, se reserva en la pila un bloque nuevo llamado **marco de pila** (*stack frame*), con sus parámetros y sus variables locales. Al terminar el método, ese marco se retira automáticamente — sin que el programador tenga que hacer nada — y todo lo que había en él deja de existir.

```csharp
static void Main()
{
    int x = 5;
    Metodo1(x);
}

static void Metodo1(int a)
{
    int b = a * 2;
    Metodo2(b);
}

static void Metodo2(int c)
{
    int d = c + 1;
    Console.WriteLine(d);
}
```

Mientras se ejecuta `Metodo2`, la pila tiene tres marcos apilados a la vez, uno encima de otro:

```
┌───────────────────┐  ← el último en apilarse
│ Metodo2: c, d      │
├───────────────────┤
│ Metodo1: a, b      │
├───────────────────┤
│ Main: x            │  ← el primero en apilarse
└───────────────────┘
```

Al terminar `Metodo2`, su marco se retira, y el control vuelve a `Metodo1` justo donde se quedó, con `a` y `b` todavía intactos. Al terminar `Metodo1`, pasa lo mismo con el marco de `Main`. Esto explica algo que ya se daba por hecho desde los primeros temas: por qué una variable local "desaparece" en cuanto el método termina, y por qué un método puede llamarse a sí mismo (recursión, tema de Métodos II) sin que sus variables de distintas llamadas se pisen entre sí — cada llamada tiene su propio marco, con su propia copia de las variables, apilado encima del anterior.

## Dónde vive cada cosa

Con esto ya se puede repasar, de forma unificada, todo lo que se ha ido diciendo por separado en varios temas.

### Un tipo por valor en una variable local

```csharp
int numero = 5;
Punto punto = new Punto(1, 1); // Punto es un struct, tema de Structs
```

Ambos viven enteros en la pila, dentro del marco del método donde se declararon. No hay ninguna referencia de por medio — la variable *es* el dato.

### Un tipo por referencia

```csharp
Persona persona = new Persona("Ana", 30); // Persona es una class
```

Aquí pasan dos cosas en dos sitios distintos: el objeto en sí (con `Nombre` y `Edad`) se crea en el heap; y en la pila solo se guarda una referencia — una dirección que apunta a ese objeto. Esto es lo que ya se explicó en Herencia con la analogía de la "nota con una dirección escrita" frente a "la taquilla en sí".

### Un struct como campo de una class

Este caso no se había explicado todavía en ningún tema anterior, y tiene una consecuencia real:

```csharp
public class Rectangulo
{
    public Punto Origen { get; set; } // Punto es un struct
}

Rectangulo rectangulo = new Rectangulo();
```

`Origen` no vive "aparte", en su propio sitio de la pila — vive **dentro** del objeto `Rectangulo`, en el heap, como parte del mismo bloque de memoria. Un struct no tiene una regla fija de "vive en la pila"; vive allí donde vive la variable que lo contiene. Si esa variable es un campo de un objeto en el heap, el struct completo va empotrado ahí, junto con el resto de campos del objeto.

```
HEAP
┌───────────────────────────┐
│ objeto Rectangulo          │
│  ┌─────────────────────┐  │
│  │ Origen (Punto)        │  │
│  │  X: 1                  │  │
│  │  Y: 1                  │  │
│  └─────────────────────┘  │
└───────────────────────────┘
```

### Un array

```csharp
int[] numeros = { 1, 2, 3 };
Persona[] personas = { new Persona("Ana", 30), new Persona("Luis", 25) };
```

El array en sí siempre es un objeto en el heap (tema de Arrays: "el array en sí es un tipo por referencia"), sin importar qué contenga. Lo que cambia es qué hay dentro de ese bloque:

```
int[] numeros                       Persona[] personas
┌─────────────────────┐            ┌─────────────────────────┐
│ 1 │ 2 │ 3            │            │ ref │ ref                │
└─────────────────────┘            └──┬────┬─────────────────┘
 valores completos,                    │    │
 uno junto al otro                     ▼    ▼
                                   Persona#1  Persona#2
                                   (en otro sitio del heap)
```

Un array de `int` (o de cualquier struct) guarda los valores completos, uno junto a otro, dentro del mismo bloque del heap — sin ninguna referencia intermedia. Un array de `Persona` (o de cualquier class) guarda una lista de referencias; cada objeto real vive en su propio sitio, en cualquier parte del heap, sin ninguna garantía de que estén unos junto a otros.

## El Garbage Collector, en la práctica

Nada de lo anterior explica cuándo se libera un objeto del heap. En muchos lenguajes (C, por ejemplo) esto se hace a mano, y olvidarlo es una fuente constante de errores. En C#, de eso se encarga el **Garbage Collector**: de vez en cuando, revisa qué objetos del heap ya no tienen ninguna referencia apuntándolos desde ningún sitio del programa, y libera ese espacio automáticamente.

Para escribir código normal, esto tiene consecuencias prácticas concretas:

- Nunca hay que liberar un objeto a mano — no existe un `free()` o un `delete` que llamar.
- No hay un momento exacto y predecible en que un objeto deja de existir — puede seguir ocupando espacio en el heap durante un tiempo después de que el programa ya no lo necesite, hasta que el GC pase por ahí.
- Crear un objeto en el heap no es gratis, aunque el recolector se encargue después de limpiarlo — sigue teniendo un coste real (reservar espacio, y más adelante liberarlo). Es más lento que trabajar con datos en la pila, aunque para la inmensa mayoría del código esta diferencia es completamente insignificante y no debería influir en cómo se escribe un programa.

## Boxing y unboxing

Con esto ya se puede explicar un caso concreto donde la diferencia entre pila y heap sí llega a notarse: qué ocurre cuando un tipo por valor se trata como si fuera un tipo por referencia.

```csharp
int numero = 5;      // vive en la pila
object cajado = numero; // boxing: se crea una copia de numero en el heap

int recuperado = (int)cajado; // unboxing: se copia el valor de vuelta a la pila
```

`object` es un tipo por referencia (toda clase, incluyendo `object`, vive en el heap — tema de Herencia). Como `int` no encaja directamente ahí, el compilador hace **boxing**: crea una copia completa del valor dentro de un objeto nuevo en el heap, y `cajado` termina siendo una referencia a esa copia — no al `numero` original de la pila. Ambas variables, a partir de ese momento, son completamente independientes. **Unboxing** es el camino inverso: copiar el valor de vuelta desde el heap a una variable de la pila.

Esto no es un caso de laboratorio — pasa en situaciones perfectamente normales, sin que sea evidente a simple vista:

```csharp
object[] valores = new object[1000];

for (int i = 0; i < valores.Length; i++)
{
    valores[i] = i; // boxing en cada vuelta: 1000 copias nuevas en el heap
}
```

Cada asignación de este bucle crea un objeto nuevo en el heap, aunque el código no lo mencione en ningún sitio de forma explícita. Comparado con `int[] valores = new int[1000];` (donde los 1000 enteros viven, completos, dentro de un único bloque del array, sin ningún boxing), la diferencia de coste es real y crece con cada elemento.

## Por qué importa para el rendimiento

Dos casos concretos, aparte del boxing, donde la distinción entre pila y heap deja de ser solo teoría:

**Copiar structs grandes muchas veces.** Un struct se copia entero cada vez que se asigna o se pasa a un método (tema de Structs). Si el struct es pequeño (dos o tres campos, como `Punto`), esa copia es prácticamente gratis. Si un struct tuviera muchos campos y se pasara así, sin `ref`, dentro de un bucle con miles de vueltas, el coste de copiarlo entero una y otra vez sí llega a notarse — frente a una clase, donde lo que se copia siempre es solo la referencia (unos pocos bytes), sin importar lo grande que sea el objeto real.

**Boxing repetido**, como el bucle de arriba — cientos o miles de asignaciones al heap donde, con el tipo adecuado, no haría falta ninguna.

Ambos casos son la razón real detrás de una regla que se verá con más peso en el tema de Colecciones: `List<int>` y `List<Persona>` no se comportan igual por dentro. Una `List<int>` guarda los enteros completos, uno junto a otro, igual que un array de `int`; una `List<Persona>` guarda referencias, igual que un array de `Persona`. La elección entre struct y class (tema de Structs) no es solo una cuestión de sintaxis — tiene consecuencias directas en cómo se organiza la memoria del programa, y por tanto en su rendimiento.