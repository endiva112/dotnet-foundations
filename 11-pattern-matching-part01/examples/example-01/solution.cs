int edad = 36;
Console.WriteLine(obtenerCategoria(edad));


double temperatura = 30.5;
Console.WriteLine(obtenerSensacionTermica(temperatura));



static string ObtenerCategoria(int edad) => edad switch
{
    < 13 => "Niño",
    < 20 => "Adolescente",
    < 65 => "Adulto",
    _ => "Mayor"
};

static string ObtenerSensacionTermica(double temperatura) => temperatura switch
{
    < 10 => "Frío",
    < 25 => "Templado",
    _ => "Caluroso"
};