# Ejercicio 2 — Gestión de pedidos

## Contexto

La tienda quiere un pequeño sistema para decidir qué acción tomar sobre un pedido, según su estado y el dispositivo que contiene.

## Requisitos

1. Crea un enum `EstadoPedido` con al menos los valores `Pendiente`, `Enviado`, `Entregado` y `Cancelado`.

2. Crea una clase `Pedido` con una propiedad `Producto` (de tipo `Dispositivo`), una propiedad `Estado` (de tipo `EstadoPedido`) y una propiedad `FechaCreacion` (de tipo `DateTime`).

3. Escribe un método que reciba un `Pedido` y devuelva un `string` con la acción a realizar, según estas reglas:
   - Si el pedido está `Cancelado`: `"Sin acción — pedido cancelado"`.
   - Si está `Pendiente` y el producto es un `SmartphoneGamer`: `"Priorizar preparación — producto de alta demanda"`.
   - Si está `Pendiente` para cualquier otro producto: `"Preparar pedido"`.
   - Si está `Enviado` y han pasado más de 5 días desde `FechaCreacion`: `"Contactar con el transportista"`.
   - Si está `Enviado` y no han pasado más de 5 días: `"En tránsito, sin acción"`.
   - Si está `Entregado`: `"Solicitar valoración al cliente"`.

   Fíjate en la regla de los "5 días desde `FechaCreacion`": no depende únicamente de una propiedad ni de un valor fijo, sino de una comparación calculada (con la fecha actual). Piensa qué mecanismo de este tema permite añadir una condición así a un caso ya filtrado por estado, cuando esa condición no encaja directamente como parte del patrón.

4. Crea al menos cinco pedidos distintos, cubriendo varias de las reglas anteriores (incluyendo al menos un `Enviado` con más de 5 días y otro con menos), y muestra por consola la acción correspondiente a cada uno.

## Para comprobar que funciona

Cambia la fecha de alguno de los pedidos `Enviado` para que cruce el límite de los 5 días, y confirma que el mensaje cambia en consecuencia.