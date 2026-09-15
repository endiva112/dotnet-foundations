Libro libro1 = new Libro("Fundamentos de Python", "Dr.Kentt", 2000);
Libro libro2 = new Libro("Spiderman vol 13", "Stan Lee", 43);

Console.WriteLine(libro1.Describir());
Console.WriteLine(libro2.Describir());

libro1.Paginas = 500;
Console.WriteLine(libro1.Describir());

class Libro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public int Paginas { get; set; }

    public Libro(string titulo, string autor, int paginas)
    {
        //El this no es necesario, ya que el compilador puede distinguir entre la mayuscula y la minuscula
        this.Titulo = titulo;
        this.Autor = autor;
        this.Paginas = paginas;
    }

    public string Describir()
    {
        return $"El libro '{Titulo}' de {Autor} tiene {Paginas} páginas.";
    }
}