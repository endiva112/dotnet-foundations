# Ejercicio 1 — Jerarquía de dispositivos

## Contexto

Una tienda de electrónica quiere modelar su catálogo. Todo dispositivo tiene una marca, un precio y una forma de "encenderse", pero cada tipo de dispositivo se enciende de una forma distinta. Además, dentro de los smartphones hay un subtipo, los smartphones gaming, con características propias.

## Requisitos

1. Crea una clase abstracta `Dispositivo` con:
   - Una propiedad `Marca` (`string`), de solo lectura desde fuera de la clase, pero modificable por cualquier derivada (el modificador de acceso adecuado ya se ha visto en este tema).
   - Una propiedad `Precio` (`decimal`), con `get` y `set` públicos.
   - Un constructor que reciba `marca` y `precio`, con el modificador de acceso que corresponda a una clase abstracta.
   - Un método abstracto `Encender()`, sin cuerpo, que no reciba parámetros ni devuelva nada.
   - Un método `MostrarFicha()`, **no** abstracto, que imprima marca y precio por consola (algo como `"Marca: X — Precio: Y€"`). Este método no se sobrescribe en ninguna derivada.

2. Crea una clase `Portatil : Dispositivo` con:
   - Una propiedad propia `TamanioPantalla` (`double`, en pulgadas).
   - Un constructor que reciba `marca`, `precio` y `tamanioPantalla`, y delegue correctamente en el constructor de `Dispositivo`.
   - Su propio `Encender()`, que imprima algo distinto a lo que imprime cualquier otro dispositivo (por ejemplo, mencionando que carga el sistema operativo).

3. Crea una clase `Smartphone : Dispositivo` con:
   - Una propiedad propia `CapacidadAlmacenamiento` (`int`, en GB).
   - Un constructor que reciba `marca`, `precio` y `capacidadAlmacenamiento`.
   - Su propio `Encender()`.
   - Un método `TomarFoto()` que imprima algo simulando que se ha tomado una foto. Este método **no** existe en `Dispositivo`, es exclusivo de `Smartphone`.

4. Crea una clase `SmartphoneGamer : Smartphone` (herencia multinivel) con:
   - Una propiedad propia `TasaRefresco` (`int`, en Hz).
   - Un constructor que reciba `marca`, `precio`, `capacidadAlmacenamiento` y `tasaRefresco`, delegando en el constructor de `Smartphone`.
   - Un `override` de `Encender()` que primero llame a la implementación de `Smartphone` (con `base.Encender()`) y después añada una línea propia (por ejemplo, mencionando el modo gaming).

5. Sobrescribe `ToString()` en `Smartphone` para que devuelva algo como `"Smartphone Marca (Almacenamiento GB)"`. No hace falta sobrescribirlo en `SmartphoneGamer` — comprueba (y explica en un comentario) qué versión de `ToString()` se ejecuta si se llama sobre una instancia de `SmartphoneGamer`, y por qué.

## Para comprobar que funciona

Instancia un `Portatil`, un `Smartphone` y un `SmartphoneGamer`, y para cada uno llama a `MostrarFicha()` y `Encender()`. Imprime también el resultado de `ToString()` sobre el `SmartphoneGamer`.