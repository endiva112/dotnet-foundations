# 07 - Nullable reference types

## Qué es null

Hay ocasiones en las que una variable no ha recibido ningún valor — por ejemplo, porque todavía no se conoce el dato, porque una búsqueda no encontró resultado, o porque un campo es opcional. Para representar esa ausencia de valor existe `null`.

```csharp
string nombre = null;
Console.WriteLine(nombre.Length); // NullReferenceException en runtime
```

El riesgo de `null` es que, si no se comprueba antes de usar la variable, el programa falla en tiempo de ejecución con una `NullReferenceException`. Tradicionalmente, el compilador no distinguía entre una variable que siempre debía tener valor y una que podía no tenerlo — ambas se declaraban igual, y el error solo se descubría al ejecutar. **Nullable reference types (NRT)**, desde C# 8, permite marcar esa diferencia y que el compilador avise antes de llegar a ese punto.

## Diferencias entre tipos de nulos

No todos los tipos se comportan igual frente a `null`, y conviene tenerlo claro antes de seguir.

**Los tipos por valor** (`int`, `double`, `bool`...) no admiten `null` de forma nativa — no hay "ausencia de valor" en un `int`, siempre ocupa su espacio con algún contenido. Existe también `struct`, otra categoría de tipo por valor que se ve en su propio tema más adelante; de momento basta con saber que se comporta igual que `int` o `double` en todo lo de esta sección — no hace falta entender qué es todavía. Para que un tipo por valor admita `null` hace falta `Nullable<T>`, con `int?` como azúcar sintáctico para `Nullable<int>`: otro `struct`, este ya provisto por C#, que envuelve el valor original junto con un flag de si está presente o no. Esto no es parte de NRT, existe desde mucho antes; funciona igual para `int?`, `double?`, `bool?` o cualquier otro tipo por valor.

Esto significa que el compilador **no deja tratar un `int?` como un `int` normal sin decidir explícitamente qué pasa si es null**:

```csharp
int? edad = null;

int valorReal = edad;        // no compila (error, no warning)
int valorReal = edad.Value;  // compila, pero lanza InvalidOperationException en runtime si edad es null
int valorReal = edad ?? 0;   // forma correcta: da un valor por defecto si es null
```

Si se accede mal, el programa lanza una excepción real en runtime — no es una advertencia que se pueda ignorar y seguir adelante.

**Los tipos por referencia** (`string`, un array, cualquier `class`...) sí pueden ser `null` de forma nativa, porque una referencia simplemente puede no apuntar a ningún objeto. Aquí es donde entra NRT: no cambia si pueden ser null (siempre han podido), sino que añade un análisis del compilador para avisar cuándo ese `null` no está siendo comprobado antes de usarse. No hay ningún wrapper de por medio — `string?` y `string` son exactamente el mismo tipo en tiempo de ejecución, mismo código generado, cero diferencia real:

```csharp
string? nombre = null;
string nombreSeguro = nombre;            // warning, no error — compila igual
Console.WriteLine(nombreSeguro.Length);  // NullReferenceException en runtime, si no se corrigió el warning
```

Esto también aplica a un array (`int[]?`) o a cualquier objeto de una clase propia (`Cliente?`): todos son tipos por referencia, así que el análisis de NRT funciona igual en los tres casos.

La diferencia práctica es la que importa: con un tipo por valor nullable (`int?`), el compilador obliga a desenvolver el valor de alguna forma; es imposible olvidarse, porque de lo contrario no compila o revienta en runtime sin excepción posible de evitar. Con un tipo por referencia nullable (`string?`, `Cliente?`...), el compilador solo avisa — el programa compila y corre igual si se ignora el warning, se silencia el análisis, o se usa `!` para saltárselo. NRT no elimina el riesgo de `NullReferenceException`, solo lo hace visible antes de que llegue a producción. Sigue siendo responsabilidad de quien programa actuar sobre el aviso.

## La anotación de nulabilidad

Saber qué variables pueden faltar y cuáles no es información valiosa para cualquiera que lea o mantenga el código — incluido quien lo escribió, unos meses después. El símbolo `?` en un tipo por referencia se llama **anotación de nulabilidad**, y sirve para declarar esa intención directamente en el código, en vez de dejarla como algo que solo está en la cabeza de quien lo escribió:

```csharp
string nombre = "Ana";   // declara la intención: nombre nunca debería ser null
string? apodo = null;    // declara la intención: apodo puede no tener valor, y quien lo use debe contarlo
```

Ambas líneas de abajo funcionan exactamente igual en tiempo de ejecución — ninguna cambia cómo corre el programa:

