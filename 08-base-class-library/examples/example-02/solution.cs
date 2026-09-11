//Pedir datos
Console.Write("Introduzaca su nombre: ");
string? nombre = Console.ReadLine();

//Comprobar contenido
if (string.IsNullOrWhiteSpace(nombre))
{
    Console.WriteLine("ERROR: El nombre está vacío o es null");
} 
else
{
    nombre = nombre.Trim();
    Console.WriteLine($"Nombre en mayúsculas: {nombre.ToUpper()}");
    Console.WriteLine($"Nombre en minúsculas: {nombre.ToLower()}");

    string[] partesNombre = nombre.Split(" ");

    if (partesNombre.Length < 2)
    {
        Console.WriteLine("Solo se introdujo una palabra");
    }
    else
    {
        for (int i = 0; i < partesNombre.Length; i++)
        {
            Console.WriteLine($"Parte {i + 1}: {partesNombre[i]}");
        }
    }
}