using System;
using Xadrez_Console.tabuleiro;
using Xadrez_Console.Xadrez;

namespace Xadrez_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                              
               
                Tela.ImprimirTabuleiro(tab);
            }

            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }

            PosicaoXadrez pos = new PosicaoXadrez('C', 7);
            Console.WriteLine(pos);
            Console.WriteLine(pos.ConvertePosicao());

            Console.ReadLine();
        }
    }
}
