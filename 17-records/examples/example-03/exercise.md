# Ejercicio 3 — Clasificar publicaciones

## Contexto

Reutiliza `Publicacion`, `Libro` y `Revista` del Ejercicio 2. La biblioteca quiere una descripción rápida de cada publicación según su tipo y su antigüedad.

## Requisitos

1. Crea un array `Publicacion[]` con al menos seis elementos, mezclando `Libro` y `Revista`, con años de publicación variados (asegúrate de que algunos sean anteriores a 1990 y otros posteriores).

2. Escribe un método que reciba una `Publicacion` y devuelva un `string` según estas reglas:
   - Un `Libro` publicado antes de 1990: `"Libro clásico: <Titulo>"`.
   - Un `Libro` publicado en 1990 o después: `"Libro: <Titulo>"`.
   - Una `Revista` con `NumeroEdicion` menor que 10: `"Revista de tirada limitada: <Titulo>"`.
   - Cualquier otra `Revista`: `"Revista: <Titulo>"`.

   No necesitas acceder a las propiedades después de comprobar el tipo — hay una forma de extraer directamente los valores que te interesan de cada propiedad, dentro de la misma comprobación, sin nombrar cada propiedad por su nombre.

3. Recorre el array y muestra la descripción de cada publicación.

## Para comprobar que funciona

Ajusta los datos del array hasta que aparezca al menos un caso de cada una de las cuatro categorías en la salida.