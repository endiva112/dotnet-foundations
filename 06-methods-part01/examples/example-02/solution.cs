int[] notas = { 6, 8, 4, 9, 7 };

Console.WriteLine(DescribirNotaMaxima(notas));
Console.WriteLine(CalcularMaxima(notas)); // Muestra el número puro para comprobar la reutilización del método

//Mis métodos
static int CalcularMaxima(int[] valores)
{
    int maximo = valores[0];
    for (int i = 1; i < valores.Length; i++)
    {
        if (valores[i] > maximo)
        {
            maximo = valores[i];
        }
    }
    return maximo;
}

static string DescribirNotaMaxima(int[] notas)
{
    int maxima = CalcularMaxima(notas);
    return $"La nota más alta es un {maxima}.";
}
