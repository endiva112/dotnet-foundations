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

//Combinar literales textuales y interpolación de cadenas
//En este ejemplo, el $ símbolo permite hacer referencia a la projectName variable dentro de las llaves, mientras que el @ símbolo permite usar el carácter sin \ escape.
string projectName = "First-Project";
Console.WriteLine($@"C:\Output\{projectName}\Data");


/* Cual de estos 3 no da error de compilacion?

directory = directory + "\"; -> esto es una cadena de texto, por lo que necesitaria ser \\ para que no falle
directory = directory + '\'; -> esto es un caracter, por lo que no se puede concatenar con una cadena de texto
directory = directory + @"\"; -> para eso sirve el @, para que no se interprete como un caracter de escape, sino como una cadena de texto
*/



//NUMEROS
int sum = 7 + 5;
int difference = 7 - 5;
int product = 7 * 5;
int quotient = 7 / 5;

Console.WriteLine("Sum: " + sum);
Console.WriteLine("Difference: " + difference);
Console.WriteLine("Product: " + product);
Console.WriteLine("Quotient: " + quotient);
//Cuando se dividen dos enteros, el resultado es un entero. Si se desea obtener un resultado decimal, al menos uno de los operandos debe ser un número decimal.
//Los numeros tienden a truncarse, es decir, si se hace una division de 7/5, el resultado es 1, ya que el resultado es un entero, y no un decimal. 
// Para obtener un resultado decimal, al menos uno de los operandos debe ser un número decimal.
decimal decimalQuotient = 7.0m / 5;
Console.WriteLine($"Decimal quotient: {decimalQuotient}");


// recuerda el orden: f flotante, d double, m decimal, l long, u unsigned, etc.

//Modulo
Console.WriteLine($"Modulus of 200 / 5 : {200 % 5}");

//C# sigue el mismo orden que PEMDAS, excepto en el caso de los exponentes. 
// Aunque no hay ningún operador exponencial en C#, puede usar el método System.Math.Pow. 
// En el módulo "Llamada a métodos de la biblioteca de clases .NET mediante C#" se presenta este método y otros.
// PEMDAS = Paréntesis, Exponentes, Multiplicación y División (de izquierda a derecha), Adición y Sustracción (de izquierda a derecha)

//Operadores de incremento y decremento
int x = 5;
x++; // Incrementa x en 1
Console.WriteLine($"Incremented x: {x}"); // Muestra 6
x--; // Decrementa x en 1
Console.WriteLine($"Decremented x: {x}"); // Muestra 5


//Si se desea escribir el operador de incremento o decremento en la misma línea que otra operación, es recomendable usar paréntesis para mejorar la 
// legibilidad y evitar confusiones. Se debe priorizar la legibilidad del código sobre la brevedad. Se escribe 1 vez pero se lee muchas veces.

//Random
Random dice = new Random();
int roll = dice.Next(1, 7);
Console.WriteLine(roll);

//el 7 no esta incluido, pero el 1 si.


//Metodos
//Hay 2 tipos. Estaticos y de instancia. Los metodos estaticos se llaman desde la clase, mientras que los metodos de instancia se llaman desde una instancia de la clase.
//En C# una variable de la clase se conoce como un campo, mientras que una variable de una instancia de la clase se conoce como una propiedad.

//Crear instancia de una clase
Random dice = new Random(); //se requiere usar el operador "new". new reserva espacio en memoria, crea el objeto y lo almacena en dicha memoria
// Y devuele la posicion de memoria donde se encuentra el objeto. En este caso, la variable dice almacena la posicion de memoria donde se encuentra el objeto Random.

//Tambien se puede declarar de la siguiente manera:
Random dice = new();

//simplifica la legibilidad del código

//A menudo, los términos 'parameter' y 'argument' se usan indistintamente. Sin embargo, "parámetro" hace 
//referencia a la variable que se usa dentro del método. El "argumento" es el valor que se pasa cuando se llama al método.

//Métodos sobrecargados
dice.Next(); //devuelve un numero aleatorio entre 0 y el maximo valor de un entero


int firstValue = 500;
int secondValue = 600;
int largerValue;

largerValue = System.Math.Max(firstValue, secondValue); //devuelve el valor mayor entre los 2 valores

Console.WriteLine(largerValue);



//Logica de control
/*
if, else y else if
if (message.Contains("fox"))
{
    Console.WriteLine("What does the fox say?");
}
*/

//Operadores de comparación
/*
==, el operador "igual que" para probar la igualdad
>, el operador "mayor que" para probar si el valor a la izquierda es mayor que el valor a la derecha
<, el operador "menor que" para probar si el valor a la izquierda es menor que el valor a la derecha
>=, el operador "mayor o igual que"
<=, el operador "menor o igual que"
*/

//Operadores lógicos
// &&, el operador "y" para probar si ambas condiciones son verdaderas
// ||, el operador "o" para probar si al menos una de las condiciones es verdadera


//Matrices aka arrays
//Una matriz es una colección de elementos del MISMO tipo.
//Ejemplo declaracion de matriz:
string[] fraudulentOrderIDs = new string[3];
//string[] fraudulentOrderIDs = [ "A123", "B456", "C789" ];
//o
/*
fraudulentOrderIDs[0] = "A123";
fraudulentOrderIDs[1] = "B456";
fraudulentOrderIDs[2] = "C789";
*/

//foreach
/*
string[] names = { "Rowena", "Robin", "Bao" };
foreach (string name in names)
{
    Console.WriteLine(name);
}
*/

//AQUI tampoco se ha creado temario para 01b-nullable-reference-types, se debe cuestionar
