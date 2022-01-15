using System;
using Xadrez_Console.tabuleiro;

namespace Xadrez_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(3, 4);

            Console.WriteLine($"Posicão: {p}");

            Tabuleiro tab = new Tabuleiro(8, 8);
        }
    }
}
