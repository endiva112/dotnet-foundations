Estudiante estudiante1 = new Estudiante("Leonardo");
Estudiante estudiante2 = new Estudiante("Rafael");
Estudiante estudiante3 = new Estudiante("Michelangello");
Estudiante estudiante4 = new Estudiante("Donatello");

Console.WriteLine($"ID: {estudiante1.Id} - {estudiante1.Nombre}");
Console.WriteLine($"ID: {estudiante2.Id} - {estudiante2.Nombre}");
Console.WriteLine($"ID: {estudiante3.Id} - {estudiante3.Nombre}");
Console.WriteLine($"ID: {estudiante4.Id} - {estudiante4.Nombre}");
Console.WriteLine($"Total de estudiantes: {Estudiante.ObtenerTotalEstudiantes()}");


class Estudiante
{
    private string nombre;
    private int id;
    private static int contadorEstudiantes = 0;

    //Constructor
    public Estudiante(string nombre)
    {
        contadorEstudiantes++;
        Id = contadorEstudiantes;
        Nombre = nombre;
    }

    //Propiedades
    public string Nombre
    {
        get { return nombre; }
        private set { nombre = value; }
    }

    public int Id
    {
        get { return id; }
        private set { id = value; }
    }

    //Metodos
    public static int ObtenerTotalEstudiantes()
    {
        return contadorEstudiantes;
    }
}