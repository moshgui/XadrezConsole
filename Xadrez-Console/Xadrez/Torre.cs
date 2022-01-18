using System;
using Xadrez_Console.tabuleiro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.Xadrez
{
    class Torre : Peca
    {
        //associando o construtor de Peca.cs com os mesmos parametros de entrada
        public Torre(CorPeca cor, Tabuleiro tab) : base(cor, tab)
        {

        }

        //método acessado apelas pelo classe Rei.Cs 
        //verifica se existe uma peça que impede o movimento do rei
        //seja inimiga ou da mesma cor que o Rei
        private bool PodeMover(Posicao pos)
        {
            Peca p = Tabuleiro.peca(pos);
            return p == null || p.CorPeca != CorPeca;
        }

        //retorna a matriz com os oito possiveis movimentos do Rei
        //heranca e sobreposicao
        //matriz
        public override bool[,] MovimentosPossíveis()
        {
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            //acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                //se a posicao no tabuleiro nao estiver preenchida e a cor for diferente da peca em movimento
                mat[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.peca(pos) != null && Tabuleiro.peca(pos).CorPeca != CorPeca)
                {
                    //break para quebrar o laço while
                    break;
                }
                pos.Linha = pos.Linha - 1;
            }

            //abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                //se a posicao no tabuleiro nao estiver preenchida e a cor for diferente da peca em movimento
                mat[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.peca(pos) != null && Tabuleiro.peca(pos).CorPeca != CorPeca)
                {
                    break;
                }
                pos.Linha = pos.Linha + 1;
            }

            //direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                //se a posicao no tabuleiro nao estiver preenchida e a cor for diferente da peca em movimento
                mat[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.peca(pos) != null && Tabuleiro.peca(pos).CorPeca != CorPeca)
                {
                    break;
                }
                pos.Coluna = pos.Coluna + 1;
            }

            //esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                //se a posicao no tabuleiro nao estiver preenchida e a cor for diferente da peca em movimento
                mat[pos.Linha, pos.Coluna] = true;
                if (Tabuleiro.peca(pos) != null && Tabuleiro.peca(pos).CorPeca != CorPeca)
                {
                    break;
                }
                pos.Coluna = pos.Coluna - 1;
            }

            return mat;
        }

        public override string ToString()
        {
            return "T";
        }
    }
}
