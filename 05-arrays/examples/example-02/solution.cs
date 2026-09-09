double[] temperaturas = { 5.5, 14.0, 22.3, 30.1, 18.7, 9.2, 25.0 };
int diasCalurosos = 0;

for (int i = 0; i < temperaturas.Length; i++)
{
    string sensacionTermica;
    if (temperaturas[i] < 10)
    {
        sensacionTermica = "Frío";
    } else if (temperaturas[i] < 25)
    {
        sensacionTermica = "Templado";
    } else
    {
        sensacionTermica = "Caluroso";
        diasCalurosos++;
    }
    Console.WriteLine($"Día {i}: {temperaturas[i]}º - {sensacionTermica}");
}
Console.WriteLine($"Número de días calurosos: {diasCalurosos}");