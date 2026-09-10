
string? ObtenerConfiguracion()
{
    return "cadena encontrada";
}

Console.WriteLine(ObtenerConfiguracion()!.Length);
/*
A pesar de que la firma del metodo sugiera que podría llegar a darse un nulo, tenemos la seguridad de que esto no va a pasar.
Esto es un caso simulado y un ejemplo algo pobre, pero el objetivo es el mismo, que el compilador no lance un warning, ya que este
no comprende la logica, solo ve que el metodo por su firma puede dar nulo (aunque sea imposible) y lo advierte.
Es por ello que usamos el operador !
*/

string? ObtenerConfiguracionNula()
{
    return null;
}
Console.WriteLine(ObtenerConfiguracionNula()!.Length);
//El resultado de esta linea es la excepción: System.NullReferenceException
//El operador ! solo silenció el warning del compilador, pero no impide que el programa falle si el valor realmente es null.

/*
La diferencia entre la negacion lógica y el null forgiving radica en si se pone a la derecha o izquierda del metodo o variable,
uno niega la expresion y el otro solo informa al compilador de que el desarrollador esta seguro de que el valor nunca será nulo.
*/