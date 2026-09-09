int[] stock = new int[5];
stock[0] = 12;
stock[1] = 5;
stock[2] = 8;
stock[3] = 20;
stock[4] = 15;

stock[2] -= 1; // Simulando una venta del producto

for (int i = 0; i < stock.Length; i++)
{
    Console.WriteLine($"Producto {i}: {stock[i]} unidades");
}

foreach (int unidades in stock)
{
    Console.WriteLine($"Unidades: {unidades}");
}

Console.WriteLine($"Longitud del array: {stock.Length}");