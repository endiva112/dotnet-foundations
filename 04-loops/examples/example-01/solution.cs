//Parte A
int limite = 10;

int indice = 1;
int suma = 0;

while (indice <= limite)
{
    suma += indice;
    indice++;
}
Console.WriteLine($"Valor del resultado del bucle while: {suma}");

int suma2 = 0;
for (int i = 1; i <= 10; i++)
{
    suma2 += i;
}
Console.WriteLine($"Valor del resultado del bucle for: {suma2}");
Console.WriteLine();//Espacio para separar los resultados y tener mejor legibilidad

//Parte B
//Para este caso concreto los más lógico sería usar un bucle for ya que conozco el número de repeticiones y además hago uso de su contador
int numero = 7;

for (int i = 1; i <= 10; i++)
{
    Console.WriteLine($"{numero} x {i} = {numero * i}");
}
Console.WriteLine();

int indiceMultiplicacion = 1;
while (indiceMultiplicacion <= 10)
{
    Console.WriteLine($"{numero} x {indiceMultiplicacion} = {numero * indiceMultiplicacion}");
    indiceMultiplicacion++;
}