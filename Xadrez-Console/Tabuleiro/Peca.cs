using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.tabuleiro
{
    class Peca
    {
        public Posicao Posicao { get; set; }

        //cor só pode ser acessada por ela, ou por suas subclasses
        //associacao de classes
        public CorPeca CorPeca { get; protected set; }
        public int QtdeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Posicao posicao, CorPeca corPeca, Tabuleiro tabuleiro)
        {
            Posicao = posicao;
            CorPeca = corPeca;
            
            //quantidade de movimentos iniciazado com zero, pois a peça ainda não possui movimentos 
            QtdeMovimentos = 0;
            Tabuleiro = tabuleiro;
        }       
    }
}
