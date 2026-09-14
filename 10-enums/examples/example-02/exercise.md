# Ejercicio 02 - Nivel de acceso

Se te pide gestionar el nivel de acceso de un usuario a partir de lo que introduce por consola.

Requisitos:

1. Declara un enum `NivelAcceso` con los valores `Invitado`, `Usuario` y `Administrador`, asignándoles explícitamente los valores numéricos `0`, `10` y `99` respectivamente (en vez de dejar que el compilador los numere automáticamente). Puedes resolver este ejercicio como file-based app o como proyecto real (tema 09) — si eliges proyecto real, declara el enum en su propio archivo.
2. Pide al usuario, por consola, que escriba el nombre de uno de los tres niveles (por ejemplo, escribiendo literalmente `Usuario`).
3. Convierte el texto introducido a un valor de `NivelAcceso` usando `Enum.Parse`.
4. Muestra por consola tanto el nombre del nivel como su valor numérico (con el cast correspondiente).
5. Compara el nivel obtenido con `NivelAcceso.Administrador` usando `==`, y muestra un mensaje distinto según si tiene permisos de administrador o no.

Prueba el programa escribiendo cada uno de los tres nombres válidos por turnos. No hace falta que el programa maneje el caso de escribir un nombre inválido (por ejemplo, `"Root"`) — eso se retoma cuando se vea el tema de excepciones; de momento basta con saber que el programa fallaría en ese caso.