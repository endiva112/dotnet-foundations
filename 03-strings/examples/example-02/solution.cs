string nombreUsuario = "Marcos";

if (string.IsNullOrWhiteSpace(nombreUsuario)) 
{
    Console.WriteLine("Nombre de usuario inválido.");
}
else if (nombreUsuario.Length < 3)
{
    Console.WriteLine("El nombre es demasiado corto.");
}
else
{
    char primerCaracter = nombreUsuario[0];
    char ultimoCaracter = nombreUsuario[nombreUsuario.Length - 1];

    Console.WriteLine($"Usuario válido: {nombreUsuario} ({nombreUsuario.Length} caracteres). Empieza por {primerCaracter} y termina por {ultimoCaracter}.");
}