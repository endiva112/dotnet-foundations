# Ejercicio 03 - Tabla de puntuaciones

Se te pide gestionar una pequeña tabla de puntuaciones de una partida.

Dato de partida:

```csharp
int[] puntuaciones = { 42, 17, 89, 3, 56 };
```

Requisitos:

1. Antes de modificar nada, crea una copia independiente de `puntuaciones` usando el método adecuado del tema, de forma que los cambios posteriores sobre una no afecten a la otra.
2. Sobre la copia (no sobre el array original), ordénala de menor a mayor y muéstrala.
3. Invierte el orden de esa misma copia (para que quede de mayor a menor) y muéstrala de nuevo.
4. Busca en la copia la posición donde se encuentra la puntuación `56`, y muestra en qué posición quedó tras los cambios anteriores.
5. Al final, muestra también el array `puntuaciones` original, y comprueba que sigue teniendo el orden con el que se declaró al principio, sin verse afectado por nada de lo que se hizo sobre la copia.