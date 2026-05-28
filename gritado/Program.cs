Console.WriteLine("Digite seu texto educadamente, por favor:");
string textoGritado = Console.ReadLine();

if (string.IsNullOrEmpty(textoGritado))
{
    Console.WriteLine("Você não digitou nada. Por favor, tente novamente.");
    return;
}

int indexInterrogacao = textoGritado.IndexOf('?');
int indexExclamacao = textoGritado.IndexOf('!');

if (indexInterrogacao > 0)
{
    for (int i = indexInterrogacao + 1; i < textoGritado.Length; i++)
    {
        if (textoGritado[i] == '?')
        {
            textoGritado = textoGritado.Remove(i, 1);
            i--; // Ajusta o índice após remover o caractere
        }
        else if (textoGritado[i] == ' ')//procura espaços em branco para separar as palavras e seus pontos.EX: O que???!!!!! Não acredito!!!
        {
            indexInterrogacao = textoGritado.IndexOf('?', i + 1);//procura novamente o ponto na próxima palavra
            if (indexInterrogacao > 0)
            {
                i = indexInterrogacao; // Atualiza o índice para continuar a busca
            }
        }

    }

    indexExclamacao = textoGritado.IndexOf('!');
}

if (indexExclamacao > 0)
{
    for (int i = indexExclamacao + 1; i < textoGritado.Length; i++)
    {
        if (textoGritado[i] == '!')
        {
            textoGritado = textoGritado.Remove(i, 1);
            i--; // Ajusta o índice após remover o caractere
        }
        else if (textoGritado[i] == ' ') //procura espaços em branco para separar as palavras e seus pontos.EX: O que???!!!!! Não acredito!!!
        {
            indexExclamacao = textoGritado.IndexOf('!', i + 1);//procura novamente o ponto na próxima palavra
            if (indexExclamacao > 0)
            {
                i = indexExclamacao; // Atualiza o índice para continuar a busca
            }
        }
    }
}

Console.WriteLine("Você digitou: " + textoGritado);
