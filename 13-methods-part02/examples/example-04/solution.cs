//Parte 1
CuentaAtras(3);
CuentaAtras(10);

static void CuentaAtras(int desde)
{
    // Caso base < 1, asegura que al llegar a 0, se muestre el mensaje y evita los números negativos, garantizando que la recursión termine.
    if (desde < 1)
    {
        Console.WriteLine("¡Despegue!");
        return;
    }
    Console.WriteLine(desde);
    CuentaAtras(desde - 1);
}

//Parte 2
Console.WriteLine(SumaDeDigitos(1234));
Console.WriteLine(SumaDeDigitos(912873));

static int SumaDeDigitos(int numero)
{
    // Caso base <= 9, asegura que al llegar a un solo dígito, se retorne ese dígito y evita la recursión infinita, garantizando que la recursión termine.
    if (numero <= 9)
    {
        return numero;
    }
    return numero % 10 + SumaDeDigitos(numero / 10);
}