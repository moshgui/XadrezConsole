using System;
using Xadrez_Console.tabuleiro;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.Xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        
        //este método converte a posicao de xadrez para a posicao da matriz instanciada no Program.cs
        public Posicao ConvertePosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'A'); 
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
