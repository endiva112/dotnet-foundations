A. Ya tienen destino fijado — se resuelven con el próximo tema
List<T>.Find, FindAll, Exists, Sort con comparador/lambda (Colecciones) — dependen de poder pasar una función como argumento.
Genéricos con delegados (Func<T>, Action<T>) (Generics) — mismo caso.

Ambos quedan resueltos en cuanto hagamos Delegados y lambdas, el tema que acabamos de acordar.

B. Con destino aproximado, pero sin tema ni fecha concreta todavía
Excepciones, LINQ, async/await, files, json-serialization, testing-fundamentals — del plan original, sin orden fino entre ellos.
Iteradores personalizados (yield return, implementar IEnumerable<T> a mano) (Interfaces, Colecciones) — encajaría junto a LINQ o justo antes, porque yield explica la ejecución diferida de LINQ por dentro.
IDisposable, constructores estáticos, destructores/finalizers (Herencia) — pendiente explícito para cuando se trate Files.
Proyectos .NET II (estructura seria, varias capas, tests) (anotado desde Introducción a proyectos .NET) — pendiente para cuando haya motivo real de varios proyectos (Files o Testing).
protected internal / private protected (Herencia) — mismo caso, necesita varios proyectos para tener sentido real.
Inyección de dependencias (Interfaces) — pendiente para cuando exista contexto de ASP.NET Core.
Indexadores (this[int i]) (Herencia) — quedó como "tema opcional si llega a hacer falta", sin ubicación.
Eventos (Herencia) — pregunta abierta sobre si entran en Delegados y lambdas (el tema que viene) o se quedan aparte; conviene decidirlo cuando lleguemos ahí.
LinkedList<T> como micro-tema (Colecciones II) — condicional, "si en algún momento renta por eficiencia".
C. Pendientes que dijimos resolver en un tema ya cerrado, y que al redactarlo se quedaron fuera

Esto es autocrítica real, repasando mis propios readmes:

IComparable<T> implementado a fondo, con código completo: se mencionó como motivo de por qué Sort() falla sin él, tanto en Interfaces como en Colecciones — pero en ningún sitio se llegó a escribir el ejemplo completo (CompareTo, la clase implementándolo). Quedó nombrado dos veces, desarrollado ninguna.
Copia superficial de Array.Clone() / inmutabilidad "profunda" de with en Records: ambos se aparcaron explícitamente para "cuando existan colecciones con las que dar un ejemplo honesto" — Colecciones ya pasó, y no volvimos a por ninguno de los dos.
Patrones de lista (is [1, 2, 3]) (Pattern Matching II): se dejó fuera "hasta que exista List<T>" — también ya existe, y no se ha retomado.
StringBuilder.AppendFormat: se dijo explícitamente que se dejaba para el tema de Formato — el readme de Formato no llegó a mencionarlo.
StringComparison explícito (comparaciones de texto insensibles a mayúsculas, etc.): se dijo que se dejaba para Tipos y parsing — tampoco llegó a aparecer ahí.
[NotNullWhen] y atributos de análisis de nulabilidad (NRT): mencionado como "se retoma cuando se vea out" — nunca se retomó explícitamente después de Métodos II.
IFormattable/ICustomFormatter: en el alcance acordado para Formato dije que iría como mención de una frase — no llegó a entrar en el readme final.

El grupo C es el que de verdad merece acción antes de seguir — son promesas concretas, hechas en temas ya cerrados, que no se cumplieron. ¿Quieres que las vaya cerrando una a una (como añadidos puntuales a los temas correspondientes), o las dejamos todas anotadas para un repaso conjunto más adelante, una vez cerremos Delegados y lambdas?