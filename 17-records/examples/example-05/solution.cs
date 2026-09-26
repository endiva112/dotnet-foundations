
Ejemplar e1 = new Ejemplar("123456789", true);
Ejemplar e2 = new Ejemplar("123456789", true);
//e1 == e2; // Esto no compila,  error CS0019: Operator '==' cannot be applied to operands of type 'Ejemplar' and 'Ejemplar'
Console.WriteLine(e1 == e2); // Esto compila y devuelve true


//Como funcionaria si hubiese un record:
EjemplarRecord er1 = new EjemplarRecord("123456789", true);
EjemplarRecord er2 = new EjemplarRecord("123456789", true);
Console.WriteLine(er1 == er2); // Esto compila y devuelve true, sin necesidad de sobrecargar el operador ==, ya que los records implementan la igualdad estructural por defecto.


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

//Lo mismo si fuese un record:
record EjemplarRecord(string CodigoBarras, bool Disponible);
