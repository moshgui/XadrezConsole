using System;
using Xadrez_Console.tabuleiro;
using Xadrez_Console.Xadrez;

namespace Xadrez_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.ColocarPeca(new Torre(CorPeca.Preta, tab), new Posicao(0, 0));
            tab.ColocarPeca(new Rei(CorPeca.Branco, tab), new Posicao(1, 3));

            Tela.ImprimirTabuleiro(tab);
        }
    }
}
