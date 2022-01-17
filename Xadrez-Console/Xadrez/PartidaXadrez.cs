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
        private int Turno;
        private CorPeca JogadorAtual;
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
