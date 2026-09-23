# Ejercicio 1 — Catálogo con condiciones

## Contexto

Reutiliza las clases `Dispositivo`, `Portatil`, `Smartphone` y `SmartphoneGamer` de los ejercicios de Herencia. La tienda quiere generar mensajes distintos según el tipo concreto de cada dispositivo y, en algunos casos, según el valor de alguna de sus propiedades.

## Requisitos

1. Crea un array `Dispositivo[]` con al menos seis elementos, mezclando los tres tipos concretos (al menos dos de cada uno, con precios y características variadas — asegúrate de que algún `Smartphone` tenga menos de 128 GB de almacenamiento, y que algún `Portatil` tenga una pantalla de más de 15 pulgadas).

2. Escribe un método que reciba un `Dispositivo` y devuelva un `string` describiéndolo, según estas reglas (en este orden de prioridad):
   - Si es un `SmartphoneGamer` con `TasaRefresco` de 240 Hz o más: `"Gaming de alta gama"`.
   - Si es cualquier otro `SmartphoneGamer`: `"Gaming estándar"`.
   - Si es un `Smartphone` (que no sea gamer) con menos de 128 GB: `"Smartphone con poco almacenamiento"`.
   - Si es cualquier otro `Smartphone`: `"Smartphone estándar"`.
   - Si es un `Portatil` con pantalla de más de 15 pulgadas: `"Portátil grande"`.
   - Si es cualquier otro `Portatil`: `"Portátil estándar"`.
   - Cualquier otro caso: `"Dispositivo sin clasificar"`.

   No necesitas comprobar el tipo y acceder a sus propiedades por separado — hay una forma de hacer ambas cosas en la misma comprobación, ya vista en este tema.

3. Recorre el array y, para cada dispositivo, imprime su descripción.

## Para comprobar que funciona

Asegúrate de que al menos un dispositivo de cada una de las siete categorías posibles aparece representado en la salida (puede que necesites ajustar los datos del array del punto 1 para conseguirlo).