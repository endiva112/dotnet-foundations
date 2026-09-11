# Roadmap — De C# a la Tactical Coordination Platform

> Objetivo final ("Everest"): una API REST robusta estilo C2/coordinación de agentes,
> con arquitectura limpia, tests, CI/CD, auth, logs y todo lo que un sistema real de
> defensa/infraestructura crítica necesitaría para que "lo use todo el mundo y su abuela
> y no se caiga".
>
> Este documento es el índice maestro. Cada carpeta numerada tiene su propio `README.md`
> con: qué se aprende, conceptos esenciales (notas breves, no un libro), y 3 enunciados
> de ejercicios que solo usan lo visto hasta ese punto.
>
> **El orden aquí no es dogma.** Si al llegar a un bloque descubro que faltaba algo antes,
> reordeno las carpetas y anoto el cambio en la sección "Cambios sobre el plan original"
> al final de este archivo. Un roadmap que no se toca es un roadmap mal hecho.

---

## Leyenda

- 🟢 **Troncal** — imprescindible para llegar al Everest, no se salta.
- 🟡 **Recomendado** — no es estrictamente necesario, pero un técnico lo esperaría ver.
- ⚪ **Opcional** — mapa mental, se aprende solo si el proyecto lo pide en su momento.
- 🏁 **Checkpoint** — al terminar este bloque hay algo demostrable (commit/release enseñable).

---

## Fase 0 — Fundamentos de C# (lenguaje puro, sin web todavía)

Todo en consola. El objetivo es pensar en C# sin depender de un framework encima.

| # | Carpeta | Contenido | Nivel |
|---|---------|-----------|-------|
| 00 | `00-general-dev-skills` | Git (branches, commits atómicos, .gitignore), CLI de `dotnet`, HTTP/HTTPS a nivel concepto (para más adelante) | 🟡 |
| 01 | `01-types-and-variables` | Tipos de valor vs referencia, inferencia (`var`), constantes | 🟢 |
| 01b | `01b-nullable-reference-types` | `?`, null-forgiving `!`, por qué importa desde ya (movido pronto a propósito) | 🟡 |
| 02 | `02-control-flow` | `if/else`, `switch` clásico, operadores lógicos | 🟢 |
| 02b | `02b-pattern-matching` | `switch` expressions, patrones (`is`, deconstrucción básica) | 🟡 |
| 03 | `03-loops` | `while`, `do-while`, `for`, `foreach` — cuándo usar cada uno | 🟢 |
| 04 | `04-methods` | Parámetros, sobrecarga, `ref`/`out`, valores por defecto | 🟡 |
| 04b | `04b-enums-and-structs` | `enum` (útil para roles/estados de tu dominio), struct vs class |  |
| 05 | `05-collections` | `List<T>`, `Dictionary<K,V>`, `HashSet<T>`, arrays |  |
| 06 | `06-classes` | Encapsulación, constructores, propiedades, `static` |  |
| 06b | `06b-records` | Records, inmutabilidad, `with` — muy usado en DTOs de API |  |
| 07 | `07-interfaces` | Contratos, inyección conceptual (antes de DI de verdad) |  |
| 08 | `08-generics` | `List<T>` por dentro, constraints básicas |  |
| 08b | `08b-delegates-and-lambdas` | `Func<>`, `Action<>`, expresiones lambda — puente obligatorio a LINQ |  |
| 09 | `09-exceptions` | `try/catch/finally`, excepciones propias, cuándo NO usar excepciones |  |
| 10 | `10-linq` | Métodos de extensión, sintaxis fluida y de query |  |
| 10b | `10b-json-serialization` | `System.Text.Json`, serializar/deserializar tus propias clases |  |
| 11 | `11-async` | `Task`, `async/await`, por qué importa en I/O (base de todo ASP.NET Core) |  |
| 12 | `12-files` | Lectura/escritura de ficheros, streams básicos |  |
| 13 | `13-testing-fundamentals` | xUnit sobre clases sueltas, sin web todavía — Arrange/Act/Assert |  |

🏁 **Checkpoint 0:** una app de consola que simule un caso mini del dominio (p. ej. un
`List<Agent>` con estado, algo de LINQ para filtrar, serializado a JSON, con 3-4 tests
xUnit) — esto ya demuestra que dominas el lenguaje base.

- bcl (cerrado)
- enums
- classes
- metodos2
- structs
- herencia-y-polimorfismo   ← nuevo, aprovechando el contraste con structs
- records
- interfaces                ← contraste "extender" vs "implementar", + nota de null object
- generics
- colecciones
- stringbuilder   

---

## Fase 1 — Bases de datos y ASP.NET Core Basics

Aquí empieza el "web". Todo lo de Fase 0 se reutiliza constantemente.

