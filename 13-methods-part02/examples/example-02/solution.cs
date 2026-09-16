//Parte A
int valorPar = 2;
int valorImpar = 5;

Console.WriteLine(valorPar);
Console.WriteLine(valorImpar);

Intercambiar(ref valorPar, ref valorImpar);

Console.WriteLine(valorPar);
Console.WriteLine(valorImpar);

static void Intercambiar(ref int a, ref int b)
{
    int aux = a;
    a = b;
    b = aux;
}


//Parte B
MostrarMensajeRepartoCaramelos(11, 5);
MostrarMensajeRepartoCaramelos(10, 5);

static void MostrarMensajeRepartoCaramelos(int a, int b)
{
    RepartirCaramelos(a, b, out int porPersona, out int sobra);
    Console.WriteLine($"Cada niño toma {porPersona} caramelos y sobrarían {sobra} caramelos.");
}

static void RepartirCaramelos(int totalCaramelos, int totalNinos, out int caramelosPorNino, out int caramelosSobrantes)
{
    caramelosPorNino = totalCaramelos / totalNinos;
    caramelosSobrantes = totalCaramelos % totalNinos;
}

//Parte B punto 5
if (int.TryParse("abc", out int numero))
{
    Console.WriteLine($"Se convirtió correctamente: {numero}");
}
else
{
    Console.WriteLine("La entrada no era un número válido");
}