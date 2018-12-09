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
        private List<Measurement> _measurements;
        public DataCollector(List<Measurement> list)
        {
            _measurements = list;
        }
        // ona rzyga listy z parametrami, które potem izi wrzucić do charta i obliczyć z nich średnie itp
        public List<int> RetrieveData(Param param)
        {
            switch (param)
            {
                case Param.SNR:
                    return _measurements.Select(x => x.SNR).ToList();
                case Param.SignalStrengthdBm:
                    return _measurements.Select(x => x.SignalStrengthdBm).ToList();
                case Param.RSRP:
                    return _measurements.Select(x => x.RSRP).ToList();
                case Param.RSRQ:
                    return _measurements.Select(x => x.RSRQ).ToList();
                case Param.CQI:
                    return _measurements.Select(x => x.CQI).ToList();
                case Param.AsuLevel:
                    return _measurements.Select(x => x.AsuLevel).ToList();
                default:
                    throw new Exception("UZUPELNIJ TE LISTE O PARAMETR");
            }            
        }

        
    }
}
