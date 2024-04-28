using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocDameMAP_MVVM_Tema2
{
    public class Result
    {
        public Player Winner { get; }

        public Result(Player winner)
        {
            Winner = winner;
        }

    }
}
