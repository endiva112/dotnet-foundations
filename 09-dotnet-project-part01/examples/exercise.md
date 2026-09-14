# Ejercicio 01 - Montar un proyecto real

Se te da el código de dos archivos ya escritos. No hace falta que entiendas la sintaxis de `enum` todavía — se verá en el próximo tema. El objetivo de este ejercicio es únicamente entender cómo se organiza y ejecuta un proyecto .NET real, no escribir código nuevo.

Archivo 1 (irá en `Program.cs`):

```csharp
Semaforo semaforo = Semaforo.Rojo;
Console.WriteLine(semaforo);
```

Archivo 2 (irá en `Semaforo.cs`):

```csharp
public enum Semaforo
{
    Rojo,
    Amarillo,
    Verde
}
```

Requisitos:

1. Crea un proyecto real con `dotnet new console -o Solution`, en una carpeta con el nombre que prefieras.
2. Sustituye el contenido del `Program.cs` que se generó automáticamente por el del Archivo 1 de arriba.
3. Crea un archivo nuevo llamado `Semaforo.cs` en la misma carpeta del proyecto, con el contenido del Archivo 2.
4. Crea un archivo nuevo llamado `.gitignore` en la misma carpeta del proyecto y excluye las carpetas `/bin` y `/obj`
5. Ejecuta el proyecto con `dotnet run` desde dentro de la carpeta (sin indicar ningún nombre de archivo) y comprueba que imprime `Rojo`.
6. Fíjate en que `Program.cs` usa `Semaforo` sin nada que lo referencie. Responde por escrito, en un comentario o en un archivo aparte: ¿por qué `Program.cs` puede ver el `enum` declarado en `Semaforo.cs` sin necesitar nada más?
7. Ahora, a propósito, añade una línea suelta (por ejemplo `Console.WriteLine("prueba");`) al principio de `Semaforo.cs`, antes de la declaración del `enum`. Intenta ejecutar `dotnet run` de nuevo y observa el error que da el compilador. Identifica qué código de error aparece.
8. Deshaz el cambio del punto 7 (deja `Semaforo.cs` solo con el `enum`) y confirma que el proyecto vuelve a compilar y ejecutarse sin problema.

Al terminar, deberías tener una carpeta con `Program.cs`, `Semaforo.cs`, el `.csproj` generado, el `.gitignore` y las carpetas `bin`/`obj` (que no hace falta tocar ni revisar en detalle).