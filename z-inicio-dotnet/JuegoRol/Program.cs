/*
REGLAS
Estas son las reglas para el juego de batalla que necesita implementar en el proyecto de código:

Debes usar la instrucción do-while o la instrucción while como un bucle de juego externo.
El héroe y el monstruo comenzarán con 10 puntos de salud.
Todos los ataques tendrán un valor comprendido entre 1 y 10.
El héroe ataca primero.
Imprima la cantidad de salud que ha perdido el monstruo y su salud restante.
Si la salud del monstruo es mayor que 0, puede atacar al héroe.
Imprima la cantidad de salud que ha perdido el héroe y su salud restante.
Continúe con esta secuencia de ataque hasta que la salud del monstruo o del héroe sea cero o menos.
Imprima el ganador.

ej salida:
Monster was damaged and lost 1 health and now has 9 health.
Hero was damaged and lost 1 health and now has 9 health.
Monster was damaged and lost 7 health and now has 2 health.
Hero was damaged and lost 6 health and now has 3 health.
Monster was damaged and lost 9 health and now has -7 health.
Hero wins!
*/

int saludHeroe = 10;
int saludMonstruo = 10;
Random dado = new Random();
int ataque;

while (saludHeroe > 0 && saludMonstruo > 0)
{
    ataque = dado.Next(1, 11);
    Console.WriteLine($"Monster was damaged and lost {ataque} health and now has {saludMonstruo -= ataque} health.");
    if (saludMonstruo <= 0) break;

    ataque = dado.Next(1, 11);
    Console.WriteLine($"Hero was damaged and lost {ataque} health and now has {saludHeroe -= ataque} health.");
};
Console.WriteLine(saludHeroe > saludMonstruo ? "Hero wins!" : "Monster wins!");

