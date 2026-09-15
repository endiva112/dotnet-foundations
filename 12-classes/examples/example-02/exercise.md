# Ejercicio 02 - Cuenta bancaria

Se te pide modelar una cuenta bancaria sencilla, controlando el acceso al saldo.

Requisitos:

1. Crea una clase `CuentaBancaria` con un campo `private` para el saldo (elige tú el nombre, en camelCase simple o con guion bajo) y una propiedad pública `Titular` (`string`) con `get`/`set` normales.
2. Añade una propiedad `Saldo` de tipo `double` con `get` público pero `set` `private` — de forma que se pueda leer desde fuera de la clase, pero no asignar directamente.
3. Añade un constructor que reciba el titular y un saldo inicial.
4. Añade un método `Depositar(double cantidad)` que aumente el saldo, pero solo si `cantidad` es mayor que `0` (si no lo es, no hagas nada — no hace falta lanzar ningún error todavía, eso se ve en el tema de excepciones).
5. Añade un método `Retirar(double cantidad)` que reduzca el saldo, solo si `cantidad` es mayor que `0` y no deja el saldo en negativo. Devuelve un `bool` indicando si la operación se realizó o no.
6. Desde el punto de entrada del programa, crea una cuenta, deposita y retira varias cantidades (incluyendo alguna que debería fallar, como retirar más de lo disponible), y muestra el saldo después de cada operación usando la propiedad `Saldo`.
7. Intenta escribir `cuenta.Saldo = 999999;` directamente desde fuera de la clase y comprueba que no compila.