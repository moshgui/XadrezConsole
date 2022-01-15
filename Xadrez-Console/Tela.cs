using System;
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
                        Console.Write(tab.peca(i, j) + " ");
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
