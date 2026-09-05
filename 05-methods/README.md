# 05 - Métodos

## Función vs método

En algunos lenguajes (C, Python, JavaScript...) se distingue entre **función** (un trozo de código reutilizable, suelto, que no pertenece a nadie) y **método** (lo mismo, pero perteneciente a una clase u objeto). En C# esa distinción no existe: todo el código ejecutable vive dentro de una clase, así que en C# **todo es un método** — no hay funciones sueltas.

Esto incluye el propio punto de entrada del programa. Cuando escribes:

```csharp
Console.WriteLine("Hola mundo");
```

parece una línea suelta, sin clase ni método a la vista. En realidad el compilador la envuelve automáticamente en un método `Main` dentro de una clase generada — es una comodidad de C# moderno (top-level statements) para no obligarte a escribir esa ceremonia en programas pequeños. Cuando en el tema de clases se vean instancias de verdad, esto quedará más claro.

Un método agrupa un bloque de código con un nombre, para poder reutilizarlo sin copiar y pegar.

## Estructura básica

```csharp
static int Sumar(int a, int b)
{
    return a + b;
}

int resultado = Sumar(3, 4); // 7
```

- `static` por ahora es obligatorio porque todavía no se han visto clases ni instancias — se retoma en el tema de clases, donde se explica qué significa de verdad.
- El tipo antes del nombre (`int` en este caso) es lo que devuelve el método. Si no devuelve nada, se usa `void`.

## Parámetros

```csharp
static void Saludar(string nombre, int edad)
{
    Console.WriteLine($"Hola {nombre}, tienes {edad} años.");
}

Saludar("Ana", 30);
```

Por defecto, los parámetros se pasan **por valor**: el método recibe una copia. Si el parámetro es un tipo por referencia (como una lista), la copia es de la referencia, así que modificar el contenido del objeto sí se nota fuera del método — pero reasignar el parámetro a otro objeto distinto, no.

También se pueden pasar parámetros por nombre, en cualquier orden:

```csharp
Saludar(edad: 30, nombre: "Ana");
```

## Sobrecarga (overloading)

Varios métodos con el mismo nombre, pero distinta lista de parámetros (distinto número o distinto tipo). El compilador decide cuál se llama según los argumentos que le pases.

```csharp
static int Sumar(int a, int b) => a + b;
static double Sumar(double a, double b) => a + b;
static int Sumar(int a, int b, int c) => a + b + c;
```

Esto funciona igual que en Java. La única diferencia real es de sintaxis: `=>` aquí es una forma corta de escribir un método de una sola expresión (*expression-bodied member*), equivalente a poner `{ return ...; }`.

Ojo: este `=>` no es una expresión lambda, aunque el símbolo sea idéntico y ambos "acorten" código. Una lambda es una función anónima que se puede guardar en una variable o pasar como argumento a otro método — un concepto bastante distinto, que se ve más adelante en su propio tema. De momento basta con saber que `=>` en la firma de un método (como aquí) solo es una forma breve de escribir su cuerpo.

## ref y out

No existen en Java, donde el equivalente pasa por devolver un objeto envoltorio o un array de resultados. Sirven para que un método pueda modificar directamente una variable de quien lo llama, incluso siendo un tipo por valor.

### `ref` — el valor ya debe existir antes de llamar

```csharp
static void Duplicar(ref int numero)
{
    numero *= 2;
}

int valor = 5;
Duplicar(ref valor);
Console.WriteLine(valor); // 10
```

### `out` — el método es quien asigna el valor por primera vez

Se usa cuando un método necesita comunicar dos cosas a la vez: si la operación fue posible (con el `return`, normalmente un `bool`), y el resultado en sí, solo si tuvo éxito (con el parámetro `out`). Es el patrón típico de "intentar hacer algo que puede fallar sin lanzar una excepción".

```csharp
static bool IntentarDividir(int a, int b, out int resultado)
{
    if (b == 0)
    {
        resultado = 0;     // out obliga a asignar algo siempre, aunque aquí no se vaya a usar
        return false;      // le dice a quien llama: "no se pudo, ignora resultado"
    }

    resultado = a / b;      // aquí sí es el dato que de verdad importa
    return true;            // le dice a quien llama: "sí se pudo, usa resultado"
}

if (IntentarDividir(10, 2, out int division))
{
    // el bloque solo se ejecuta si IntentarDividir devolvió true,
    // así que aquí ya se sabe que "division" tiene un resultado válido
    Console.WriteLine($"Resultado: {division}");
}
else
{
    Console.WriteLine("No se puede dividir entre 0.");
}
```

Diferencia clave: con `ref`, la variable tiene que estar inicializada antes de la llamada. Con `out`, no hace falta (de hecho, el método está obligado a asignarle un valor antes de terminar).

## Valores por defecto

Un parámetro puede tener un valor por defecto, que se usa si no se indica ese argumento al llamar al método. Esto sustituye a bastantes de las sobrecargas que en Java tendrías que escribir a mano.

```csharp
static void MostrarMensaje(string texto, int repeticiones = 1)
{
    for (int i = 0; i < repeticiones; i++)
    {
        Console.WriteLine(texto);
    }
}

MostrarMensaje("Hola");           // usa repeticiones = 1
MostrarMensaje("Hola", 3);        // usa repeticiones = 3
```

Los parámetros con valor por defecto deben ir siempre después de los que no lo tienen.

## Cuándo usar cada mecanismo

| Necesidad | Mecanismo |
|---|---|
| Un método hace variantes de lo mismo con distinto tipo/número de datos de entrada | Sobrecarga |
| Un parámetro casi siempre tiene el mismo valor, pero a veces hace falta cambiarlo | Valor por defecto |
| El método necesita modificar una variable que ya existe fuera | `ref` |
| El método necesita "devolver" un resultado extra, además del `return` | `out` |