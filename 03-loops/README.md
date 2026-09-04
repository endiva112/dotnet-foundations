# 03 - Bucles

Un bucle repite un bloque de código mientras se cumpla una condición, o una vez por cada elemento de una colección.

## while

Comprueba la condición **antes** de cada vuelta. Si la condición es falsa desde el principio, el bloque no se ejecuta ni una vez.

```csharp
int contador = 0;

while (contador < 5)
{
    Console.WriteLine(contador);
    contador++;
}
```

## do-while

Igual que `while`, pero comprueba la condición **después** de cada vuelta. El bloque se ejecuta siempre al menos una vez, aunque la condición sea falsa desde el principio.

```csharp
int intento = 0;

do
{
    Console.WriteLine($"Intento {intento}");
    intento++;
} while (intento < 3);
```

Se usa poco comparado con `while`. Es útil cuando por definición el bloque tiene que correr al menos una vez, típicamente en menús: "muestra el menú, pide una opción, repite hasta que el usuario elija salir".

## for

Cuando se conoce de antemano cuántas veces hay que repetir (o se necesita un contador explícito), `for` agrupa inicialización, condición e incremento en una sola línea.

```csharp
for (int i = 0; i < 5; i++)
{
    Console.WriteLine(i);
}
```

Equivale al `while` de arriba, pero deja claro de un vistazo dónde empieza el contador, hasta dónde llega y cómo avanza.

## foreach

Recorre cada elemento de una colección (array, `List<T>`, etc.) sin necesidad de gestionar un índice manualmente.

```csharp
string[] nombres = { "Ana", "Luis", "Marta" };

foreach (string nombre in nombres)
{
    Console.WriteLine(nombre);
}
```

No se puede usar `foreach` para modificar el índice ni saltar posiciones — solo avanza elemento a elemento. Si se necesita el índice, o modificar la colección mientras se recorre, hace falta `for`.

## break y continue

- `break` corta el bucle por completo, salta a lo que hay después de él.
- `continue` corta solo la vuelta actual, y pasa directamente a la siguiente.

```csharp
for (int i = 0; i < 10; i++)
{
    if (i == 3) continue; // se salta el 3
    if (i == 6) break;    // se detiene al llegar a 6
    Console.WriteLine(i);
}
```

## Cuándo usar cada uno

| Situación | Bucle |
|---|---|
| Se conoce el número de repeticiones o se necesita un contador | `for` |
| Se repite mientras se cumpla una condición, número de vueltas desconocido | `while` |
| El bloque tiene que ejecutarse al menos una vez sí o sí | `do-while` |
| Se recorre una colección entera, sin necesitar el índice | `foreach` |