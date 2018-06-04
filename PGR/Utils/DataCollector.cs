using PGRForms.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGRForms
{
    public class DataCollector
    {
        FirebaseConnection fc = new FirebaseConnection();
        private List<Measurement> _messurements;
        public DataCollector(List<Measurement> list)
        {
            _messurements = list;
        }
        // ona rzyga listy z parametrami, które potem izi wrzucić do charta i obliczyć z nich średnie itp
        public List<int> RetrieveData(Param param)
        {
            switch (param)
            {
                case Param.SNR:
                    return _messurements.Select(x => x.SNR).ToList();
                case Param.SignalStrengthdBm:
                    return _messurements.Select(x => x.SignalStrengthdBm).ToList();
                default:
                    throw new Exception("UZUPELNIJ TE LISTE O PARAMETR");
            }            
        }

        
    }
}
