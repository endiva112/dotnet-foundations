// Cambiar este valor para simular distintas elecciones
int opcionSimulada = 1;

do 
{
    Console.Write(" 1. Saludar! \n 2. Mostrar fecha \n 3. Salir \n");
    switch (opcionSimulada)
    {
        case 1:
            Console.WriteLine("¡Hola! ¿Cómo estás?");
            break;
        case 2:
            Console.WriteLine("Hoy es 4 de septiembre");
            break;
        case 3:
            Console.WriteLine("Saliendo...");
            break;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
} while (opcionSimulada != 3);