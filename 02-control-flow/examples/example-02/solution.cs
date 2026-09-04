//Declaracion de variables
int diaSemana = 1; // 1 = Lunes ... 7 = Domingo
string nombreDia = "";

switch (diaSemana) {
    case 1:
        nombreDia = "Lunes";
        break;
    case 2:
        nombreDia = "Martes";
        break;
    case 3:
        nombreDia = "Miércoles";
        break;
    case 4:
        nombreDia = "Jueves";
        break;
    case 5:
        nombreDia = "Viernes";
        break;
    case 6:
        nombreDia = "Sábado";
        break;
    case 7:
        nombreDia = "Domingo";
        break;
    default:
        nombreDia = "Día inválido.";
        break;
}

//Salida de resultados
if (nombreDia != "Día inválido.") {

    //Operador ternario para determinar si es fin de semana o día laborable
    string tipoDia = (diaSemana == 6 || diaSemana == 7) ? "Es fin de semana." : "Es día laborable.";

        //Se mete aqui dentro para que solo se ejecute si el dia es valido, sino se mostraria el mensaje de dia invalido y luego el mensaje de fin de semana o dia laborable, lo cual no tiene sentido

    Console.WriteLine($"Hoy es {nombreDia}. {tipoDia}");
} else {
    Console.WriteLine($"{nombreDia}");
}
