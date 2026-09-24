# 01 - Tipos, variables, constantes y comentarios

Una variable es un contenedor que almacena un valor. C# es fuertemente tipado, por lo que la variable solo puede almacenar un único `tipo` de dato.

## Tipos de datos básicos

| Tipo | Qué guarda | Ejemplo |
|---|---|---|
| `int` | Entero (32 bits) | `int edad = 30;` |
| `long` | Entero grande (64 bits) | `long poblacion = 8000000000L;` |
| `double` | Decimal (64 bits, el más usado por defecto) | `double precio = 19.99;` |
| `float` | Decimal (32 bits, menos preciso, necesita sufijo `f`) | `float x = 3.14f;` |
| `decimal` | Decimal de alta precisión, para dinero | `decimal total = 19.99m;` |
| `bool` | Verdadero / falso | `bool activo = true;` |
| `char` | Un único carácter | `char inicial = 'A';` |
| `string` | Cadena de texto | `string nombre = "Ana";` |

`string` es un tipo por referencia (vive en el heap). El resto de la tabla son tipos por valor (viven en la pila/stack, se copian al asignarlos). Esto se nota más adelante: si copias un `int` a otra variable y cambias la copia, el original no cambia; con objetos (referencia), ambas variables apuntan a lo mismo. De momento basta con saber que existe esa diferencia — se explica con detalle, memoria incluida, en un tema de refuerzo más adelante, una vez se hayan visto clases, structs y records, que es cuando el contraste se aprecia mejor con ejemplos reales delante.

## Inferencia con `var`

```csharp
var edad = 30;       // el compilador infiere int
var nombre = "Ana";  // el compilador infiere string
```

`var` no es tipado dinámico (no es como `var` en JavaScript). El tipo se decide en tiempo de compilación según lo que hay a la derecha del `=`, y ya no cambia. Es solo azúcar sintáctico para no repetir el tipo cuando ya es obvio.

## Constantes

```csharp
const double Pi = 3.14159;
```

- Se resuelve en tiempo de compilación, así que el valor tiene que conocerse al escribir el código.
- Convención de nombres en C#: PascalCase (`Pi`, `MaxIntentos`), a diferencia de Java que usa `MAX_INTENTOS` en mayúsculas.
- Más adelante (tema de clases) aparece `readonly`, que es parecido pero se asigna en tiempo de ejecución. No hace falta ahora.

## Comentarios

```csharp
// comentario de una línea

/* comentario
   de varias líneas */

/// comentario de documentación (XML), se usa sobre métodos y clases.
/// Se verá con más detalle cuando toque documentar APIs.
```

También existen `#region` / `#endregion`, un par de directivas que no son comentarios en sentido estricto, pero cumplen un papel parecido: agrupan un bloque de código bajo un nombre, para poder plegarlo y desplegarlo en el editor.

```csharp
#region Validaciones

// código de validación aquí

#endregion
```

No afectan en nada a la compilación ni al comportamiento del programa — son una ayuda puramente visual del IDE para organizar archivos largos. Se usan con moderación: en un archivo pequeño no hacen falta, y depender de ellas para que un archivo se lea bien suele ser señal de que ese archivo debería dividirse en varios más pequeños, en vez de organizarse a base de regiones.

## Mostrar información por consola

```csharp
Console.WriteLine("Hola mundo");           // imprime y salta de línea
Console.Write("sin salto de línea");       // imprime sin saltar de línea
Console.WriteLine($"Nombre: {nombre}");    // interpolación de strings
```

Esto es solo lo mínimo para poder ver resultados en los ejercicios. La consola como tal (lectura de entrada, formateo, etc.) se trata en profundidad más adelante, en el tema de librerías y métodos nativos.

## Ejecutar el programa

Dos formas, hacen lo mismo:

- Botón de play (▶) en la esquina superior derecha del IDE (VS Code con C# Dev Kit).
- Desde terminal, en la carpeta del proyecto:

```
dotnet run
```

Por ahora `dotnet run` es solo "el comando para lanzar esto". El resto de comandos de la CLI de dotnet (`new`, `build`, `test`, `add package`, etc.) se ven más adelante, cuando se trate dotnet en profundidad y no solo C# como lenguaje.