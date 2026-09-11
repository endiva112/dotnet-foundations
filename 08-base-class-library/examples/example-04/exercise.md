# Ejercicio 04 - Ficha de nacimiento

Se te pide calcular un par de datos a partir de una fecha de nacimiento.

Dato de partida:

```csharp
DateTime fechaNacimiento = new DateTime(2000, 2, 29);
```

Requisitos:

1. Muestra la fecha de nacimiento por consola.
2. Calcula y muestra cuántos días han pasado desde esa fecha hasta hoy (usa la fecha y hora actuales).
3. A partir de esa cantidad de días, calcula aproximadamente a cuántos años equivale (puedes dividir entre 365 y quedarte con la parte entera) y muéstralo.
4. Determina si el año de `fechaNacimiento` fue un año bisiesto, y muestra el resultado como `true` o `false`. No lo calcules a mano con la lógica de bisiestos (múltiplos de 4, excepciones de siglo, etc.) — investiga si `DateTime` ya trae algo preparado para esto exactamente, revisando con IntelliSense o la documentación oficial qué ofrece el tipo para resolverlo directamente.
5. Cambia el año de `fechaNacimiento` a uno que no sea bisiesto y comprueba que el resultado del punto anterior cambia en consecuencia.