| # | Carpeta | Contenido | Nivel |
|---|---------|-----------|-------|
| 14 | `14-sql-fundamentals` | SQL básico: SELECT/JOIN/WHERE, diseño de tablas, claves foráneas | 🟢 |
| 15 | `15-aspnet-core-basics` | Minimal APIs, routing, model binding, `IResult` | 🟢 |
| 16 | `16-configuration-and-middleware` | `appsettings.json`, pipeline de middleware, entornos (Dev/Prod) | 🟢 |
| 17 | `17-dependency-injection` | Scoped/Transient/Singleton, `IServiceCollection`, por qué DI y no `new` a mano | 🟢 |
| 18 | `18-orm-ef-core` | EF Core: DbContext, Code First, migraciones, queries básicas | 🟢 |
| 18b | `18b-ef-core-relationships` | Relaciones 1-N / N-N, lazy vs eager loading (solo cuando duela, no antes) | 🟢 |

🏁 **Checkpoint 1 (esto ya es "algo que enseñar"):** API REST mínima —
`Agent`, `Mission`, `DeploymentZone` con CRUD real sobre Postgres/SQL Server vía EF Core,
documentada con Swagger/OpenAPI. **Meta realista: aquí en 3-5 semanas si vas constante.**

---

## Fase 2 — Hacerla robusta (esto es lo que impresiona al técnico)

| # | Carpeta | Contenido | Nivel |
|---|---------|-----------|-------|
| 19 | `19-validation` | FluentValidation o DataAnnotations, respuestas 400 consistentes | 🟢 |
| 20 | `20-error-handling` | Middleware global de excepciones, `ProblemDetails` (RFC 7807) | 🟢 |
| 21 | `21-logging` | Serilog, logging estructurado, correlación de requests | 🟢 |
| 22 | `22-authentication-authorization` | JWT, roles (operador/comandante/admin — encaja con el dominio), políticas | 🟢 |
| 23 | `23-solid-and-architecture` | SOLID aplicado, capas (API / Application / Domain / Infrastructure) | 🟢 |

🏁 **Checkpoint 2:** el CRUD de la Fase 1 pero con auth real, validación, logs y
arquitectura en capas. Este es el punto en el que un reclutador técnico ya se sienta
a mirarlo en serio.

---

## Fase 3 — Calidad de ingeniería (tests, CI/CD)

| # | Carpeta | Contenido | Nivel |
|---|---------|-----------|-------|
| 24 | `24-testing-aspnet` | xUnit + `WebApplicationFactory` (tests de integración reales sobre la API) | 🟢 |
| 25 | `25-docker` | Dockerfile, docker-compose (API + base de datos) | 🟢 |
| 26 | `26-cicd-github-actions` | Pipeline: build → test → (opcional) publicar imagen | 🟢 |

🏁 **Checkpoint 3:** repo con badge de CI en verde, tests corriendo en cada push,
todo levantable con `docker-compose up`. Esto es lo que se pide en la mayoría de
ofertas serias como "must have".

---

## Fase 4 — El dominio C2 de verdad (aquí empieza a parecerse al Everest)

Todo lo anterior era genérico y aplicable a cualquier API. Aquí es donde se construye
la lógica específica de "simulación de despliegue de agentes".

| # | Carpeta | Contenido | Nivel |
|---|---------|-----------|-------|
| 27 | `27-caching` | Memory cache / Redis — solo si hay un endpoint que lo justifique | ⚪ |
| 28 | `28-background-jobs` | `IHostedService` / Hangfire para procesos de simulación en segundo plano | 🟡 |
| 29 | `29-realtime-signalr` | SignalR para estado de agentes en tiempo real (si se quiere UI algún día) | ⚪ |
| 30 | `30-domain-simulation-engine` | La lógica real: estados de agentes, eventos, misiones, reglas de negocio | 🟢 |

---

## Fase 5 — El Everest

| # | Carpeta | Contenido |
|---|---------|-----------|
| 31 | `31-tactical-coordination-platform` | El proyecto final integrando todo lo anterior. Aquí no se "aprende" nuevo, se **compone** todo lo que ya sabes hacer bien. |

---

## Convención de cada carpeta

Cada carpeta (a partir de la 01) sigue esta estructura interna:

```
NN-nombre-del-tema/
├── README.md          # qué se aprende + notas esenciales + enlaces para googlear
├── ejercicios/
│   ├── 01-enunciado.md
│   ├── 02-enunciado.md
│   └── 03-enunciado.md
└── solucion/           # tu propia solución, una vez corregida en otro chat
```

El `README.md` de cada bloque NUNCA explica algo ya visto en un bloque anterior a fondo —
como mucho lo menciona de pasada. Solo explica lo nuevo de ese bloque.

---

## Cambios sobre el plan original

*(Aquí anoto cuando reordene algo porque en la práctica no encajó como estaba pensado)*

- —

---

## Meta de tiempo (orientativa, no una promesa)

| Hito | Semana aprox. |
|------|----------------|
| Fin Fase 0 (fundamentos C#) | 2-3 |
| Checkpoint 1 (CRUD básico enseñable) | 4-5 |
| Checkpoint 2 (robusta: auth, logs, capas) | 6-7 |
| Checkpoint 3 (tests + CI/CD) | 8 |
| Fase 4-5 (dominio C2 real) | en adelante, sin prisa artificial |
