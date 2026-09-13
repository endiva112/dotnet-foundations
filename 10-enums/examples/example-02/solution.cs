Semaforo semaforo = Semaforo.Rojo;
switch (semaforo)
{
    case Semaforo.Rojo:
        Console.WriteLine("Detente");
        break;
    case Semaforo.Amarillo:
        Console.WriteLine("Prepárate");
        break;
    case Semaforo.Verde:
        Console.WriteLine("Avanza");
        break;
}
Console.WriteLine(semaforo);
Console.WriteLine((int)semaforo);

enum Semaforo
{
    Rojo,
    Amarillo,
    Verde
}