//Declaración de variables
int edad = 20;
bool llevaEntrada = true;

//Estructura de control if-else para determinar el acceso al evento
if (edad < 18) {
    Console.WriteLine("Acceso denegado: eres menor de edad.");
} else if (edad >= 65) {
    Console.WriteLine("Acceso gratuito para mayores de 65.");
} else if (!llevaEntrada) { //edad >= 18 && edad < 65 seria redundante para este punto, porque ya se sabe que la edad es >= 18 y < 65, pero no por ello seria incorrecto dejarlo
    Console.WriteLine("Acceso denegado: no llevas entrada.");
} else {
    Console.WriteLine("Bienvenido, disfruta del evento.");
}