# Ejercicio 01 - Control de acceso a un evento

Se te pide programar el control de acceso a la entrada de un evento nocturno.

Datos de partida (defínelos tú con los valores que quieras para probar distintos casos):

```csharp
int edad = 20;
bool llevaEntrada = true;
```

Reglas, en este orden de prioridad:

1. Si es menor de 18 años, se deniega el acceso: `"Acceso denegado: eres menor de edad."`
2. Si tiene 65 años o más, entra gratis sin necesidad de entrada: `"Acceso gratuito para mayores de 65."`
3. Si tiene entre 18 y 64 años y **no** lleva entrada, se deniega el acceso: `"Acceso denegado: no llevas entrada."`
4. Si tiene entre 18 y 64 años y lleva entrada, se le da acceso: `"Bienvenido, disfruta del evento."`

Requisitos:

- Resuélvelo únicamente con `if / else if / else`. No uses `switch` ni el operador ternario en este ejercicio.
- Usa al menos un operador de comparación (`>=`, `<`, etc.) y al menos un operador lógico (`&&`, `||` o `!`).
- Prueba el programa cambiando los valores de `edad` y `llevaEntrada` para comprobar que las 4 reglas funcionan.