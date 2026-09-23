//Demostracion parte B
Dispositivo aparato = new RouterWifi("LG");
RouterWifi aparato2 = new RouterWifi("TP-Link");

aparato.Reiniciar();
//Reiniciando dispositivo genérico...   --> Usa el metodo de la clase Dispositivo
aparato2.Reiniciar();
//Reiniciando el router...              --> usa el metodo de la clase RouterWifi

//Ambos son Objetos RouterWifi, pero cada uno está siendo apuntado por una variable diferente, por lo que el compilador ejecuta el metodo correspondiente
//para LA VARIABLE, siendo metodos completamente difrentes a pesar de llamarse igual.

//Parte A
public class Smartphone {}

public sealed class SmartphoneGamer : Smartphone {}

//public class HighPerformanceSmartPhone : SmartphoneGamer {}
//error CS0509: 'HighPerformanceSmartPhone': cannot derive from sealed type 'SmartphoneGamer'

//Parte B
public class Dispositivo
{
    public string Marca { get; set; }
    //Parte C
    protected string NumeroSerie { get; set; }

    public Dispositivo(string marca)
    {
        Marca = marca;
    }

    public void Reiniciar()
    {
        Console.WriteLine("Reiniciando dispositivo genérico...");
    }
}

public class RouterWifi : Dispositivo
{
    public RouterWifi(string marca) : base(marca) {}

    public new void Reiniciar()
    {
        Console.WriteLine("Reiniciando el router...");
    }
    //Sin usar el new da el siguiente warning:
    //warning CS0108: 'RouterWifi.Reiniciar()' hides inherited member 'Dispositivo.Reiniciar()'. Use the new keyword if hiding was intended.
}

//Parte C
internal class ConfiguracionFabrica
{
    //TODO - Agregar logica a la clase.
}