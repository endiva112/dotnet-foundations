# Ejercicio 1 — Catálogo de libros

## Contexto

Una biblioteca quiere modelar los libros de su catálogo. Un libro, una vez dado de alta, no debería cambiar de título ni de autor — si algo así ocurre, en realidad se trata de otra edición o de una corrección puntual, no de "editar" el mismo registro.

## Requisitos

1. Declara un tipo, con la sintaxis más compacta posible, que represente un `Libro` con `Titulo` (`string`), `Autor` (`string`) y `AnioPublicacion` (`int`). Ningún dato debería poder modificarse después de crear el libro.

2. Crea dos libros distintos con los mismos tres valores exactos, sin ser el mismo objeto (dos llamadas independientes al constructor). Compáralos con `==` e imprime el resultado — antes de ejecutar, anota en un comentario qué esperas que imprima y por qué, comparándolo con lo que habría pasado si `Libro` fuera una `class` normal.

3. Imprime uno de los libros directamente con `Console.WriteLine`, sin concatenar ni acceder a sus propiedades una a una. Anota en un comentario qué es lo que hace posible que salga algo legible sin haber escrito nada para conseguirlo.

4. Una biblioteca vecina te presta el mismo libro, pero con una reimpresión de 2024. Sin modificar ninguno de los libros ya creados, obtén una copia con el mismo título y autor pero con `AnioPublicacion = 2024`, usando la sintaxis pensada para eso.

## Para comprobar que funciona

Imprime los tres libros (los dos originales y la copia del punto 4) y confirma que los dos primeros son iguales entre sí, y que el tercero, aunque comparta título y autor con ellos, no lo es.