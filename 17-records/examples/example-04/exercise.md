# Ejercicio 4 — Coordenadas de estanterías

## Contexto

La biblioteca quiere registrar la posición física de cada estantería en la sala, como una coordenada simple (fila y columna). Es un dato pequeño, de vida corta, que se comporta como un valor — el mismo tipo de caso que ya se trató en el tema de Structs.

## Requisitos

1. Declara un tipo `Posicion` con `Fila` (`int`) y `Columna` (`int`), combinando lo visto en Structs (semántica de valor) con lo visto en este tema (igualdad y demás miembros generados automáticamente), usando sintaxis posicional.

2. Crea dos posiciones con los mismos valores y compruébalas con `==`. No hace falta que escribas nada adicional para que esto compile y funcione — si te encuentras necesitando `operator ==` a mano, revisa qué tipo declaraste en el punto 1.

3. Crea un array de varias `Posicion` y recorre el array con un `foreach`, intentando modificar la `Fila` de cada elemento directamente dentro del bucle. Observa qué ocurre al compilar, y compáralo con lo que ya viste en Structs sobre la variable de un `foreach`.

4. Declara una segunda versión del mismo tipo, esta vez impidiendo que sus propiedades se puedan modificar después de construidas. Repite el punto 2 (la comparación con `==`) para confirmar que la igualdad se sigue comportando igual.

## Para comprobar que funciona

El punto 3 debe dar un error de compilación con la primera versión de `Posicion`. Anota en un comentario, con tus propias palabras, por qué ese error ocurre con este tipo y no ocurriría con una `class` equivalente.