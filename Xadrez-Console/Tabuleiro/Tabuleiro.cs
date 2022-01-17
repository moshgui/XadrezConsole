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

        //construtor Tabuleiro
        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;

            //matriz de 'Peca' recebe os parametros do construtor Tabuleiro
            pecas = new Peca[Linhas, Colunas];
        }

        //como a matriz para acessar as peças é 'private', é necesário um método para que ela possa ser acessada e modificada
        public Peca peca(int linhas, int colunas)
        {
            return pecas[linhas, colunas];
        }

        //melhoria de sobrecarga no construtor de uma peça
        //método recebe uma Posicao de pos de Posicao.cs
        //antes de receber uma posicao, há a validação se existe uma peça na posicao informa atravé do ExistePeca()
        public Peca peca(Posicao pos)
        { 
            return pecas[pos.Linha, pos.Coluna];
        }

        //método que verifica se existe uma peça na posicao que foi informada
        //mas antes verifica se a posicao que foi informada é válida
        //caso não tenha peca, retorna verdadeiro
        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos); ;
            return peca(pos) != null;
        }

        //método fará com que objeto 'p' nao posicao 'pos' ocupe uma Posicao (classe) que contem linha e coluna
        //a classe Posicao.cs recebe uma nova posicao chamada 'pos'
        public void ColocarPeca(Peca p, Posicao pos)
        {
            if (ExistePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição!");
            }
            pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        //metodo que remove peca
        //remove a peca e marca a posicao do tabuleiro como null
        public Peca RemoverPeca(Posicao pos)
        {
            if (peca(pos) == null)
            {
                return null;
            }

            Peca aux = peca(pos);
            aux.Posicao = null;
            pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        //método que irá validar se a posicao que a peça será colocada é válida
        //pos.Linha de Posicao.cs
        //Linhas de Tabuleiro.cs
        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Linha >= Linhas || pos.Coluna < 0 || pos.Coluna >= Colunas)
            {
                return false;
            }

            return true;
        }

        //caso a posição não seja válida, este método irá lançar uma exceção personalizada
        public void ValidarPosicao(Posicao pos)
        {
            //se a posicao 'pos' no método PosicaoValida() não for válida, é lançada uma exceção
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posicão inválida!");
            }
        }
    }
}
