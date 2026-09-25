
Publicacion[] publicaciones = new Publicacion[]
{
    new Libro("Cien Años de Soledad", 1967, "Gabriel García Márquez"),
    new Revista("National Geographic", 1985, 5),
    new Libro("El Amor en los Tiempos del Cólera", 1985, "Gabriel García Márquez"),
    new Revista("Time", 1995, 12),
    new Libro("La Sombra del Viento", 2001, "Carlos Ruiz Zafón"),
    new Revista("Vogue", 2020, 15)
};

string MostrarDescripcion(Publicacion publicacion) => publicacion switch
{
      Libro(var titulo, < 1990, _) => $"Libro clásico: {titulo}",
      Libro(var titulo, _, _) => $"Libro: {titulo}",
      Revista(var titulo, _, < 10) => $"Revista de tirada limitada: {titulo}",
      Revista(var titulo, _, _) => $"Revista: {titulo}",
      _ => "Publicación desconocida"
};

foreach (var publicacion in publicaciones)
{
    Console.WriteLine(MostrarDescripcion(publicacion));
}

#region Clases ejercicio 2
record class Publicacion(string Titulo, int AnioPublicacion);
record class Libro(string Titulo, int AnioPublicacion, string Autor) : Publicacion(Titulo, AnioPublicacion);
record class Revista(string Titulo, int AnioPublicacion, int NumeroEdicion) : Publicacion(Titulo, AnioPublicacion);
#endregion