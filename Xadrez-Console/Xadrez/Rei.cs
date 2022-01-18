using System;
using Xadrez_Console.tabuleiro;
using Xadrez_Console.Xadrez;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.Xadrez
{
    class Rei : Peca
    {
        //associando o construtor de Peca.cs com os mesmos parametros de entrada
        public Rei(CorPeca cor, Tabuleiro tab) : base(cor, tab)
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
            //matriz booleana que tem como tamanho o proprio tamanho do tabuleiro
            bool[,] mat = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            //posicao instanciada em zero para depois receber a posicao do objeto Peca
            Posicao pos = new Posicao(0, 0);

            //acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Nordeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Sudeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Sudoeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Esquerda
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }
            //Noroeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos))
            {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }

        //objeto Rei retorna ao usuário como R
        public override string ToString()
        {
            return "R";
        }
    }
}
