using System;
using Xadrez_Console.tabuleiro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.Xadrez
{
    class Bispo : Peca
    {
        public Bispo(CorPeca cor, Tabuleiro tab) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "B";
        }

        private bool PodeMover(Posicao pos)
        {
            Peca p = Tabuleiro.peca(pos);
            return p == null || p.CorPeca != CorPeca;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            //
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null && Tabuleiro.peca(pos).CorPeca != CorPeca)
                {
                    break;
                }

                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            }

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null && Tabuleiro.peca(pos).CorPeca != CorPeca)
                {
                    break;
                }

                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
            }

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null && Tabuleiro.peca(pos).CorPeca != CorPeca)
                {
                    break;
                }

                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
            }

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.peca(pos) != null && Tabuleiro.peca(pos).CorPeca != CorPeca)
                {
                    break;
                }

                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
            }

            return mat;
        }
    }
}
