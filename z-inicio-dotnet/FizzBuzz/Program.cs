/*
REGLAS:
Valores de salida de 1 a 100, un número por línea, dentro del bloque de código de una instrucción de iteración.
Si el valor actual es divisible por 3, se imprime el término Fizz junto al número.
Si el valor actual es divisible por 5, se imprime el término Buzz junto al número.
Si el valor actual es divisible por 3 y por 5, se imprime el término FizzBuzz junto al número.

ej salida: 
1
2
3 - Fizz
*/

for (int i = 1; i <= 100; i++)
{
    Console.Write($"{i}");
    if (i % 3 == 0 || i % 5 == 0)
    {
        Console.Write($" - ");

        if (i % 3 == 0)
            Console.Write($"Fizz");

        if (i % 5 == 0)
            Console.Write($"Buzz");
    }
    Console.WriteLine();
}

