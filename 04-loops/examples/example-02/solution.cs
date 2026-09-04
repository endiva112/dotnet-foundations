



/*

```
1. Saludar
2. Mostrar fecha (de mentira, escribe un texto fijo tipo "Hoy es 4 de septiembre")
3. Salir
```

Como todavía no se ha visto lectura de entrada de teclado (eso se ve en el tema de librerías/métodos nativos), simula la elección del usuario con una variable que cambies a mano entre ejecuciones, por ejemplo:

```csharp
int opcionSimulada = 1; // cambia este valor para simular distintas elecciones
```

Requisitos:

1. Usa un `do-while` para que el menú se muestre al menos una vez.
2. Dentro del bucle, usa un `switch` clásico (tema 02) para reaccionar a `opcionSimulada`:
   - `1` → muestra un saludo.
   - `2` → muestra la fecha de mentira.
   - `3` → muestra `"Saliendo..."` y termina el bucle.
   - cualquier otro valor → muestra `"Opción no válida."`
3. La condición del `do-while` debe comprobar que la opción no ha sido `3` (salir).

Como no hay entrada real de teclado, para probarlo de verdad tendrás que cambiar `opcionSimulada` y volver 
a ejecutar el programa unas cuantas veces. No hace falta que el bucle repita infinitamente con el mismo valor: con una sola vuelta 
(mostrar menú, reaccionar a la opción, salir si toca) es suficiente para este ejercicio.
*/