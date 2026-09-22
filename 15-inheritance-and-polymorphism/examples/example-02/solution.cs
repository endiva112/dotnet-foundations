Dispositivo[] dispositivos = 
{
    new Portatil("Asus 5", 500m, 140), 
    new Portatil("Poco X3 Pro", 154.99m, 150), 
    new Smartphone("Poco X5", 254.99m, 200), 
    new Smartphone("Huawei 5", 350.99m, 250), 
    new SmartphoneGamer("Huawei 7", 450.99m, 350, 300)
};

foreach (Dispositivo dispositivo in dispositivos)
{
    dispositivo.MostrarFicha();
    dispositivo.Encender();
    Console.WriteLine("- - -"); //Mejorar la lectura de la salida.
}

foreach (Dispositivo dispositivo in dispositivos)
{
    if (dispositivo is Smartphone)
    {
        Smartphone aux = (Smartphone)dispositivo;
        aux.TomarFoto();
    }        
}

//Compila, pero lanza una excepción en tiempo de ejecución, ya que el primer elemento del array es un Portatil y no un Smartphone.
Smartphone intentoCasteo = (Smartphone)dispositivos[0]; // Este elemento es un Portatil
//Unhandled exception. System.InvalidCastException: Unable to cast object of type 'Portatil' to type 'Smartphone'

#region  Clases del Ejercicio 1
public abstract class Dispositivo
{
    public string Marca { get; protected set; }
    public decimal Precio { get; set; } 

    protected Dispositivo(string marca, decimal precio)
    {
        Marca = marca;
        Precio = precio;
    }

    public abstract void Encender();

    public void MostrarFicha()
    {
        Console.WriteLine($"Marca: {Marca} \nPrecio: {Precio} €");
    }
}

public class Portatil : Dispositivo
{
    public double TamanioPantalla { get; set; }

    public Portatil(string marca, decimal precio, double tamanioPantalla) : base(marca, precio)
    {
        TamanioPantalla = tamanioPantalla;
    }

    public override void Encender()
    {
        Console.WriteLine("Cargando Linux OS...");
    }
}

public class Smartphone : Dispositivo
{
    public int CapacidadAlmacenamiento { get; set; }

    public Smartphone(string marca, decimal precio, int capacidadAlmacenamiento) : base(marca, precio)
    {
        CapacidadAlmacenamiento = capacidadAlmacenamiento;
    }

    public override void Encender()
    {
        Console.WriteLine("Lanzando Android OS, espere unos segundos...");
    }

    public void TomarFoto()
    {
        Console.WriteLine("Foto tomada y guardada en la galería!");
    }

    public override string ToString()
    {
        return $"Smartphone {Marca} ({CapacidadAlmacenamiento} GB)";
    }
}

public class SmartphoneGamer : Smartphone
{
    public int TasaRefresco { get; set; }

    public SmartphoneGamer(string marca, decimal precio, int capacidadAlmacenamiento, int tasaRefresco) 
        : base(marca, precio, capacidadAlmacenamiento)
    {
        TasaRefresco = tasaRefresco;
    }

    public override void Encender()
    {
        base.Encender();
        Console.WriteLine("Cargando GameSense, el acelerador de apps de videojuegos");
    }
}
#endregion