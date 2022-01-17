using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.tabuleiro
{
    class TabuleiroException : Exception
    {
        //exceção personalizada que retorna uma mensagem
        public TabuleiroException (string msg) : base(msg)
        {

        }
    }
}
