//IndexOf
string message = "What is the value <span>between the tags</span>?";
const string openSpan = "<span>";
const string closeSpan = "</span>";

int openingPosition = message.IndexOf(openSpan);
int closingPosition = message.IndexOf(closeSpan);

openingPosition += openSpan.Length;
int length = closingPosition - openingPosition;
Console.WriteLine(message.Substring(openingPosition, length));


//LastIndexOf
string message2 = "(What if) I am (only interested) in the last (set of parentheses)?";
int openingPosition2 = message2.LastIndexOf('(');

openingPosition2 += 1;
int closingPosition2 = message2.LastIndexOf(')');
int length2 = closingPosition2 - openingPosition2;
Console.WriteLine(message2.Substring(openingPosition2, length2));

//Multiples instancias de IndexOf
string message3 = "(What if) there are (more than) one (set of parentheses)?";
while (true)
{
    int openingPosition3 = message3.IndexOf('(');
    if (openingPosition3 == -1) break;

    openingPosition3 += 1;
    int closingPosition3 = message3.IndexOf(')');
    int length3 = closingPosition3 - openingPosition3;
    Console.WriteLine(message3.Substring(openingPosition3, length3));

    // Note the overload of the Substring to return only the remaining 
    // unprocessed message:
    message3 = message3.Substring(closingPosition3 + 1);
}