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
        public Peca vulneravelEnPassant { get; private set; }

        //construtor da partida de xadrez
        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = CorPeca.Branco;
            Terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            Xeque = false;
            vulneravelEnPassant = null;
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

            //jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RemoverPeca(origemTorre);
                T.Movimento();
                tab.ColocarPeca(T, destinoTorre);
            }

            //jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RemoverPeca(origemTorre);
                T.Movimento();
                tab.ColocarPeca(T, destinoTorre);
            }

            //en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.CorPeca == CorPeca.Branco)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }

                    pecaCapturada = tab.RemoverPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }

        //metodo que desfaz o movimento das pecas
        //normalmente é acionado quando há um erro
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

            //jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RemoverPeca(destinoTorre);
                T.Decrementar();
                tab.ColocarPeca(T, origemTorre);
            }

            //jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RemoverPeca(destinoTorre);
                T.Movimento();
                tab.ColocarPeca(T, origemTorre);
            }

            //en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
                {
                    Peca peao = tab.RemoverPeca(destino);
                    Posicao posP;
                    if (p.CorPeca == CorPeca.Branco)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }

                    tab.ColocarPeca(peao, posP);
                }
            }

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

            Peca p = tab.peca(destino);
            //jogada especial promocao
            //caso o peao chegue a ultima linha do tabuleiro se torna dama
            if (p is Peao)
            {
                if ((p.CorPeca == CorPeca.Branco && destino.Linha == 0) || (p.CorPeca == CorPeca.Preta && destino.Linha == 7))
                {
                    p = tab.RemoverPeca(destino);   
                    pecas.Remove(p);    
                    Peca Dama = new Dama(p.CorPeca, tab);
                    tab.ColocarPeca(Dama, destino);
                    pecas.Add(Dama);
                }
            }

            //testando se está em xeque
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

           

            //jogada especial en passant
            if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
            {
                vulneravelEnPassant = p;
            }
            else
            {
                vulneravelEnPassant = null;
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
                bool[,] mat = x.MovimentosPossiveis();
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
                bool[,] mat = x.MovimentosPossiveis();
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
            ColocarNovaPeca('A', 1, new Torre(CorPeca.Branco, tab));
            ColocarNovaPeca('B', 1, new Cavalo(CorPeca.Branco, tab));
            ColocarNovaPeca('C', 1, new Bispo(CorPeca.Branco, tab));
            ColocarNovaPeca('D', 1, new Dama(CorPeca.Branco, tab));
            ColocarNovaPeca('E', 1, new Rei(CorPeca.Branco, tab, this));
            ColocarNovaPeca('F', 1, new Bispo(CorPeca.Branco, tab));
            ColocarNovaPeca('G', 1, new Cavalo(CorPeca.Branco, tab));
            ColocarNovaPeca('H', 1, new Torre(CorPeca.Branco, tab));
            ColocarNovaPeca('A', 2, new Peao(CorPeca.Branco, tab, this));
            ColocarNovaPeca('B', 2, new Peao(CorPeca.Branco, tab, this));
            ColocarNovaPeca('C', 2, new Peao(CorPeca.Branco, tab, this));
            ColocarNovaPeca('D', 2, new Peao(CorPeca.Branco, tab, this));
            ColocarNovaPeca('E', 2, new Peao(CorPeca.Branco, tab, this));
            ColocarNovaPeca('F', 2, new Peao(CorPeca.Branco, tab, this));
            ColocarNovaPeca('G', 2, new Peao(CorPeca.Branco, tab, this));
            ColocarNovaPeca('H', 2, new Peao(CorPeca.Branco, tab, this));

            ColocarNovaPeca('A', 7, new Peao(CorPeca.Preta, tab, this));
            ColocarNovaPeca('B', 7, new Peao(CorPeca.Preta, tab, this));
            ColocarNovaPeca('C', 7, new Peao(CorPeca.Preta, tab, this));
            ColocarNovaPeca('D', 7, new Peao(CorPeca.Preta, tab, this));
            ColocarNovaPeca('E', 7, new Peao(CorPeca.Preta, tab, this));
            ColocarNovaPeca('F', 7, new Peao(CorPeca.Preta, tab, this));
            ColocarNovaPeca('G', 7, new Peao(CorPeca.Preta, tab, this));
            ColocarNovaPeca('H', 7, new Peao(CorPeca.Preta, tab, this));
            ColocarNovaPeca('A', 8, new Torre(CorPeca.Preta, tab));
            ColocarNovaPeca('B', 8, new Cavalo(CorPeca.Preta, tab));
            ColocarNovaPeca('C', 8, new Bispo(CorPeca.Preta, tab));
            ColocarNovaPeca('D', 8, new Dama(CorPeca.Preta, tab));
            ColocarNovaPeca('E', 8, new Rei(CorPeca.Preta, tab, this));
            ColocarNovaPeca('F', 8, new Bispo(CorPeca.Preta, tab));
            ColocarNovaPeca('G', 8, new Cavalo(CorPeca.Preta, tab));
            ColocarNovaPeca('H', 8, new Torre(CorPeca.Preta, tab));
        }
    }
}
