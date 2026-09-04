string nombre = "Ana";
string apellido = "García";
string ciudad = "Sevilla";
string rutaFoto = @"C:\Users\Ana\Fotos\perfil.jpg";

//Concatenación vs interpolación
Console.WriteLine("Nombre completo usando concatenación: " + nombre + " " + apellido);
Console.WriteLine($"Nombre completo usando interpolación: {nombre} {apellido}");

//Mensaje de varias líneas
Console.WriteLine($"Ficha de contacto:\nNombre: \"{nombre} {apellido}\"\nCiudad: {ciudad}\nRuta de la foto: {rutaFoto}");

//Comparación de cadenas, ejemplo simple
string cadena1 = "Sevilla";
string cadena2 = "Sevilla";

bool sonIguales = cadena1 == cadena2;
//No es la forma más elegante de comparar cadenas, pero es la más simple y directa. 
//En C# también se puede usar el método Equals() para comparar cadenas, que es más explícito y
// permite especificar opciones de comparación, como la sensibilidad a mayúsculas y minúsculas.
// Dicho metodo se verá más adelante.
Console.WriteLine(sonIguales);