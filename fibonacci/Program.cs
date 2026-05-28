Console.WriteLine("Digite um número inteiro:");
int elementos = int.Parse(Console.ReadLine());

if (elementos <= 0)
{
    Console.WriteLine("Por favor, digite um número inteiro positivo.");
    return;
}

int[] fibonacci = new int[elementos];

fibonacci[0] = 0;
if (elementos > 1)
{
    fibonacci[1] = 1;
}
//Começa a contar a sequência a partir do terceiro elemento, que é a soma dos dois anteriores(0,1,1)
for (int i = 2; i < elementos; i++)
{
    //o cálculo volta um índice para pegar o número anterior, e depois volta mais outro índice para aplicar a soma dos dois números anteriores.
    fibonacci[i] = fibonacci[i - 1] + fibonacci[i - 2];
}

Console.WriteLine("Sequência de Fibonacci:");

//o índice começa em 0, por isso i tem que ser menor que elementos, e não menor igual.
for (int i = 0; i < elementos; i++)
{
    Console.Write(fibonacci[i] + ", ");
}   