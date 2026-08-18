Console.WriteLine("Hello World!");

// comentarios

Console.Write("mensaje sin '\n' al final de la linea");


/*
comntarios
multi-linea
*/

//varaibles

int numero = 10;
string nombre;
nombre = "Juan";

//variables de tipo implicito, no me agradan, pero se pueden usar con la palabra reservada var
//las varaibles de tipo implicito, una vez que se les asigna un valor, no se puede cambiar el tipo de dato, 
// es decir, si se le asigna un string, no se le puede asignar un int
var numero2 = 20;
var nombre2 = "Pedro"; 

//secuencias de escape
Console.WriteLine("Hello\nWorld!"); //salto de linea
Console.WriteLine("Hello\tWorld!"); //tabulacion
Console.WriteLine("Hello \"World\"!"); //comillas dobles
Console.WriteLine("c:\\source\\repos"); //barra invertida

//literal de cadena textual
Console.WriteLine(@"    c:\source\repos    
        (this is where your code goes)");

//caracteres de escape unicode
// Kon'nichiwa World
Console.WriteLine("\u3053\u3093\u306B\u3061\u306F World!");

//Concatenación
string firstName = "Bob";
string greeting = "Hello";
Console.WriteLine(greeting + " " + firstName + "!");

//Interpolación de cadenas
string message = $"{greeting} {firstName}!";