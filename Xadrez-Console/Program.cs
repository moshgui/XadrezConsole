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
                PartidaXadrez partida = new PartidaXadrez();

                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.Write("Digite a posição de origem: ");
                    Posicao origem = Tela.LerPosicao().ConvertePosicao();

                    bool[,] posicoesPossiveis = partida.tab.peca(origem).MovimentosPossíveis();

                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab, posicoesPossiveis);

                    Console.Write("Digite a posição destino: ");
                    Posicao destino = Tela.LerPosicao().ConvertePosicao();

                    partida.ExecutaMovimento(origem, destino);
                }            
                            
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
