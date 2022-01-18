using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xadrez_Console.tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }

        //cor só pode ser acessada por ela, ou por suas subclasses
        //associacao de classes
        public CorPeca CorPeca { get; protected set; }
        public int QtdeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        //posicao de uma peca comeca como 'null' pois nao existe movimento para ela
        //a mesma regra vale para QtdeMovimentos
        public Peca(CorPeca corPeca, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Tabuleiro = tabuleiro;
            QtdeMovimentos = 0;
            CorPeca = corPeca;
            
            //quantidade de movimentos iniciazado com zero, pois a peça ainda não possui movimentos 
            QtdeMovimentos = 0;
            
        }    
        
        //a cada movimento de uma peça, esse método faz com que seja incrementando mais um movimento
        //como a QtdeMovimentos é instanciada com zero, nenhuma peca tem movimento registrado
        public void Movimento()
        {
            QtdeMovimentos++;
        }

        //for varre toda a matriz do metodo MovimentosPossives()
        //caso exista alguma posicao que esteja vaga, retorna true
        //caso contrato, false
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossíveis();
            for (int i = 0; i < Tabuleiro.Linhas; i++)
            {
                for (int j = 0; j < Tabuleiro.Colunas; j++)
                {
                    if (mat[i, j] == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossíveis()[pos.Linha, pos.Coluna];
        }
        
        //método abstrato pois não existem movimentos definidos para peca que nao sabemos qual é
        //método genérico
        //deve ser implementado nas classes de pecas
        public abstract bool [,] MovimentosPossíveis();
    }
}
