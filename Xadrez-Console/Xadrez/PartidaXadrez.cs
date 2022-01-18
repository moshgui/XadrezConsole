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

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = CorPeca.Branco;
            Terminada = false;
            IniciarPecas();
        }

        //método para movimentar peca
        //pega a peca na posicao origem e remove
        //captura a peca que esta na posicao destino
        //e insere na posicao destino
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RemoverPeca(origem);
            p.Movimento();
            Peca pecaCapturada = tab.RemoverPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        //passagem de turno
        //incrementa mais um no turno
        //chama a funcao MudaJogador()
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
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

        //método que irá iniciar as pecas do xadrez em suas devidas posicoes iniciais
        //as posicoes são instanciadas de acordo com o indice do xadrez
        //exemplo: Torre C1
        private void IniciarPecas()
        {
            tab.ColocarPeca(new Torre(CorPeca.Branco, tab), new PosicaoXadrez('C', 1).ConvertePosicao());
            tab.ColocarPeca(new Torre(CorPeca.Branco, tab), new PosicaoXadrez('C', 2).ConvertePosicao());
            tab.ColocarPeca(new Torre(CorPeca.Branco, tab), new PosicaoXadrez('D', 2).ConvertePosicao());
            tab.ColocarPeca(new Torre(CorPeca.Branco, tab), new PosicaoXadrez('E', 2).ConvertePosicao());
            tab.ColocarPeca(new Torre(CorPeca.Branco, tab), new PosicaoXadrez('E', 1).ConvertePosicao());
            tab.ColocarPeca(new Rei(CorPeca.Branco, tab), new PosicaoXadrez('D', 1).ConvertePosicao());

            tab.ColocarPeca(new Torre(CorPeca.Preta, tab), new PosicaoXadrez('C', 7).ConvertePosicao());
            tab.ColocarPeca(new Torre(CorPeca.Preta, tab), new PosicaoXadrez('C', 8).ConvertePosicao());
            tab.ColocarPeca(new Torre(CorPeca.Preta, tab), new PosicaoXadrez('D', 7).ConvertePosicao());
            tab.ColocarPeca(new Torre(CorPeca.Preta, tab), new PosicaoXadrez('E', 7).ConvertePosicao());
            tab.ColocarPeca(new Torre(CorPeca.Preta, tab), new PosicaoXadrez('E', 8).ConvertePosicao());
            tab.ColocarPeca(new Rei(CorPeca.Preta, tab), new PosicaoXadrez('D', 8).ConvertePosicao());

        }
    }
}
