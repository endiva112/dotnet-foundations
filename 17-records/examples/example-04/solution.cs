
Posicion p1 = new Posicion(1, 2);
Posicion p2 = new Posicion(1, 2);
Console.WriteLine(p1 == p2); // Espero que devuelva true, porque aunque sean instancias distintas, sus valores son iguales.

Posicion[] posiciones =
{
    new Posicion(1, 2),
    new Posicion(3, 4),
    new Posicion(5, 6)
};

foreach (var posicion in posiciones)
{
    //posicion.Fila = 10; 
    // Esto da error de compilación, porque no se puede modificar la propiedad de un record struct dentro de un foreach.
    //error CS1654: Cannot modify members of 'posicion' because it is a 'foreach iteration variable'
    //posicion es una variable de iteración del foreach, y en C# las variables de iteración son inmutables. 
    //Esto significa que no se puede cambiar el valor de sus propiedades dentro del bucle. 
    //Si `Posicion` fuera una clase (reference type), la variable de iteración sería una referencia a un objeto, y 
    //podría modificar las propiedades del objeto al que apunta, pero no podrías cambiar la referencia en sí.
}

PosicionFija p01 = new PosicionFija(1, 2);
PosicionFija p02 = new PosicionFija(1, 2);
Console.WriteLine(p01 == p02);


record struct Posicion(int Fila, int Columna);// esto ha de declarase como record struct, porque es un tipo de valor, y queremos que se comporte como tal (igualdad por valor, etc).

readonly record struct PosicionFija(int Fila, int Columna);
