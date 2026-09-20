# Ejercicio 4 — ¿Herencia o composición?

## Parte A — Decidir

Para cada uno de los siguientes tres casos, decide si la relación encaja mejor como herencia o como composición, aplicando los criterios vistos en el readme (relación "es un" real y permanente, datos/comportamiento compartido, interés real en polimorfismo, independencia del tiempo o el contexto). Escribe la decisión y una justificación breve (dos o tres líneas) para cada uno, en un archivo de texto o como comentarios — no hace falta código todavía en esta parte.

1. `Coche` y `Motor`.
2. `Cuadrado` y `Rectangulo`.
3. `Usuario` y `Suscripcion` (una suscripción que el usuario puede tener, renovar o cancelar).

## Parte B — Implementar la que elegiste como composición

De los tres casos anteriores, al menos uno debería haberte salido como composición y no como herencia. Impleméntalo con composición real:

- La clase "contenedora" debe tener una propiedad cuyo tipo sea la otra clase (no heredar de ella).
- Debe ser posible crear la clase contenedora sin decidir todavía los detalles de la clase contenida, y asignarla o reemplazarla después.
- Añade al menos un método en la clase contenedora que delegue en la clase contenida (por ejemplo, si elegiste `Usuario`/`Suscripcion`, un método `Usuario.RenovarSuscripcion()` que internamente llame a un método de `Suscripcion`).

## Para comprobar que funciona

Crea una instancia de la clase contenedora, comprueba que puedes acceder a la clase contenida a través de ella, y que el método de delegación funciona correctamente.