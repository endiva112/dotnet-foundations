# Ejercicio 01 - Presentador de personajes

Se te pide crear un método llamado `Presentar` que muestre información de un personaje, con al menos 3 sobrecargas distintas:

1. `Presentar(string nombre)` → muestra algo como `"Este es Aragorn."`
2. `Presentar(string nombre, int edad)` → muestra algo como `"Este es Aragorn, tiene 87 años."`
3. `Presentar(string nombre, int edad, string raza)` → muestra algo como `"Este es Aragorn, tiene 87 años, es de raza humana."`

Requisitos:

- Las tres deben ser métodos `static` distintos con el mismo nombre (`Presentar`), diferenciados solo por su lista de parámetros.
- Llama a las tres desde el método principal, con datos de al menos dos personajes distintos.
- En al menos una de las llamadas, usa argumentos nombrados (`Presentar(edad: 87, nombre: "Aragorn")`) en vez del orden posicional habitual.

No hace falta `ref`, `out` ni valores por defecto en este ejercicio — eso se ve en los dos siguientes.