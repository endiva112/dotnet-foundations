# 00 - Dev skills generales

Antes de tocar C#, esto es lo mínimo que hace falta tener controlado.

## Git

Comandos que se usan el 90% del tiempo:

- `git init` — crea un repo nuevo en la carpeta actual
- `git clone <url>` — descarga un repo existente
- `git status` — qué ha cambiado, qué hay en staging
- `git add <archivo>` / `git add .` — mete cambios en staging
- `git commit -m "mensaje"` — guarda los cambios en staging como un punto en la historia
- `git push` / `git pull` — sube / baja cambios al remoto
- `git branch <nombre>` — crea una rama
- `git switch <rama>` (o `git checkout <rama>`) — cambia de rama
- `git switch -c <rama>` — crea la rama y cambia a ella en un paso
- `git merge <rama>` — mezcla una rama en la rama actual
- `git log --oneline` — historial resumido

Commits atómicos: cada commit debería representar un único cambio lógico, no un batiburrillo de "arreglos varios". Si cuesta resumir el commit en una línea, probablemente son dos commits.

`.gitignore`: archivo que le dice a git qué carpetas/archivos no debe versionar (`bin/`, `obj/`, configuración local del IDE, etc). Para un proyecto .NET, la plantilla estándar de .gitignore para Visual Studio ya cubre esto.

## IDE

Se usa Visual Studio Code con la extensión **C# Dev Kit** (oficial de Microsoft). Se instala desde el marketplace de extensiones dentro de VS Code.

## .NET SDK

Comando para comprobar la versión instalada:

```
dotnet --version
```

Salida esperada (ejemplo):

```
10.0.400
```

Descarga del SDK si hace falta instalarlo: https://dotnet.microsoft.com/download

## HTTP/HTTPS (a nivel concepto, para más adelante)

No hace falta profundizar todavía, pero conviene tenerlo claro desde ya porque todo ASP.NET Core se construye sobre esto:

- HTTP es el protocolo con el que un cliente (navegador, app, otro servicio) pide algo a un servidor y este responde.
- HTTPS es HTTP cifrado con TLS.
- Una petición tiene un método (`GET`, `POST`, `PUT`, `DELETE`...), una URL, cabeceras y, opcionalmente, un cuerpo.
- Una respuesta tiene un código de estado (`200`, `404`, `500`...), cabeceras y, opcionalmente, un cuerpo.