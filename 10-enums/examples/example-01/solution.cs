static void MostrarTalla(Talla talla)
{
    switch(talla)
    {
        case Talla.XS:
            Console.WriteLine("Muy pequeña");
            break;
        case Talla.S:
            Console.WriteLine("Pequeña");
            break;
        case Talla.M:
            Console.WriteLine("Mediana");
            break;
        case Talla.L:
            Console.WriteLine("Grande");
            break;
        case Talla.XL:
            Console.WriteLine("Muy grande");
            break;
    }
}

Talla miTalla = Talla.M;
MostrarTalla(miTalla);
Console.WriteLine(miTalla);
Console.WriteLine((int)miTalla);

miTalla = Talla.XL;
MostrarTalla(miTalla);
Console.WriteLine(miTalla);
Console.WriteLine((int)miTalla);

enum Talla
{
    XS,
    S,
    M,
    L,
    XL
}