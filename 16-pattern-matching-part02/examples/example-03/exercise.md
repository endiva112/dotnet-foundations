# Ejercicio 3 — Procesar una entrega

## Contexto

Al recibir un `Dispositivo` en el almacén, hay que ejecutar una acción distinta según qué es y en qué condiciones llega — pero aquí no se necesita devolver ningún texto ni valor, solo actuar (imprimir por consola, en este caso). Esto no encaja como una expresión que produce un resultado: encaja mejor como una instrucción con varios casos, algo que ya conoces de otro tema.

## Requisitos

1. Escribe un método `void` que reciba un `Dispositivo` y, según su tipo y (cuando corresponda) sus propiedades, imprima un mensaje:
   - Un `Portatil` con menos de 13 pulgadas: mensaje indicando que se etiqueta como "ultraportátil".
   - Cualquier otro `Portatil`: mensaje indicando que se etiqueta como "estándar".
   - Un `SmartphoneGamer`: mensaje indicando que se revisa la refrigeración antes de almacenarlo.
   - Cualquier otro `Smartphone`: mensaje indicando que se almacena directamente.
   - Cualquier otro dispositivo no contemplado: mensaje indicando que se revisa manualmente.

2. La sintaxis que ya usaste en los ejercicios 1 y 2 para comprobar tipo y propiedades a la vez no es exclusiva de la expresión `switch` — funciona igual en la instrucción que vas a usar aquí. No necesitas convertir nada a mano ni comprobar el tipo por separado antes de acceder a sus propiedades.

3. Prueba el método con al menos cinco dispositivos distintos que cubran todas las ramas.

## Para comprobar que funciona

Cada uno de los cinco casos de la lista debe producir su mensaje correspondiente, sin que ningún dispositivo caiga en una rama equivocada.