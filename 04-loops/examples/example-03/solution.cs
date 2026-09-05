string palabra = "programacion";
int numeroVocales = 0;

foreach (char letra in palabra)
{
    if (letra == 'x')
    {
        break;
    }

    if (letra == ' ')
    {
        continue;
    }

    Console.WriteLine(letra);

    if (letra == 'a' || letra == 'e' || letra == 'i' || letra == 'o' || letra == 'u')
    {
        numeroVocales++;
    }
}
Console.WriteLine($"La palabra '{palabra}' tiene {numeroVocales} vocales y una longitud de {palabra.Length} caracteres.");