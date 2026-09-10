string[] empleados = { "Ana", "Luis", "Marta" };

string? BuscarEmpleado(string[] nombres, string buscado)
{
    foreach (string nombre in nombres)
    {
        if (nombre == buscado) return nombre;
    }
    return null;
}

//Punto 3
void MostrarMensaje(string? nombre)
{
    if (nombre != null)
    {
        Console.WriteLine($"Encontrado: {nombre}.");
        Console.WriteLine($"Longitud del nombre: {nombre.Length}");
    }
    else
    {
        Console.WriteLine("No se encontró a nadie con ese nombre.");
    }
}

MostrarMensaje(BuscarEmpleado(empleados, "Ana"));
MostrarMensaje(BuscarEmpleado(empleados, "Pepe"));

//Punto 4
var apodoAMostrar = BuscarEmpleado(empleados, "Pepe") ?? "Sin apodo";

//Punto 5
string? ciudad = "Sevilla";
Console.WriteLine($"Ciudad antes: {ciudad}");

ciudad ??= "Desconocida";
Console.WriteLine($"Ciudad después: {ciudad}");

//Punto 6
string? telefono = null;
int? longitud = telefono?.Length;
Console.WriteLine($"Longitud del teléfono: {longitud}");