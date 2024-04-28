using Newtonsoft.Json;
using System.IO;

namespace JocDameMAP_MVVM_Tema2
{
    public class StatsVM
    {
        public Statistics stats { get; set; }
        public StatsVM() 
        {
            stats = LoadFromFile();   
        }
        public Statistics LoadFromFile()
        {
            string json = File.ReadAllText("C:/Users/andre/Desktop/sem II/MAP/JocDameMAP-MVVM-Tema2/JocDameMAP-MVVM-Tema2/Models/Data/Statistics.json");
            return JsonConvert.DeserializeObject<Statistics>(json);
        }



    }
}
