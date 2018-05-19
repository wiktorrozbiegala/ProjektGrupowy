using PGRForms.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGRForms
{
    public class DataModifications
    {
        FirebaseConnection fc = new FirebaseConnection();
        
        // ona rzyga listy z parametrami, które potem izi wrzucić do charta i obliczyć z nich średnie itp
        public List<int> ArrangeData(string param)
        {
            List<Measurement> parameters = fc.GetSingleSessionMeas("-LB8eNjME3_jibkajhcw");

            //tu taki tajm roboczy zeby ladnie wyswietlac, zalozony timestamp 1s
            if (param == "time")
            {
                List<int> time = Enumerable.Range(1, parameters.Count).ToList();
                return time;
            }
            if (param == "snr")
            {
                var snr = parameters.Select(x => x.SNR).ToList();
                return snr;
            }
            if (param == "SignalStrengthdBm")
            {
                var SignalStrengthdBm = parameters.Select(x => x.SignalStrengthdBm).ToList();
                return SignalStrengthdBm;
            }
            
            return null;
        }

        
    }
}
