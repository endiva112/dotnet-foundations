CuentaBancaria miCuenta = new CuentaBancaria("John Doe", 2000);

miCuenta.Depositar(200);
Console.WriteLine(miCuenta.Saldo);
miCuenta.Retirar(3000);
Console.WriteLine(miCuenta.Saldo);
miCuenta.Retirar(1200);
Console.WriteLine(miCuenta.Saldo);

//miCuenta.Saldo = 99999;
//The property or indexer 'CuentaBancaria.Saldo' cannot be used in this context because the set accessor is inaccessible

class CuentaBancaria
{
    //Esto es un campo, almacena un valor
    private double saldo;
    public string Titular {get; set;}

    public CuentaBancaria(string titular, double saldo)
    {
        this.Titular = titular;
        this.saldo = saldo;
    }

    //Esto es una propiedad para un campo. permite acceder al campo
    public double Saldo
    {
        get { return saldo; }
        private set { saldo = value; }
    }

    public void Depositar(double cantidad)
    {
        if (cantidad >= 0) 
            saldo += cantidad;
    }

    public bool Retirar(double cantidad)
    {
        if (cantidad > 0)
        {
            double saldoAuxilar = saldo;
            saldoAuxilar -= cantidad;
            if (saldoAuxilar > 0)
            {
                saldo = saldoAuxilar;
                return true;  
            }
        }    
        return false;       
    }
}