//Llamadas
int suma = Sumar(5, 3);
int resta = Restar(5, 3);
int multiplicacion = Multiplicar(b: 5, a: 3);

static int Sumar(int a, int b) => a + b;
static int Restar(int a, int b) => a - b;
static int Multiplicar(int a, int b) => a * b;

Console.WriteLine($"Suma: {suma} \nResta: {resta} \nMultiplicación: {multiplicacion}");
