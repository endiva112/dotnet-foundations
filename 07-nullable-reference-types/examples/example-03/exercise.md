# Ejercicio 03 - Validación previa

Se te pide simular una situación donde ya se ha validado un dato en un punto del programa, y hace falta usarlo después sin volver a comprobarlo.

Requisitos:

1. Crea un método `string? ObtenerConfiguracion()` que siempre devuelva un string con contenido (nunca `null` en la práctica), pero cuya firma sea `string?` porque en teoría podría no encontrar la configuración.
2. Llama al método y, sin usar `if` ni `??`, usa el operador `!` para acceder directamente a `.Length` sobre el resultado, dejando claro con un comentario por qué en este caso concreto es razonable confiar en que no será `null`.
3. Ahora fuerza el caso contrario: cambia temporalmente el método para que devuelva `null`, ejecuta el programa, y observa qué excepción se lanza a pesar de haber usado `!` (para comprobar que `!` no protege nada en runtime).
4. Deja el método devolviendo un valor real de nuevo antes de terminar, y añade un comentario explicando en una línea la diferencia entre el `!` de este ejercicio y el `!` de negación lógica (tema 02).