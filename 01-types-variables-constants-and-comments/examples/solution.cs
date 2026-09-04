long starsCount = 1700000000;        // cifra enorme, catalogadas por la sonda Gaia -> long
int beetleSpecies = 400000;          // cabe de sobra en un int -> int
double moonDistanceKm = 384400.0;    // distancia media con decimales, sin necesitar precisión extrema -> double
decimal bitcoinPriceUsd = 60250.75m; // dinero -> decimal, evita errores de redondeo binario
bool isPlutoAPlanet = false;         // solo dos estados posibles -> bool
char mostCommonBloodType = 'O';      // un único carácter -> char
float ozoneLayerThicknessKm = 20.5f; // decimal simple, aquí solo para practicar el sufijo f -> float
string mostSpokenLanguage = "inglés"; // texto -> string

Console.WriteLine($"Se han catalogado {starsCount} estrellas en la Vía Láctea, y existen aproximadamente {beetleSpecies} especies conocidas de escarabajos.");
Console.WriteLine($"La Luna está a una media de {moonDistanceKm} km de la Tierra, y la capa de ozono tiene un grosor medio de {ozoneLayerThicknessKm} km.");
Console.WriteLine($"Un bitcoin cuesta hoy {bitcoinPriceUsd} dólares. Plutón {(isPlutoAPlanet ? "sigue siendo" : "ya no es")} un planeta, el idioma más hablado del mundo es el {mostSpokenLanguage} y el grupo sanguíneo más común es el tipo {mostCommonBloodType}.");