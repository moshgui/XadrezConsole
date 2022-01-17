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
        //classe que tem como funcão apenas mostrar o tabuleiro na tela
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                //inserindo os índices das linhas do xadrez
                Console.Write(8 - i + " ");
               
                for (int j = 0; j < tab.Colunas; j++)
                {
                    //se a posicao i, j do tabuleiro estiver vazia, o método escreve apenas um traco
                    if (tab.peca(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    //caso a posicao i, j esteja preenchida, o metodo escreve a peca e as posicoes vazias
                    else
                    {
                        ImprimirCorPeca(tab.peca(i, j));
                        Console.Write(" ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H");
        }

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
        }
    }
}