```csharp
nombre = null; // rompe la intención declarada arriba → warning
apodo = null;  // es justo lo que se dijo que podía pasar → sin warning
```

La utilidad real es esta: si en algún punto del código se contradice la intención que se declaró (asignar `null` a algo que se dijo que nunca lo sería, o usar sin comprobar algo que se dijo que podía serlo), el compilador lo señala con un warning antes de que el programa llegue a ejecutarse. No es un cambio de comportamiento — es una herramienta para cazar ese tipo de error mientras se escribe el código, en vez de descubrirlo cuando ya está en producción.

## Análisis de flujo (null-checking narrowing)

El compilador sigue el flujo del código dentro del método y "recuerda" cuándo ya se ha comprobado que algo no es null:

```csharp
string? entrada = ObtenerEntrada();

if (entrada != null)
{
    Console.WriteLine(entrada.Length); // sin warning: el compilador sabe que aquí entrada no es null
}

Console.WriteLine(entrada.Length); // warning: fuera del if, ya no hay esa garantía
```

Este análisis es local y bastante literal — reconoce patrones simples como el `if` de arriba, pero no sigue lógica que se mueva a otro método (eso se retoma más adelante, si hace falta, con atributos como `[NotNullWhen]`).

## Operadores relacionados

Estos operadores existen específicamente para trabajar con valores que pueden ser null, y cobran su sentido completo con NRT activado.

### `?.` — null-conditional

Accede a un miembro solo si el objeto no es null; si es null, toda la expresión se corta y da `null` en vez de lanzar excepción.

```csharp
string? nombre = null;
int? longitud = nombre?.Length; // null, en vez de reventar
```

### `??` — null-coalescing

Da un valor por defecto si la expresión de la izquierda es null.

```csharp
string? apodo = null;
string mostrar = apodo ?? "Sin apodo"; // "Sin apodo"
```

### `??=` — asignación null-coalescing

Asigna solo si la variable es actualmente null.

```csharp
string? apodo = null;
apodo ??= "Sin apodo"; // apodo pasa a ser "Sin apodo"
apodo ??= "Otro valor"; // ya no es null, no se reasigna
```

### `!` — null-forgiving

Le dice al compilador "confía en mí, sé que esto no es null aquí", silenciando el warning sin comprobar nada realmente. No cambia absolutamente nada en tiempo de ejecución — si te equivocas, la excepción llega igual.

```csharp
string? nombre = ObtenerNombreSeguro();
Console.WriteLine(nombre!.Length); // sin warning, pero sigue siendo responsabilidad tuya que sea correcto
```

Usar `!` con criterio: es apropiado cuando tienes información que el compilador no puede deducir (por ejemplo, ya lo validaste en otro punto del programa). Usarlo para simplemente silenciar warnings sin pensar es exactamente el hábito que NRT intenta evitar.

Ojo con este símbolo: es el mismo `!` que la negación lógica del tema 02 (`!activo`), pero significa algo completamente distinto según dónde se coloque. Delante de un `bool` es negación; pegado detrás de una expresión (`nombre!`) es null-forgiving. No es el mismo operador con dos usos — son dos operadores distintos que comparten símbolo, y el contexto (qué hay a la izquierda) es lo que determina cuál se está usando.

## Aplicado a métodos

Los parámetros y el tipo de retorno de un método también se anotan, y el análisis de flujo se aplica igual al llamar:

```csharp
static string Saludar(string? nombre)
{
    string nombreSeguro = nombre ?? "desconocido";
    return $"Hola, {nombreSeguro}";
}

static string? BuscarUsuario(int id)
{
    // puede devolver null si no se encuentra
    return id == 1 ? "Ana" : null;
}

string? resultado = BuscarUsuario(2);
Console.WriteLine(resultado.Length); // warning: resultado puede ser null aquí
```

## Activar y desactivar avisos relacionados con NRT

.NET da la opción de que el compilador comunique avisos relacionados con posibles nulos, o no.

Es una opción a nivel de proyecto, en el archivo `.csproj`:

```xml
<Nullable>enable</Nullable>
```

Los proyectos nuevos de .NET moderno lo traen activado por defecto. No hace falta profundizar en la estructura del `.csproj` todavía — eso se retoma cuando toque estructura de proyecto y la CLI de `dotnet` en más detalle.

Cuando se vea `out` en Métodos II, aparecerán algunos atributos (`[NotNullWhen]` y similares) que ayudan a este análisis en patrones como `TryParse` — no hace falta nada de eso todavía.