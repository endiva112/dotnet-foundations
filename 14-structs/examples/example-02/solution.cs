Rango test = new Rango(1, 10);

//Por valor
Console.WriteLine($"Valor de rango max fuera del metodo: {test.Maximo}");
Ampliar(test, 10);
Console.WriteLine($"Valor de rango max fuera del metodo: {test.Maximo}");

static void Ampliar(Rango rango, int cantidad)
{
    rango.Maximo += cantidad;
    Console.WriteLine($"Valor de rango max dentro del metodo: {rango.Maximo}");
}

//Por referencia
Console.WriteLine($"Valor de rango max fuera del metodo: {test.Maximo}");
AmpliarPorReferencia(ref test, 10);
Console.WriteLine($"Valor de rango max fuera del metodo: {test.Maximo}");

static void AmpliarPorReferencia(ref Rango rango, int cantidad)
{
    rango.Maximo += cantidad;
    Console.WriteLine($"Valor de rango max dentro del metodo: {rango.Maximo}");
}

Rango[] rangos = {new Rango(1, 10), new Rango(11, 20), new Rango(21, 30)};

/*
foreach (var rango in rangos)
{
    rango.Maximo = 2;
}
// error CS1654: Cannot modify members of 'rango' because it is a 'foreach iteration variable'
*/

for (int i = 0; i < rangos.Length; i++)
{
    rangos[i].Maximo = 50;
    //modifico el maximo de todos los rangos
    //rangos[0].Maximo = 50; en caso de querer solo modificar 1 concreto, en este caso el primero.
}


//Struct ej 1
struct Rango
{
    public int Minimo { get; set; }
    public int Maximo { get; set; }

    public Rango(int minimo, int maximo)
    {
        Minimo = minimo;
        Maximo = maximo;
    }
}