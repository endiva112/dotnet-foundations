//Linea que evita bugs raros a la hora de mostrar caracteres en mi terminal
//forzando el uso de UTF-8
Console.OutputEncoding = System.Text.Encoding.UTF8;

Portatil portatil = new Portatil("Asus 5", 500m, 140);
Smartphone telefono1 = new Smartphone("Poco X3 Pro", 154.99m, 150);
SmartphoneGamer telefono2 = new SmartphoneGamer("Huawei 5", 350.99m, 250, 120);


portatil.MostrarFicha();
portatil.Encender();
telefono1.MostrarFicha();
telefono1.Encender();
telefono2.MostrarFicha();
telefono2.Encender();


Console.WriteLine(telefono1);

//SmartphoneGamer utiliza la version de ToString de la clase de la que hereda, debido a que se propaga el Override, debido a ello, no usa una propia, pues no
//implementa su propio override NI usa el de la clase Dispositivo, porque como se ha dicho, ha quedado sobrescrita por la clase Smartphone que es de la que hereda.
Console.WriteLine(telefono2);

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

    public Portatil(string marca, decimal precio, double tamanioPantalla) : base (marca, precio)
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

    public Smartphone(string marca, decimal precio, int capacidadAlmacenamiento) : base (marca, precio)
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
        : base (marca, precio, capacidadAlmacenamiento)
    {
        TasaRefresco = tasaRefresco;
    }

    public override void Encender()
    {
        base.Encender();
        Console.WriteLine("Cargando GameSense, el acelerador de apps de videojuegos");
    }
}