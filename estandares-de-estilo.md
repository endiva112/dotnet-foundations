# Estándares de estilo en C#

Notas rápidas, se amplía según aparezcan más casos.

## Naming

| Elemento | Convención | Ejemplo |
|---|---|---|
| Variable local | camelCase | `int edad`, `string nombreCompleto` |
| Parámetro | camelCase | `Saludar(string nombre)` |
| Método | PascalCase | `CalcularMedia()`, `Sumar()` |
| Constante | PascalCase | `const double Pi = 3.14159;` |
| Clase / tipo | PascalCase | `class Cliente`, `struct Punto` |

Regla simple: si es un tipo (clase, struct, método) o una constante, PascalCase. Si es una variable o parámetro normal, camelCase.

No mezclar estilos dentro del mismo elemento (`stockActual` vale, `Stock_actual` o `StockActual` para una variable local no).

## Comentarios

En minúscula tras `//`, salvo que empiecen con un nombre propio o sigla:

```csharp
// esto está bien
// Esto no sigue la convención del resto del código
```

## Llaves: Allman style

C# usa **Allman style**: cada llave `{` y `}` va en su propia línea, siempre — incluye `else`, `else if`, bucles, métodos, todo.

```csharp
if (edad < 18)
{
    Console.WriteLine("Menor de edad");
}
else
{
    Console.WriteLine("Adulto");
}
```

Mal (estilo común en C/Java/JS, pero no en C#):

```csharp
if (edad < 18) {
    Console.WriteLine("Menor de edad");
} else {
    Console.WriteLine("Adulto");
}
```

Un formateador automático (el del SDK, o el de VS/VS Code) reescribe esto solo a Allman style. Si se deja con el otro estilo, el primer `dotnet format` va a mover todo y generar un diff grande sin motivo real.

