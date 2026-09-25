
Ejemplar e1 = new Ejemplar("123456789", true);
Ejemplar e2 = new Ejemplar("123456789", true);
//e1 == e2; // Esto no compila,  error CS0019: Operator '==' cannot be applied to operands of type 'Ejemplar' and 'Ejemplar'
Console.WriteLine(e1 == e2); // Esto compila y devuelve true

struct Ejemplar
{
    public string CodigoBarras { get; set; }
    public bool Disponible { get; set; }

    public Ejemplar(string codigoBarras, bool disponible)
    {
        CodigoBarras = codigoBarras;
        Disponible = disponible;
    }

    public static bool operator ==(Ejemplar e1, Ejemplar e2)
    {
        return e1.CodigoBarras == e2.CodigoBarras && e1.Disponible == e2.Disponible;
    }

    public static bool operator !=(Ejemplar e1, Ejemplar e2)
    {
        return !(e1 == e2);
    }
}