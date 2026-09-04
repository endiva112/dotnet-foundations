# Ejercicio 01 - Ficha de contacto

Se te pide construir y mostrar una ficha de contacto sencilla.

Datos de partida:

```csharp
string nombre = "Ana";
string apellido = "García";
string ciudad = "Sevilla";
string rutaFoto = "C:\Users\Ana\Fotos\perfil.jpg"; // ojo con esta línea
```

Requisitos:

1. Construye el nombre completo (`nombre` + `apellido`) de dos formas distintas: una usando el operador `+` y otra usando interpolación (`$"..."`). Muestra ambas por separado y comprueba que el resultado es el mismo.
2. La variable `rutaFoto` tal y como está escrita arriba no va a compilar, o no va a dar el resultado esperado. Corrígela usando un string verbatim (`@"..."`).
3. Muestra un mensaje de varias líneas usando `\n`, con este formato (usa `\"` donde haga falta para las comillas):

```
Ficha de contacto:
Nombre: "Ana García"
Ciudad: Sevilla
```

4. Declara dos variables `string` con el mismo contenido (por ejemplo, dos veces `"Sevilla"`) y compáralas con `==`. Muestra por consola si son iguales o no.

No hace falta usar `Length` ni indexado todavía, eso se ve en el siguiente ejercicio.