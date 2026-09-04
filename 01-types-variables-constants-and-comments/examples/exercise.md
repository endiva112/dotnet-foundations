# Ejercicio 01 - Datos curiosos

Se te pide crear un programa que muestre un par de datos curiosos por consola, usando el tipo de dato correcto para cada uno.

Requisitos:

- Usa al menos una variable de cada tipo visto en el tema (`int`, `long`, `double`, `float`, `decimal`, `bool`, `char`, `string`).
- Junto a cada variable, deja un comentario de una línea explicando por qué ese es el tipo adecuado.
- Reparte la salida en 2 o 3 `Console.WriteLine`, no todo en uno solo.
- Algunos datos no los vas a saber de memoria: búscalos (número aproximado real, no hace falta que sea exacto al dígito).

Empieza a partir de estas líneas:

```csharp
//Declaración de variables:
string mostSpokenLanguage = "inglés"; // texto -> string

//Mensaje de salida. No modificar esta parte.
Console.WriteLine($"Se han catalogado {starsCount} estrellas en la Vía Láctea, y existen aproximadamente {beetleSpecies} especies conocidas de escarabajos.");
Console.WriteLine($"La Luna está a una media de {moonDistanceKm} km de la Tierra, y la capa de ozono tiene un grosor medio de {ozoneLayerThicknessKm} km.");
Console.WriteLine($"Un bitcoin cuesta hoy {bitcoinPriceUsd} dólares. Plutón {(isPlutoAPlanet ? "sigue siendo" : "ya no es")} un planeta, el idioma más hablado del mundo es el {mostSpokenLanguage} y el grupo sanguíneo más común es el tipo {mostCommonBloodType}.");
```

A partir de ahí, añade el resto de datos que hagan falta hasta cubrir todos los tipos.