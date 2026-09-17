Rango rango1 = new Rango(1, 10);
Rango rango2 = rango1;

rango1.Maximo = 20;
Console.WriteLine(rango1.Maximo);
Console.WriteLine(rango2.Maximo);

/*
if (rango1 == rango2)
    Console.WriteLine("Son iguales");
//error CS0019: Operator '==' cannot be applied to operands of type 'Rango' and 'Rango'
*/

if (rango1.Equals(rango2))
    Console.WriteLine("Son iguales");
else
    Console.WriteLine("Son distintos");

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