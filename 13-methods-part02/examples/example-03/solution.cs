//Parte A
RegistrarEvento("Usuario @michel se ha conectado al sistema");
RegistrarEvento("La base de datos ha realizado una copia de seguridad", "SystemData");
RegistrarEvento("Administrador @kyle se ha conectado al sistema", mostrarHora: true);

static void RegistrarEvento(string mensaje, string nivel = "Info", bool mostrarHora = false)
{
    string hora = mostrarHora ? $"{DateTime.Now}:" : "";
    Console.WriteLine($"[{nivel}] {hora} {mensaje}");
}

//Parte B
Console.WriteLine(Promedio(2, 4));
Console.WriteLine(Promedio(10, 8, 9, 10, 8));
Console.WriteLine(Promedio());

static double Promedio(params double[] valores)
{
    if (valores.Length < 1)
        return 0;

    double total = 0;
    foreach (double valor in valores)
    {
        total += valor;
    }
    return total / valores.Length;
}