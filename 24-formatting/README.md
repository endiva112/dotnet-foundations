# 24 - Formato

Este tema es, a propósito, más una referencia rápida que un desarrollo en profundidad — nada de lo que aparece aquí introduce ningún concepto nuevo del lenguaje; son formas concretas de mostrar datos ya conocidos (números, texto, fechas) de manera legible.

## `String.Format` frente a interpolación

```csharp
string nombre = "Ana";
int edad = 30;

string mensaje1 = string.Format("Hola {0}, tienes {1} años", nombre, edad);
string mensaje2 = $"Hola {nombre}, tienes {edad} años";
```

Ambas líneas producen el mismo resultado. `{0}`, `{1}`... son marcadores de posición que `String.Format` sustituye, en orden, por los argumentos que le siguen — es la forma en que se hacía esto antes de que existiera la interpolación de strings (tema 03). Hoy en día la interpolación es la opción preferida por ser más legible (el nombre de la variable está directamente donde va a aparecer, no hay que contar posiciones); `String.Format` sigue apareciendo en código ya existente, o en los casos, poco frecuentes, donde la propia cadena de formato se decide en tiempo de ejecución en vez de escribirse directamente en el código.

Todo lo que sigue en este tema (los especificadores de formato) funciona exactamente igual dentro de una interpolación que dentro de `String.Format` — solo cambia la sintaxis que los envuelve.

## Especificadores de formato numérico

Dentro de una interpolación, después de `:`, se puede indicar cómo formatear el valor:

```csharp
decimal precio = 1234.5m;

Console.WriteLine($"{precio:C}"); // "1.234,50 €" (o "$1,234.50", según la configuración regional)
Console.WriteLine($"{precio:N}"); // "1.234,50" — número con separador de miles, sin símbolo de moneda
Console.WriteLine($"{precio:F2}"); // "1234,50" — siempre 2 decimales, sin separador de miles

double proporcion = 0.256;
Console.WriteLine($"{proporcion:P}"); // "25,60 %" — como porcentaje

int codigo = 7;
Console.WriteLine($"{codigo:D4}"); // "0007" — entero con ceros a la izquierda hasta completar 4 dígitos
```

| Especificador | Qué hace | Ejemplo |
|---|---|---|
| `C` | Moneda, con el símbolo de la configuración regional | `1.234,50 €` |
| `N` | Número con separador de miles | `1.234,50` |
| `F2` | Decimales fijos (el número indica cuántos) | `1234,50` |
| `P` | Porcentaje (multiplica por 100 y añade `%`) | `25,60 %` |
| `D4` | Entero con ceros a la izquierda (el número indica el ancho mínimo) | `0007` |

El símbolo exacto de moneda, el separador decimal (`,` o `.`) y el separador de miles no están fijados por el especificador en sí — dependen de la configuración regional de la máquina donde se ejecuta el programa, tal como se explica en la sección de `CultureInfo`, más abajo.

## `X` — hexadecimal

```csharp
int numero = 255;
Console.WriteLine($"{numero:X}"); // "FF"
Console.WriteLine($"{numero:X4}"); // "00FF" — con ceros a la izquierda hasta completar 4 dígitos
```

Aparece con cierta frecuencia en contextos de bajo nivel (códigos de color, operaciones a nivel de bit, depuración de valores) — suficiente con reconocer la sintaxis, sin necesidad de profundizar más.

## `PadLeft` y `PadRight`

No son especificadores de formato — son métodos de `string` (BCL) para alinear texto añadiendo espacios (u otro carácter) hasta alcanzar un ancho determinado:

```csharp
Console.WriteLine("Ana".PadRight(10) + "|"); // "Ana       |" — rellena por la derecha
Console.WriteLine("Ana".PadLeft(10) + "|");  // "       Ana|" — rellena por la izquierda
```

El caso de uso típico es imprimir una tabla legible por consola, con varias columnas alineadas:

```csharp
Console.WriteLine("Nombre".PadRight(10) + "Edad".PadLeft(5));
Console.WriteLine("Ana".PadRight(10) + "30".PadLeft(5));
Console.WriteLine("Luis".PadRight(10) + "25".PadLeft(5));
```

```
Nombre     Edad
Ana          30
Luis         25
```

## `CultureInfo`

Los ejemplos de arriba mostraban resultados con coma decimal y punto de miles — la convención habitual en español. Pero ese formato no está fijado por el lenguaje: depende de la **cultura** configurada en la máquina donde corre el programa.

```csharp
decimal precio = 1234.5m;

Console.WriteLine(precio.ToString("N2")); // "1.234,50" en una máquina configurada en español...
                                           // ...pero "1,234.50" en una configurada en inglés (EE. UU.)
```

Esto no es solo una cuestión de visualización — también afecta a **parsear** texto de vuelta a número, y ahí es donde se convierte en un problema real:

```csharp
decimal precio = decimal.Parse("1234,50"); // funciona en una máquina en español...
                                             // ...pero lanza FormatException en una configurada en inglés,
                                             // porque allí la coma no es el separador decimal esperado
```

Un programa que funciona perfectamente en la máquina de quien lo escribió, y falla en la de otra persona (o en un servidor con otra configuración regional), es un bug real y frecuente en código que no tiene en cuenta este detalle. La solución estándar, cuando el formato de un número no debe depender de dónde se ejecute el programa (por ejemplo, al guardar un dato en un archivo o enviarlo a otro sistema, en vez de mostrarlo a una persona), es forzar explícitamente una cultura fija:

```csharp
using System.Globalization;

decimal precio = decimal.Parse("1234.50", CultureInfo.InvariantCulture); // siempre con punto, sin importar la máquina

string texto = precio.ToString("N2", CultureInfo.InvariantCulture); // siempre "1,234.50", sin importar la máquina
```

`CultureInfo.InvariantCulture` es una configuración neutral, fija, que no cambia según la máquina — la opción por defecto razonable para cualquier formato de número (o fecha) que el programa vaya a leer o escribir él mismo, en vez de mostrar directamente a una persona.

## Formato de fechas, lo mínimo

Sobre el `DateTime` ya visto en BCL, el mismo mecanismo de `:` dentro de una interpolación permite indicar cómo mostrar la fecha:

```csharp
DateTime fecha = new DateTime(2026, 3, 15);

Console.WriteLine($"{fecha:yyyy-MM-dd}"); // "2026-03-15"
Console.WriteLine($"{fecha:dd/MM/yyyy}"); // "15/03/2026"
```

Cada letra representa una parte de la fecha (`yyyy` año con 4 dígitos, `MM` mes con 2 dígitos, `dd` día con 2 dígitos), y se combinan en el orden que se quiera mostrar. Con estos dos formatos cubiertos —uno pensado para ordenar o guardar (`yyyy-MM-dd`, que además ordena correctamente como texto) y otro para mostrar a una persona en el formato habitual en español— es suficiente para lo que este curso necesita; existen muchas más combinaciones posibles, que se consultan en la documentación en el momento en que hagan falta.