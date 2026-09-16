Calculadora miCalculadora = new Calculadora();

Console.WriteLine(miCalculadora.Sumar(1, 2));
Console.WriteLine(miCalculadora.Sumar(1.05, 2));
Console.WriteLine(miCalculadora.Sumar(1, 2, 3));

class Calculadora
{
    public int Sumar(int a, int b)
    {
        return a + b;
    }

    public double Sumar(double a, double b)
    {
        return a + b;
    }

    public int Sumar(int a, int b, int c)
    {
        return a + b + c;
    }

    /*
    public double Sumar(int a, int b)
    {
        return a + b;
    }
    //error CS0111: Type 'Calculadora' already defines a member called 'Sumar' with the same parameter types
    */
}