# Ejercicio 02 - Validador de nombre de usuario

Se te pide validar un nombre de usuario introducido por un supuesto formulario.

Dato de partida (cambia el valor para probar distintos casos):

```csharp
string nombreUsuario = "Marcos";
```

Requisitos:

1. Primero comprueba si `nombreUsuario` está vacío, es `null`, o son solo espacios en blanco, usando `string.IsNullOrWhiteSpace`. Si es así, muestra `"Nombre de usuario inválido."` y no sigas con el resto de comprobaciones.
2. Si el nombre es válido, comprueba su longitud con `Length`:
   - Si tiene menos de 3 caracteres, muestra `"El nombre es demasiado corto."`
   - Si tiene 3 caracteres o más, continúa al siguiente punto.
3. Muestra el primer y el último carácter del nombre de usuario por separado, accediendo por índice (nada de `Substring` ni métodos parecidos, eso se ve más adelante).
4. Muestra un resumen final con interpolación, por ejemplo:

```
Usuario válido: Marcos (6 caracteres). Empieza por M y termina por s.
```

Prueba el programa con al menos tres valores distintos de `nombreUsuario`: uno vacío o con espacios, uno demasiado corto, y uno válido.