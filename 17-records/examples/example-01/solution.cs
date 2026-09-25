
Libro libro1 = new Libro("Cien Años de Soledad", "Gabriel García Márquez", 1967);
Libro libro2 = new Libro("Cien Años de Soledad", "Gabriel García Márquez", 1967);

Console.WriteLine(libro1 == libro2); 
// Espero que imprima "True" porque los records comparan por valor, no por referencia. Si Libro fuera una class normal, imprimiría "False".

Console.WriteLine(libro1); 
// Imprime la representación legible del libro. Esto es posible porque los records generan automáticamente un método ToString() que devuelve una representación legible de sus propiedades.

Libro copia = libro1 with { AnioPublicacion = 2024 };
Console.WriteLine(copia);

record class Libro(string Titulo, string Autor, int AnioPublicacion);