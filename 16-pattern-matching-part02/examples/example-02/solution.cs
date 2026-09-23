
Pedido[] pedidos = 
{
    new Pedido(new Portatil("Asus", 450m, 10), EstadoPedido.Cancelado, new DateTime(2026, 9, 22)),
    new Pedido(new Smartphone("Poco", 150m, 250), EstadoPedido.Pendiente, new DateTime(2026, 9, 10)),
    new Pedido(new Portatil("Lenovo", 250m, 12), EstadoPedido.Enviado, new DateTime(2026, 9, 16)),
    new Pedido(new SmartphoneGamer("Huawei", 550m, 1000, 120), EstadoPedido.Enviado, new DateTime(2026, 9, 22)),
    new Pedido(new SmartphoneGamer("Apple", 950m, 1000, 320), EstadoPedido.Entregado, new DateTime(2026, 9, 10)),
};

foreach (var pedido in pedidos)
{
    Console.WriteLine(AccionDelPedido(pedido));   
}

string AccionDelPedido(Pedido pedido) => pedido switch
{
    Pedido { Estado: EstadoPedido.Cancelado } => "Sin acción — pedido cancelado",
    Pedido { Estado: EstadoPedido.Pendiente, Producto: SmartphoneGamer} => "Priorizar preparación — producto de alta demanda",
    Pedido { Estado: EstadoPedido.Pendiente } => "Preparar pedido",
    //Aqui hay que usar when, las expresiones solo funcionan con constantes y aqui necesitamos calcular un valor en tiempo de ejecucion
    Pedido { Estado: EstadoPedido.Enviado } when pedido.FechaCreacion < DateTime.Now.AddDays(-5) => "Contactar con el transportista",
    Pedido { Estado: EstadoPedido.Enviado } => "En tránsito, sin acción",
    Pedido { Estado: EstadoPedido.Entregado } => "Solicitar valoración al cliente",
    _ => "Estado desconocido"
};

public enum EstadoPedido
{
    Pendiente,
    Enviado,
    Entregado,
    Cancelado
}

public class Pedido
{
    public Dispositivo Producto { get; set; }
    public EstadoPedido Estado { get; set; }
    public DateTime FechaCreacion { get; set; }

    public Pedido(Dispositivo producto, EstadoPedido estado, DateTime fechaCreacion)
    {
        Producto = producto;
        Estado = estado;
        FechaCreacion = fechaCreacion;
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