//Parte A
/*
1. `Coche` y `Motor`.
-> Depende del contexto, pero me decanto por Composicion, aunque Motor "es un" una parte de un coche, podrian existir coche sin motor, por ejemplo en un desguace. No creo que haya cualidades faciles de heredar sin importar si coche hereda de motor o al reves.

2. `Cuadrado` y `Rectangulo`.
-> Herencia. Este es el caso mas obvio, todos los cuadrados son rectangulos. Todo cuadrado "es un" rectangulo. No todos los rectangulos son cuadrados.

3. `Usuario` y `Suscripcion` (una suscripción que el usuario puede tener, renovar o cancelar).
-> Composicion, Una sucripcion no "es un" usuario ni viceversa, un usuario puede TENER una suscripcion, no creo que las clases compartan mucha logica para ser viable usar herencia.
*/

//Parte B
Usuario user = new Usuario();
Console.WriteLine(user);
Console.WriteLine(user.Suscripcion.Estado); //inactiva
user.RenovarSuscripcion();
Console.WriteLine(user.Suscripcion.Estado); //activa

public class Usuario
{
    public Suscripcion Suscripcion { get; set; } = new Suscripcion();

    public void RenovarSuscripcion()
    {
        Suscripcion.ActivarSuscripcion();
    }
}

public class Suscripcion
{
    public enum estadoSuscripcion
    {
        Activa,
        Inactiva,
    }

    public estadoSuscripcion Estado { get; private set; }

    public Suscripcion()//Toda suscripcion comenzaria como inactiva
    {
        Estado = estadoSuscripcion.Inactiva;
    }

    public void ActivarSuscripcion()
    {
        if (Estado == estadoSuscripcion.Inactiva)
            Estado = estadoSuscripcion.Activa;
    }
}