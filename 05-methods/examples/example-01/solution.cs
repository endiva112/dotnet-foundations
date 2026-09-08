
//Metodo
static string Presentar(string nombre)
{
    return $"Este es {nombre}.";
}

//Sobrecarga 1
static string Presentar(string nombre, int edad)
{
    return $"Este es {nombre}, tiene {edad} años.";
}

//Sobrecarga 2
static string Presentar(string nombre, int edad, string raza)
{
    return $"Este es {nombre}, tiene {edad} años, es de raza {raza}.";
}

//Lammadas
Console.WriteLine(Presentar("Legolas"));
Console.WriteLine(Presentar("Gandalf", 24000));
Console.WriteLine(Presentar(raza: "hobbit", edad: 50, nombre: "Frodo"));
