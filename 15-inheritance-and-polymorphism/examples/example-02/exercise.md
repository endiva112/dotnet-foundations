# Ejercicio 2 — Inventario polimórfico

## Contexto

Este ejercicio parte de las clases del Ejercicio 1 (`Dispositivo`, `Portatil`, `Smartphone`, `SmartphoneGamer`). La tienda quiere recorrer su inventario completo sin tener que preguntar, dispositivo a dispositivo, de qué tipo concreto es cada uno.

## Requisitos

1. Crea un array de tipo `Dispositivo[]` con al menos cinco elementos, mezclando instancias de `Portatil`, `Smartphone` y `SmartphoneGamer` (al menos una de cada tipo).

2. Recorre el array con un único `foreach` y, para cada dispositivo, llama a `MostrarFicha()` y a `Encender()`. No debe haber ningún `if`, `switch` ni comprobación de tipo dentro de este bucle — la llamada tiene que funcionar igual para los tres tipos, apoyándose únicamente en que `Encender()` es `virtual`/`override`.

3. Después de ese recorrido, escribe un segundo bucle sobre el mismo array que haga lo siguiente: para cada dispositivo que sea realmente un `Smartphone` (o un `SmartphoneGamer`, que también lo es), llama a `TomarFoto()`. Como `TomarFoto()` no existe en `Dispositivo`, necesitarás comprobar el tipo con `is` y convertir explícitamente con un cast antes de poder llamarlo — no uses todavía la forma abreviada `is Smartphone s` (patrón de tipo), eso se ve en el tema siguiente.

4. Añade al array un `Dispositivo` cualquiera (por ejemplo, un `Portatil`) e intenta, a propósito, convertirlo a `Smartphone` con un cast directo, sin comprobar antes con `is`. Ejecuta el programa y confirma que salta una excepción en tiempo de ejecución — anota en un comentario qué tipo de excepción es y por qué ocurre. Después, comenta esa línea (o bórrala) para que el resto del programa pueda ejecutarse sin interrumpirse.

## Para comprobar que funciona

El programa debe imprimir la ficha y el "encendido" de los cinco dispositivos sin ningún chequeo de tipo, y a continuación solo las fotos de los que realmente son smartphones.