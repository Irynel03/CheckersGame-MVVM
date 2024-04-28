using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocDameMAP_MVVM_Tema2
{
    public enum Player
    {
        None,
        White,
        Red
    }

    public static class PlayerExtensions
    {
        public static Player Opponent(this Player player)
        {
            return player switch
            {
                Player.White => Player.Red,
                Player.Red => Player.White,
                _ => Player.None,
            };
        }

    }
}
