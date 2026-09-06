# Ejercicio 03 - Saludo multilingüe

Se te pide un método `SaludarVarios` que salude a alguien un número de veces, en un idioma determinado.

```csharp
static void SaludarVarios(string nombre, int veces = 1, string idioma = "es")
{
    // tu código aquí
}
```

Requisitos:

1. `idioma` puede ser `"es"`, `"en"` o `"fr"`. Dentro del método, usa un `switch` clásico (tema 02) para decidir el saludo:
   - `"es"` → `"Hola"`
   - `"en"` → `"Hello"`
   - `"fr"` → `"Bonjour"`
   - cualquier otro valor → `"Hola"` (por defecto)
2. Usa un `for` (tema 04) para repetir el saludo `veces` veces, mostrando algo como `"Hola, Ana!"` en cada línea.
3. Llama al método al menos tres veces, combinando argumentos por defecto y explícitos:
   - Solo con `nombre` (usa los valores por defecto de `veces` e `idioma`).
   - Con `nombre` y `veces`, sin indicar `idioma`.
   - Con los tres, usando argumentos nombrados y en un idioma distinto de `"es"`.

Este ejercicio reutiliza: interpolación de strings (tema 03), `for` (tema 04) y `switch` (tema 02), además del propio concepto nuevo de valores por defecto.