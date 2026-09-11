using System.Text;

int[] puntuaciones = { 42, 17, 89, 3, 56 };

int[] copiaPuntuaciones = new int[puntuaciones.Length];
Array.Copy(puntuaciones, copiaPuntuaciones, puntuaciones.Length);

Array.Sort(copiaPuntuaciones);
foreach (int puntuacion in copiaPuntuaciones)
{
    Console.WriteLine(puntuacion);
}

Array.Reverse(copiaPuntuaciones);
foreach (int puntuacion in copiaPuntuaciones)
{
    Console.WriteLine(puntuacion);
}

int posicionConcreta = Array.IndexOf(copiaPuntuaciones, 56);
Console.WriteLine($"Indice de posición del nº 56: {posicionConcreta}");

Console.WriteLine("Array original");
foreach (int puntuacion in puntuaciones)
{
    Console.WriteLine(puntuacion);
}