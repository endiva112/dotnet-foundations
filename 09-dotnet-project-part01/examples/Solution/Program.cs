Semaforo semaforo = Semaforo.Rojo;
Console.WriteLine(semaforo);

/*
¿por qué `Program.cs` puede ver el `enum` declarado en `Semaforo.cs` sin necesitar nada más?
    > Esto ocurre debido a que ambos archivos están en el mismo proyecto, el .csproj se encarga 
    de compilar todos los archivos .cs en el mismo ensamblado, por lo que todos los tipos definidos en 
    esos archivos son accesibles entre sí sin necesidad de importar espacios de nombres adicionales.
*/