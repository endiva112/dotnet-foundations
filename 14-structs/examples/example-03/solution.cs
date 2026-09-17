Rango rango = new Rango();
Console.WriteLine($"Valor del mínimo: {rango.Minimo}");
Console.WriteLine($"Valor del máximo: {rango.Maximo}");

Rango b = default;
Console.WriteLine($"Valor del mínimo: {b.Minimo}");
Console.WriteLine($"Valor del máximo: {b.Maximo}");

//default nunca ejecuta ningún constructor, solo pone cada campo a su valor por defecto. 0 para int, null para string, false para bool, etc

struct Rango
{
    public int Minimo { get; set; }
    public int Maximo { get; set; }

    public Rango(int minimo, int maximo)
    {
        Minimo = minimo;
        Maximo = maximo;
    }

    public Rango()
    {
        Minimo = 0;
        Maximo = 100;
    }
}