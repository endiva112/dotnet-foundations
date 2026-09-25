

/*
# Ejercicio 2 — Publicaciones y sus tipos

## Contexto

Además de libros, la biblioteca también cataloga revistas. Ambos comparten algunos datos, pero son cosas distintas — 
nadie confundiría un libro con una revista solo porque coincidan en título y año.

## Requisitos

1. Declara un tipo base `Publicacion` con `Titulo` (`string`) y `AnioPublicacion` (`int`), usando sintaxis posicional.

2. Declara dos tipos que hereden de `Publicacion`:
   - `Libro`, añadiendo `Autor` (`string`).
   - `Revista`, añadiendo `NumeroEdicion` (`int`).

   Ambos deben poder construirse pasando todos sus datos (los propios y los heredados) en una sola llamada.

3. Crea un `Libro` y una `Revista` que compartan exactamente el mismo `Titulo` y el mismo `AnioPublicacion` (aunque cada uno 
tenga, además, su propio dato adicional con cualquier valor). Sube ambos a una variable de tipo `Publicacion` y compáralos 
entre sí. Antes de ejecutar, anota en un comentario qué esperas que devuelva la comparación, y por qué, teniendo en cuenta lo 
que ya viste sobre igualdad en el tema anterior de Herencia.

4. Crea un segundo `Libro`, distinto del primero solo en el año de publicación, obtenido a partir del primero sin volver a 
escribir el título ni el autor.

## Para comprobar que funciona

La comparación del punto 3 debe confirmar tu hipótesis. Si no lo hace, revisa qué falta para que dos tipos de una misma jerarquía,
 aunque compartan valores en las propiedades heredadas, no se consideren iguales.
*/