using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGRForms
{
    /// <summary>
    /// Consists of all params from database
    /// </summary>
    public class Measurement
    {
        public string AsuLevel { get; set; }
        public string CQI { get; set; }
        public string CellId { get; set; }
        public string CellIdentity { get; set; }
        public string MobileCountryCode { get; set; }
        public string MobileNetworkCode { get; set; }
        public string RSRP { get; set; }
        public string RSRQ { get; set; }
        public string SNR { get; set; }
        public string SignalStrengthdBm { get; set; }
        public string TrackingAreaCode { get; set; }
    }
}
