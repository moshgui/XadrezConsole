using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.tabuleiro
{
    class Tabuleiro
    {
        //tabuleiro não está com valor instanciando pois pode se tratar de qualquer tabuleiro
        public int Linhas { get; set; }
        public int Colunas { get; set; }

        //matriz de Peca
        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;

            //matriz de Peca recebe os parametros do construtor Tabuleiro
            pecas = new Peca[Linhas, Colunas];
        }
    }
}
