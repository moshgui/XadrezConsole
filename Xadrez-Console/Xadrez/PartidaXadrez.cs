using System;
using Xadrez_Console.tabuleiro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.Xadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int Turno { get; private set; }
        public CorPeca JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool Xeque { get; private set; }
        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = CorPeca.Branco;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            Xeque = false;
            IniciarPecas();
        }

        //método para movimentar peca
        //pega a peca na posicao origem e remove
        //captura a peca que esta na posicao destino
        //e insere na posicao destino
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RemoverPeca(origem);
            p.Movimento();
            Peca pecaCapturada = tab.RemoverPeca(destino);
            tab.ColocarPeca(p, destino);

            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RemoverPeca(destino);
            p.Decrementar();

            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            tab.ColocarPeca(p, origem);
        }

        //passagem de turno
        //incrementa mais um no turno
        //chama a funcao MudaJogador()
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXeque(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }


        //método que verifa se a posicao de origem existe
        //não pode ser uma posicao que está sem peca
        //não pode ser uma peca que nao seja sua
        //cada tipo de if tem sua propria excecao
        public void ValidarOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if (JogadorAtual != tab.peca(pos).CorPeca)
            {
                throw new TabuleiroException("Escolha uma peça sua!");
            }

            if (!tab.peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não existe movimentos possíveis para a peça escolhida!");
            }
        }

        //verifica se a posicao de destino da peca é valida
        //se e peca pode realizar o movimento para a casa especificada
        public void ValidarDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        //A cada jogada, verifica se o a cor que vai jogar é diferente da cor que jogou por ultimo
        private void MudaJogador()
        {
            if (JogadorAtual == CorPeca.Branco)
            {
                JogadorAtual = CorPeca.Preta;
            }
        }

        //método de conjuntos para contar quantas pecas foram capturadas passando a cor como parametro
        //se a cor da peca for igual a peca informada, adiciona no conjunto
        public HashSet<Peca> PecasCapturadas(CorPeca cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.CorPeca == cor)
                {
                    aux.Add(x);
                }
            }

            return aux;
        }

        //método semelhante ao de contar quantas pecas foram capturadas
        //mas ao inves de adicionar, ele retira
        public HashSet<Peca> PecasEmJogo(CorPeca cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas)
            {
                if (x.CorPeca == cor)
                {
                    aux.Add(x);
                }
            }

            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private CorPeca Adversaria(CorPeca cor)
        {
            if (cor == CorPeca.Branco)
            {
                return CorPeca.Preta;
            }
            else
            {
                return CorPeca.Branco;
            }
        }

        private Peca rei(CorPeca cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }

            return null;
        }

        public bool EstaEmXeque(CorPeca cor)
        {
            Peca R = rei(cor);

            /*if (R == null)
            {
                throw new TabuleiroException($"Não existe Rei no tabuleiro da cor {cor}");
            }/*/

            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossíveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TesteXeque(CorPeca cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossíveis();
                for (int i = 0; i < tab.Linhas; i++)
                {
                    for (int j = 0; j < tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        //método que ira converter a posicao da matriz para uma posicao valida do tabuleiro
        //ao iniciar uma peca, ela é adicionada ao conjuntos 'pecas'
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ConvertePosicao());
            pecas.Add(peca);
        }

        //método que irá iniciar as pecas do xadrez em suas devidas posicoes iniciais
        //as posicoes são instanciadas de acordo com o indice do xadrez
        //exemplo: Torre C1
        //mais legivel
        private void IniciarPecas()
        {
            ColocarNovaPeca('C', 1, new Torre(CorPeca.Branco, tab));


            ColocarNovaPeca('C', 2, new Torre(CorPeca.Branco, tab));
            ColocarNovaPeca('D', 2, new Torre(CorPeca.Branco, tab));
            ColocarNovaPeca('E', 2, new Torre(CorPeca.Branco, tab));
            ColocarNovaPeca('E', 1, new Torre(CorPeca.Branco, tab));
            ColocarNovaPeca('D', 1, new Rei(CorPeca.Branco, tab));

            ColocarNovaPeca('C', 7, new Torre(CorPeca.Preta, tab));
            ColocarNovaPeca('C', 8, new Torre(CorPeca.Preta, tab));
            ColocarNovaPeca('D', 7, new Torre(CorPeca.Preta, tab));
            ColocarNovaPeca('E', 7, new Torre(CorPeca.Preta, tab));
            ColocarNovaPeca('E', 8, new Torre(CorPeca.Preta, tab));
            ColocarNovaPeca('D', 8, new Rei(CorPeca.Preta, tab));
        }
    }
}
