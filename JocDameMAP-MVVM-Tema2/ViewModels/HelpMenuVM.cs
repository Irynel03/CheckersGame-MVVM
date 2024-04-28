using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JocDameMAP_MVVM_Tema2
{
    public class HelpMenuVM
    {
        public string creatorName { get; set; }
        public string institutionalEmail {  get; set; }
        public string grupa {  get; set; }
        public string gameDescription {  get; set; }
        
        public HelpMenuVM()
        {
            creatorName = "Ene Irinel";
            institutionalEmail = "andrei.ene@student.unitbv.ro";
            grupa =  "10LF222";
            gameDescription = "Damele este un joc de strategie pentru doi jucători, jucat pe o tablă de" +
                " 64 de pătrățele. Fiecare jucător își plasează piesele (damele) pe pătrățele de culoare închisă " +
                "și apoi le mișcă diagonal înainte pe diagonale libere, sărind peste piesele adverse pentru a le captura. " +
                "Scopul jocului este să capturezi toate piesele adversarului sau să-l împiedici să facă mutări valide. " +
                "Damele implică planificare strategică și anticipare pentru a obține avantajul și a câștiga";
        }

    }
}
