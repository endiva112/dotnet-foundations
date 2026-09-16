# Ejercicio 04 - Cuenta atrás y suma de dígitos

Se te pide practicar recursión con dos problemas sencillos.

Requisitos:

1. Crea un método recursivo `static void CuentaAtras(int desde)` que muestre por consola cada número desde `desde` hasta `1`, uno por línea, y termine mostrando `"¡Despegue!"` cuando llegue a `0`. Identifica con claridad cuál es el caso base de esta recursión.
2. Crea un método recursivo `static int SumaDeDigitos(int numero)` que devuelva la suma de todos los dígitos de un número (por ejemplo, `SumaDeDigitos(1234)` debería devolver `10`, ya que `1+2+3+4=10`). Pista: el resto de dividir entre 10 (`numero % 10`) da el último dígito, y dividir entre 10 (división entera) quita ese dígito del número.
3. Llama a ambos métodos con al menos dos valores distintos cada uno, y muestra los resultados.
4. Añade un comentario junto a cada método señalando cuál es su caso base y por qué garantiza que la recursión termina en algún momento.