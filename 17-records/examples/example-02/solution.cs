
Publicacion libro = new Libro("Cien Años de Soledad", 1967, "Gabriel García Márquez");
Publicacion revista = new Revista("Cien Años de Soledad", 1967, 1);

Console.WriteLine(libro == revista); // Espero que devuelva false, porque aunque compartan el mismo Titulo y AnioPublicacion, son de tipos diferentes (Libro y Revista).

Libro copia = (Libro)libro with { AnioPublicacion = 1968 }; // Crea un nuevo libro con el mismo título y autor, pero con un año de publicación diferente.
Console.WriteLine(libro == copia); //si el año hubiese sido 1967, el resultado seria True.

record class Publicacion(string Titulo, int AnioPublicacion);
record class Libro(string Titulo, int AnioPublicacion, string Autor) : Publicacion(Titulo, AnioPublicacion);
record class Revista(string Titulo, int AnioPublicacion, int NumeroEdicion) : Publicacion(Titulo, AnioPublicacion);