using Newtonsoft.Json;
using System.IO;

namespace JocDameMAP_MVVM_Tema2
{
    public class Statistics
    {
        public int redWins { get; set; } = 0;
        public int whiteWins {  get; set; }
        public int biggestWinDiff {  get; set; }

        public Statistics(StatsVM statsVM)
        {
            redWins = statsVM.stats.redWins;
            whiteWins = statsVM.stats.whiteWins;
            biggestWinDiff = statsVM.stats.biggestWinDiff;
        }
        public Statistics(Statistics stats)
        {
            redWins = stats.redWins;
            whiteWins = stats.whiteWins;
            biggestWinDiff = stats.biggestWinDiff;
        }
        public Statistics() 
        {
            
        }
       


    }
}
