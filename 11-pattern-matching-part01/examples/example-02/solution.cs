Console.Write("Introduce un número entero: ");
int numeroIntroducido = int.Parse(Console.ReadLine()!);

if (numeroIntroducido is < 1 or > 100)
    Console.WriteLine("El número está fuera de rango");
else
    Console.WriteLine("El número está dentro de rango");



Console.Write("Introduce un texto: ");
string? texto = Console.ReadLine();
if (texto is null)
    Console.WriteLine("El texto es nulo");
if (texto is not null)
    if (texto is not "")
        Console.WriteLine("El texto no está vacío");
    else
        Console.WriteLine("El texto está vacío");
