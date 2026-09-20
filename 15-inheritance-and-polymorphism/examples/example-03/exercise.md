# Ejercicio 3 — `sealed`, colisión de nombres y modificadores de acceso

Este ejercicio no continúa directamente el inventario de los dos anteriores — usa clases nuevas, más pequeñas, para poder aislar cada mecanismo sin el ruido de la jerarquía completa.

## Parte A — `sealed`

1. Declara una clase `Smartphone` (independiente de la del Ejercicio 1, puede ser mucho más simple) y una clase `SmartphoneGamer : Smartphone`, marcando `SmartphoneGamer` como `sealed`.
2. Intenta crear una tercera clase que herede de `SmartphoneGamer`. Debe dar un error de compilación. Comenta esa declaración (o bórrala) y anota en un comentario el mensaje de error exacto que da el compilador.

## Parte B — el escenario real de `new`

Imagina que `Dispositivo` (la clase base, en una versión anterior) ya tenía una clase derivada, `RouterWifi`, con un método propio:

```csharp
public class Dispositivo
{
    public string Marca { get; set; }
}

public class RouterWifi : Dispositivo
{
    public void Reiniciar()
    {
        Console.WriteLine("Reiniciando el router...");
    }
}
```

Con el tiempo, quien mantiene `Dispositivo` añade un método nuevo, sin saber que `RouterWifi` ya tenía uno con el mismo nombre:

```csharp
public class Dispositivo
{
    public string Marca { get; set; }

    public void Reiniciar()
    {
        Console.WriteLine("Reiniciando dispositivo genérico...");
    }
}
```

3. Copia ambas clases tal cual (con el `Dispositivo` ya actualizado) y compílalas. Deberías ver la advertencia `CS0108` sobre `RouterWifi.Reiniciar()`. Captura o copia el texto exacto de la advertencia en un comentario.
4. Corrige la advertencia añadiendo `new` al `Reiniciar()` de `RouterWifi`, dejando constancia de que la ocultación es intencional.
5. Escribe un pequeño fragmento que demuestre la diferencia de comportamiento entre acceder a través de una variable `Dispositivo` y a través de una variable `RouterWifi`:

```csharp
Dispositivo comoDispositivo = new RouterWifi();
RouterWifi comoRouter = new RouterWifi();

comoDispositivo.Reiniciar(); // ¿qué imprime, y por qué?
comoRouter.Reiniciar();      // ¿qué imprime, y por qué?
```

Responde en un comentario, con tus propias palabras, por qué esas dos llamadas no imprimen lo mismo aunque el objeto real detrás sea el mismo en ambos casos.

## Parte C — `protected` e `internal`

6. Añade a `Dispositivo` un campo `NumeroSerie` (`string`) que solo la propia clase `Dispositivo` y sus derivadas puedan leer o modificar — nunca código externo a la jerarquía.
7. Declara una clase `ConfiguracionFabrica`, con el modificador de acceso adecuado para que solo sea visible dentro de este mismo proyecto, nunca desde otro proyecto que lo use como dependencia.