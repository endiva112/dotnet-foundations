# Ejercicio 01 - Ficha de libro

Se te pide modelar un libro con una clase.

Requisitos:

1. Crea una clase `Libro` con las propiedades `Titulo` (`string`), `Autor` (`string`) y `Paginas` (`int`), todas con `get` y `set` públicos (auto-propiedades).
2. Añade un constructor que reciba los tres datos y los asigne, usando `this` en los parámetros que compartan nombre con la propiedad correspondiente.
3. Añade un método `Describir()` que devuelva un `string` con un mensaje usando los tres datos, por ejemplo: `"El libro 'Cien años de soledad' de Gabriel García Márquez tiene 471 páginas."`
4. Desde el punto de entrada del programa, crea al menos dos instancias distintas de `Libro`, y muestra el resultado de `Describir()` para cada una.
5. Sobre una de las instancias, cambia el valor de `Paginas` directamente (`miLibro.Paginas = 500;`) y vuelve a llamar a `Describir()` para comprobar que el cambio se refleja.