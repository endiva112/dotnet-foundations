# Ejercicio 03 - Analizador de una palabra

Se te pide analizar una palabra carácter a carácter.

Dato de partida:

```csharp
string palabra = "programacion";
```

Requisitos:

1. Recorre `palabra` con `foreach`, mostrando cada carácter por separado.
2. Mientras recorres, cuenta cuántas vocales (a, e, i, o, u, sin tildes) contiene la palabra.
3. Si en algún momento encuentras la letra `'x'`, corta el recorrido inmediatamente con `break` (no hace falta seguir analizando el resto).
4. Si encuentras un espacio `' '`, sáltatelo con `continue` sin contarlo como vocal ni como nada (aunque `palabra` no tenga espacios ahora, el código debe contemplarlo por si acaso).
5. Al terminar, muestra el número total de vocales encontradas y la longitud total de la palabra (`Length`, tema 03) en un único mensaje interpolado.

Prueba el programa con al menos dos palabras distintas: una sin `'x'` y otra que sí la contenga, para comprobar que el `break` corta el recorrido donde toca.