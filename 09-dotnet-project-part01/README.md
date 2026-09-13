# 09 - Introducción a proyectos .NET

Hasta este punto, cada archivo `.cs` se ha creado y ejecutado de forma individual, probando con el botón de play (▶) de VS Code o con el comando `dotnet run archivo.cs`. Este comportamiento puede resultar esperado si se viene de otros lenguajes como PHP o Java, pero en C# es una funcionalidad relativamente nueva, propia de **.NET 10**, conocida como: *file-based apps*.

Antes de esta función, cualquier programa en .NET —por simple que fuera— requería crear un proyecto primero. Ejecutar `dotnet run` sin un proyecto de por medio daba error, porque el comando necesitaba un archivo `.csproj` para saber qué compilar y cómo; sin él, no tenía ninguna instrucción que seguir.

Los *file-based apps* permiten saltarse ese paso: se ejecuta un `.cs` suelto sin crear ningún archivo de proyecto, y por debajo el SDK genera un proyecto temporal e invisible que contiene únicamente ese archivo, lo compila, lo ejecuta, y lo descarta. Técnicamente se construye la misma estructura que comparte cualquier proyecto .NET, pero se descarta justo después, sin dejar rastro en el disco — ni siquiera se generan las carpetas `bin`/`obj` de las que se habla más abajo.

La consecuencia práctica de esto es importante, aunque existiesen varios `.cs` dentro de la misma carpeta, **cada archivo `.cs` ejecutado hasta ahora, ha sido tratado como un proyecto completamente independiente**. Dos archivos así, en la misma carpeta, nunca han compartido nada entre sí — por eso, si ambos necesitaban el mismo tipo de dato, había que declararlo dos veces, una en cada archivo.

## Qué determina qué archivos ve el compilador

En lenguajes como Java, el compilador solo conoce una clase si se importa explícitamente (`import`), referenciando su paquete y ruta. En C#, el mecanismo es distinto de raíz: lo que decide qué archivos se compilan juntos no es ningún `import` dentro del código, sino el **archivo de proyecto** (`.csproj`). Todos los `.cs` que estén dentro de la carpeta de un proyecto (y sus subcarpetas) se compilan como una única unidad, automáticamente, sin ningún paso adicional — un tipo declarado en un archivo está disponible en cualquier otro archivo del mismo proyecto sin hacer nada más.

Similar a `import`, puede que nos topemos con la palabra reservada `using`, la cual puede resultar familiar viniendo de otros lenguajes, pero en C# cumple una función muy concreta. Solo sirve para acortar el nombre completo al usar una clase:

```csharp
// sin using:
System.Console.WriteLine("Hola");

// con using System; al principio del archivo:
Console.WriteLine("Hola");
```

Ambas líneas hacen exactamente lo mismo y compilan igual — la única diferencia es cuánto hay que escribir. `using` no es lo que permite que el compilador "encuentre" `Console`; `Console` ya está disponible de todas formas gracias a los *implicit usings* (tema de BCL). Un tipo propio, declarado en otro archivo del mismo proyecto, funciona igual: no hace falta ningún `using` para que sea visible, porque la visibilidad la da pertenecer al mismo proyecto, no ninguna palabra clave.

## Creación de un proyecto .NET

```
dotnet new console -o .\nombreProyecto
```

- `dotnet new console`: genera la plantilla de una aplicación de consola.
- `-o .\nombreProyecto`: indica en qué carpeta crearlo (se crea si no existe). El nombre del proyecto y del `.csproj` se toman de esa carpeta.

Ejecutado así, genera:

- `Program.cs` — con top-level statements, como los archivos ya conocidos. Este archivo puede renombrarse sin ningún problema: a diferencia de Java, en C# el nombre de un `.cs` no tiene por qué coincidir con nada de su contenido. Se puede llamar `Program.cs`, `Inicio.cs` o cualquier otro nombre — la única condición es que siga siendo el único archivo del proyecto con top-level statements.
- `nombreProyecto.csproj` — el archivo que convierte esa carpeta en un proyecto real.

El `.csproj` generado es breve:

```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net10.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
  </PropertyGroup>

</Project>
```

Con lo visto hasta ahora, ya se puede leer línea por línea:

- `OutputType -> Exe`: el proyecto es un ejecutable (frente a, por ejemplo, una librería).
- `TargetFramework -> net10.0`: la versión de .NET contra la que se compila.
- `ImplicitUsings -> enable`: activa los *implicit usings* (tema de BCL) — namespaces comunes como `System` disponibles sin escribir `using` a mano.
- `Nullable -> enable`: activa los avisos de NRT (tema 07) — advierte cuando el código contradice lo que declaró sobre si algo puede ser `null`.

