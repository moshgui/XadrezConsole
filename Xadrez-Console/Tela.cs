using System;
using Xadrez_Console.Xadrez;
using Xadrez_Console.tabuleiro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console
{
    class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partida)
        {
            ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            ImprimirPecaCapturada(partida);
            Console.WriteLine();
            Console.WriteLine($"Turno: {partida.Turno}");
            Console.WriteLine($"Aguardando jogada do {partida.JogadorAtual}");
        }
        
        //método estático para imprimir na tela as informações referentes as pecas capturadas
        //chama a funcao ImprimirConjunto() para mostrar as pecas capturadas
        public static void ImprimirPecaCapturada(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas");
            Console.Write($"Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(CorPeca.Branco));
            Console.Write($"Pretas: ");
            ImprimirConjunto(partida.PecasCapturadas(CorPeca.Preta));
        }

        //método estático para mostrar quantas pecas já foram capturadas
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca x in conjunto)
            {
                Console.Write($"{x} ");
            }
            Console.Write("]");
        }
        
        //classe que tem como funcão apenas mostrar o tabuleiro na tela
        //tabuleiro se trata de uma matriz definida pela classe PartidaXadrez.cs
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                //inserindo os índices das linhas do xadrez
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirCorPeca(tab.peca(i, j));
                }
                //cw que quebra a linha no fim da matriz
                Console.WriteLine();
            }
            //todas as colunas de uma partida de xadrez
            Console.WriteLine("  A B C D E F G H");
        }

        //sobrecarga do método para receber uma matriz de bool
        //para mostrar quais as casas possiveis para movimento
        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                //inserindo os índices das linhas do xadrez
                Console.Write(8 - i + " ");

                for (int j = 0; j < tab.Colunas; j++)
                {
                    //se a posicao em i, j for vazia, muda para cor DarkGray
                    if (posicoesPossiveis[i, j] == true)
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    //caso contrario, mantem a cor original
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirCorPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                //cw que quebra a linha no fim da matriz
                Console.WriteLine();
            }
            //todas as colunas de uma partida de xadrez
            Console.WriteLine("  A B C D E F G H");
            Console.BackgroundColor = fundoOriginal;    
        }

        //método que pergunta pra qual posicao deseja movimentar uma peca
        //a string 's' captura a entrada pelo usuário, ex C2
        //faz um split na variavel de linha e instancia uma nova posicao
        public static PosicaoXadrez LerPosicao()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        //método que diferencia a cor das pecas
        public static void ImprimirCorPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                //se a cor da peca for branco, imprime normalmente
                if (peca.CorPeca == CorPeca.Branco)
                {
                    Console.Write(peca);
                }
                //se nao for, imprime em amarelo
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }
    }
}
