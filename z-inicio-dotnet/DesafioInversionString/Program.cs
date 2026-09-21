string pangram = "The quick brown fox jumps over the lazy dog";

//OBJETIVO -> ehT kciuq nworb xof spmuj revo eht yzal god

string[] words = pangram.Split(' ');

for (int i = 0; i < words.Length; i++)          // The
{
    char[] letters = words[i].ToCharArray();    // {T, h, e}
    Array.Reverse(letters);                     // {e, h, T}
    words[i] = String.Join("", letters);        // ehT
}

pangram = String.Join(" ", words);

Console.WriteLine(pangram);