No hace falta escribir este archivo de memoria, el comando lo crea para nosotros y de momento no será necesario agregarle contenido adicional; con entender qué hace cada línea es suficiente por ahora.

## Estructura de un proyecto .NET

Tras crear el proyecto y compilarlo al menos una vez, la carpeta tiene esta forma:

```
nombreProyecto/
├── Program.cs
├── nombreProyecto.csproj
├── bin/
└── obj/
```

`Program.cs` y `nombreProyecto.csproj` son los dos archivos que genera `dotnet new console`, ya vistos arriba. `bin` y `obj` aparecen después, al compilar (con `dotnet build` o `dotnet run`):

- **`obj`**: archivos intermedios que el compilador usa durante el proceso de compilación.
- **`bin`**: el resultado final compilado (el ejecutable y sus dependencias).

Ninguna de las dos debe subirse a git — se regeneran automáticamente cada vez que se compila. Un `.gitignore` mínimo para este nivel del curso solo necesita estas dos líneas:

```
bin/
obj/
```

No hacía falta esto antes porque un file-based app nunca llega a generar estas carpetas; a partir de ahora, cualquier carpeta con un proyecto real debería tener este `.gitignore`

## Punto de entrada del proyecto

Todos los ejemplos del temario, hasta ahora, han usado top-level statements — código suelto, sin `class Program` ni un método `Main` explícito, porque el compilador genera esa estructura automáticamente por debajo. Es una forma de escribir C# pensada para programas pequeños y para reducir ceremonia al empezar; según se avance en el temario (sobre todo a partir del tema de clases), su uso se irá haciendo menos frecuente, no porque deje de funcionar, sino porque un programa más grande y organizado en varios archivos suele tener un único punto de entrada muy corto que simplemente arranca el resto, en vez de contener toda la lógica del programa suelta ahí. En la industria, los top-level statements se ven sobre todo en programas pequeños, scripts, ejemplos rápidos y plantillas mínimas de proyectos web (minimal APIs); el código de producción de mayor tamaño suele seguir organizándose con clases desde el principio.

Con proyecto real de por medio, esto tiene una consecuencia concreta: un proyecto solo puede tener **un** archivo con top-level statements. Si se intenta tener top-level statements en dos archivos `.cs` del mismo proyecto, el resultado es un error de compilación: `CS8802: Only one compilation unit can have top-level statements`. Esto es justo lo que no podía pasar con los file-based apps de un solo archivo — nunca había un "segundo archivo" con el que entrar en conflicto, porque nunca se compilaban juntos.

## Dónde van los tipos de datos que necesitan su propio archivo

Por lo general, hay categorías de tipo de dato que se declaran en su propio archivo: `enum` (tema siguiente), y más adelante `class`, `struct` e `interface`. Todos comparten una misma regla dentro de un archivo que contiene top-level statements: cualquier declaración de este tipo tiene que ir físicamente **después** de todos los top-level statements de ese archivo — si no, error de compilación `CS8803: Top-level statements must precede namespace and type declarations`.

Es decir, C# sí permite declarar uno de estos tipos junto a top-level statements en un único archivo, siempre que vaya al final:

```csharp
Semaforo semaforo = Semaforo.Rojo;
Console.WriteLine(semaforo);

enum Semaforo
{
    Rojo,
    Amarillo,
    Verde
}
```

Pero con un proyecto real, la solución habitual no es esa — es simplemente **declarar el tipo en su propio archivo**, sin ninguna restricción de orden:

```csharp
// Semaforo.cs
public enum Semaforo
{
    Rojo,
    Amarillo,
    Verde
}
```

```csharp
// Program.cs
Semaforo semaforo = Semaforo.Rojo;
Console.WriteLine(semaforo);
```

`Program.cs` ve `Semaforo` sin necesidad de `import` o `using` adicional, por la misma razón explicada arriba: ambos archivos pertenecen al mismo proyecto.

## Ejecución de un proyecto .NET

Hasta la fecha se ha usado el botón de play (▶) o `dotnet run archivo.cs`. En un proyecto real, dentro de la carpeta donde está el `.csproj`, basta con:

```
dotnet run
```

Sin nombre de archivo — `dotnet run` encuentra el `.csproj` en la carpeta actual y ya sabe qué compilar y ejecutar.

## Una nota sobre el futuro

.NET 11 planea añadir una directiva, `#:include`, que permitiría incluir varios archivos dentro de un file-based app sin necesidad de un `.csproj`. Para este temario, basado en .NET 10, es irrelevante por ahora.

Más adelante, en el tema **Proyectos .NET II**, se entrará en más detalle en la estructura de tests, capas y convenciones de un proyecto serio, una vez haya piezas suficientes (clases, interfaces...) para que esa organización tenga sentido real.