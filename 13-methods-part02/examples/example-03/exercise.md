# Ejercicio 03 - Registro de eventos y suma flexible

Se te pide combinar parámetros opcionales con `params`.

Requisitos:

**Parte A — valores por defecto:**

1. Crea un método `static void RegistrarEvento(string mensaje, string nivel = "Info", bool mostrarHora = false)` que muestre el mensaje con un formato como `"[Info] Algo ha pasado"`, y si `mostrarHora` es `true`, añada también la hora actual (`DateTime.Now`, tema de BCL) al mensaje.
2. Llama al método tres veces: una usando solo el mensaje (el resto por defecto), otra indicando también el nivel, y otra usando argumentos por nombre para saltarte `nivel` y activar `mostrarHora` directamente.

**Parte B — `params`:**

3. Crea un método `static double Promedio(params double[] valores)` que calcule la media de los valores recibidos. Si no se recibe ningún valor, debe devolver `0` sin dividir por cero.
4. Llama al método con distintas cantidades de argumentos (dos, cinco, y ninguno), mostrando el resultado de cada llamada.