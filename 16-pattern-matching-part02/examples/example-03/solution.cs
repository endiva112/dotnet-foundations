Dispositivo[] dispositivos = 
{
    new Portatil("Asus", 250m, 10), 
    new Portatil("Mac", 400m, 16), 
    new Smartphone("Poco", 100m, 100), 
    new Smartphone("Xiaomi", 200m, 250), 
    new SmartphoneGamer("Apple", 1000m, 500, 280), 
    new Gameboy("Gameboy", 600m)
};

foreach (var dispositivo in dispositivos)
{
    ObtenerInstrucciones(dispositivo);
}

void ObtenerInstrucciones(Dispositivo dispositivo)
{
    switch (dispositivo)
    {
        case Portatil { TamanioPantalla: < 13 }:
            Console.WriteLine("Item etiquetado como \"ultraportátil\"");
            break;
        case Portatil:
            Console.WriteLine("Item etiquetado como \"estándar\"");
            break;
        case SmartphoneGamer:
            Console.WriteLine("Revisar refrigeración antes de almacenarlo");
            break;
        case Smartphone:
            Console.WriteLine("Almacenar");
            break;
        default:
            Console.WriteLine("Revisar el dispositivo manualmente");
            break;
    }
}

//Clase creada para que entre en la opcion default de mi programa.
public class Gameboy : Dispositivo
{
    public Gameboy(string marca, decimal precio) : base(marca, precio) {}

    public override void Encender()
    {
        Console.WriteLine("Encendiendo la diversión!");
    }
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