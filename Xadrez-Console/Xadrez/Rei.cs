using System;
using Xadrez_Console.tabuleiro;
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

        public override string ToString()
        {
            return "R";
        }
    }
}
