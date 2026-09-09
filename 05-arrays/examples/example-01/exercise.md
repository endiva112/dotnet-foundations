# Ejercicio 01 - Inventario de una tienda

Se te pide gestionar un pequeño inventario de 5 productos, guardando solo la cantidad de stock de cada uno.

Requisitos:

1. Crea un array de 5 `int` usando `new int[5]` (no lo rellenes con un literal todavía).
2. Asigna manualmente un valor distinto a cada posición usando el índice (`stock[0] = 12;`, etc.).
3. Cambia el valor de una posición ya asignada, simulando una venta (por ejemplo, resta 1 al stock de esa posición).
4. Muestra el contenido completo del array dos veces: una con `for` (mostrando también el índice de cada producto, por ejemplo `"Producto 0: 12 unidades"`) y otra con `foreach` (mostrando solo la cantidad, sin el índice).
5. Muestra también `stock.Length` para comprobar que sigue siendo 5 después de todos los cambios.