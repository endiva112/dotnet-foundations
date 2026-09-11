//Mi array de tiradas
int[] tiradas = new int[3];

//Asiganación de valores para mi array
Random dado = new Random();
for (int i = 0; i < tiradas.Length; i++)
{
    tiradas[i] = dado.Next(1, 7); 
}

//Mostrar los datos
for (int i = 0; i < tiradas.Length; i++)
{
    Console.WriteLine($"Tirada nº {i} -> {tiradas[i]}");
}

//Valor más alto
Array.Sort(tiradas);
Console.WriteLine($"El valor más alto de las 3 tiradas ha sido el: {tiradas[tiradas.Length - 1]}");//Acceder dinámicamente a la última posición

//Diferencia en valor absoluto entre tirada más alta y más baja.
int diferencia = Math.Abs(tiradas[0] - tiradas[tiradas.Length - 1]);
Console.WriteLine($"La diferencia entre la tirada más alta y la más baja es de: {diferencia}");

//Decimal aleatorio
double probabilidad = dado.NextDouble() * 100;
Console.WriteLine($"Probabilidad de ganar el premio gordo: {Math.Round(probabilidad, 1)} %");
