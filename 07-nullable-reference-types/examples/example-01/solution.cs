double? temperatura = null;

// 1 y 2. Podemos imprimir directamente un nullable.
// Si es null, Console.WriteLine no lanza excepción.
Console.WriteLine(temperatura);

// 3. .Value intenta obtener el double que hay dentro.
// Como temperatura es null, lanza InvalidOperationException.
// Console.WriteLine(temperatura.Value);

// 4. ?? proporciona 0.0 si temperatura es null.
double resultado = temperatura ?? 0.0;

Console.WriteLine($"Temperatura: {resultado}");

// 5. Ahora temperatura tiene un valor.
temperatura = 23.5;

resultado = temperatura ?? 0.0;

Console.WriteLine($"Temperatura: {resultado}");