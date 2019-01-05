using PGRForms.Database;
using PGRForms.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PGRForms
{
    public class DataCollector
    {
        private List<BaseMeasurement> _measurements;
        public DataCollector(List<BaseMeasurement> list)
        {
            _measurements = list;
        }
        
        public List<int> RetrieveData(AvgParam param)
        {
            switch (param)
            {
                case AvgParam.SNR:
                    return _measurements.Select(x => x.SNR).ToList();
                case AvgParam.RSRP:
                    return _measurements.Select(x => x.RSRP).ToList();
                case AvgParam.RSRQ:
                    return _measurements.Select(x => x.RSRQ).ToList();
                case AvgParam.CQI:
                    return _measurements.Select(x => x.CQI).ToList();
                case AvgParam.AsuLevel:
                    return _measurements.Select(x => x.AsuLevel).ToList();
                default:
                    throw new Exception("UZUPELNIJ TE LISTE O PARAMETR");
            }            
        }

        
    }
}
