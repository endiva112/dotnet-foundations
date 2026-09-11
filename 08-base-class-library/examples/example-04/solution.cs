DateTime fechaNacimiento = new DateTime(2000, 2, 29);

Console.WriteLine(fechaNacimiento);

//Diferencia entre entonces y hoy
DateTime fechaActual = DateTime.Now;
TimeSpan diferencia = fechaActual - fechaNacimiento;
Console.WriteLine($"Desde la fecha de nacimiento hasta ahora han pasado {diferencia.Days} días");

//Años a los que equivale
int anyos = diferencia.Days / 365;
Console.WriteLine($"Esto equivale a {anyos} años");

//Año bisiesto usando el método IsLeapYear
Console.WriteLine($"El año de nacimiento fue bisiesto: {DateTime.IsLeapYear(fechaNacimiento.Year)}");

//Cambiar año
fechaNacimiento = new DateTime(2001, 2, 28);
Console.WriteLine(fechaNacimiento);