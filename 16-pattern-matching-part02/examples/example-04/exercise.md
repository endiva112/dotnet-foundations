# Ejercicio 4 — Informe de almacén

## Contexto

El almacén necesita dos cosas sobre el mismo lote de dispositivos: una acción inmediata para cada uno, y después un informe en texto que se pueda guardar o enviar. Son necesidades distintas, y en este tema ya has visto dos formas distintas de aplicar patrones — piensa cuál encaja mejor en cada caso antes de escribir nada.

## Requisitos

1. Crea un array `Dispositivo[]` con al menos seis elementos variados, reutilizando `Portatil`, `Smartphone` y `SmartphoneGamer`.

2. Para cada dispositivo del array, hay que imprimir por consola una alerta si (y solo si) su precio supera los 400, indicando de qué tipo concreto es y cuánto cuesta. Los dispositivos por debajo de ese precio no generan ninguna salida.

3. Después, necesitas una línea de texto por dispositivo (todas juntas, formando un informe) que lo describa según su tipo y sus características — puedes reutilizar el mismo criterio de clasificación que ya escribiste en el Ejercicio 1, o definir uno propio si lo prefieres.

4. Imprime primero las alertas del punto 2, y después el informe completo del punto 3.

## Para comprobar que funciona

Ajusta los precios del array para que algunos dispositivos generen alerta y otros no, y confirma que el informe final incluye una línea por cada dispositivo, sin excepción, independientemente de si generó alerta o no.