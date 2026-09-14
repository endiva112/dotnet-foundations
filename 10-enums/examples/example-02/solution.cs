
Console.WriteLine("Escriba el nombre de uno de los 3 niveles de permisos");
Console.WriteLine("Opciones: ( Invitado | Usuario | Administrador )");
Console.Write("Escriba su opción: ");
string? opcion = Console.ReadLine();

//el enunciado me exime de controlar la excepcion, asumo que nunca sera nulo.
NivelAcceso nivelSeleccionado = Enum.Parse<NivelAcceso>(opcion!);

Console.WriteLine(nivelSeleccionado);
Console.WriteLine((int)nivelSeleccionado);

string mensaje = (NivelAcceso.Administrador == nivelSeleccionado) ? "Bienvenido, administrador" : "Bienvenido, empleado";
Console.WriteLine(mensaje);

enum NivelAcceso
{
    Invitado = 0,
    Usuario = 10,
    Administrador = 99
}