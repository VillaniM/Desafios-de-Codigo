Console.WriteLine("Digite uma palavra:");
string palavra = Console.ReadLine();
List<char> contrario = new List<char>();

for (int i = palavra.Length - 1; i >= 0; i--)
{
    contrario.Add(palavra[i]);
}

string palavraContraria = new string(contrario.ToArray());
if (palavra != palavraContraria)
{
    Console.WriteLine("A palavra não é um palíndromo.");
}
else{
    Console.WriteLine("A palavra é um palíndromo.");
}