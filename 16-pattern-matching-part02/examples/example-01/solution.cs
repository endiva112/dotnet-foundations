
Dispositivo[] dispositivos = 
{
    new Portatil("Asus", 250m, 10), 
    new Portatil("Mac", 400m, 16), 
    new Smartphone("Poco", 100m, 100), 
    new Smartphone("Xiaomi", 200m, 250), 
    new SmartphoneGamer("Apple", 1000m, 500, 280), 
    new SmartphoneGamer("Huawei", 600m, 500, 200)
};

string DescribirDispositivo(Dispositivo dispositivo) => dispositivo switch
{
    SmartphoneGamer a when a.TasaRefresco >= 240 => "Gaming de alta gama",
    SmartphoneGamer => "Gaming estándar",
    Smartphone b when b.CapacidadAlmacenamiento < 128 => "Smartphone con poco almacenamiento",
    Smartphone => "Smartphone estándar",
    Portatil c when c.TamanioPantalla > 15 => "Portátil grande",
    Portatil => "Portátil estándar",
    _ => "Dispositivo sin clasificar"
};

foreach (var dispositivo in dispositivos)
{
    Console.WriteLine(DescribirDispositivo(dispositivo));
}

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