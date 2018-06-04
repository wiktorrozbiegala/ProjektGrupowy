using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGRForms
{
    
    /// zmienilem na inty bo tak prościej chyba, chart przyjmuje inty, average liczymy z intów, to pozmieniałem wszedzie
    public class Measurement
    {
        public int AsuLevel { get; set; }
        public int CQI { get; set; }
        public int CellId { get; set; }
        public int CellIdentity { get; set; }
        public int MobileCountryCode { get; set; }
        public int MobileNetworkCode { get; set; }
        public int RSRP { get; set; }
        public int RSRQ { get; set; }
        public int SNR { get; set; }
        public int SignalStrengthdBm { get; set; }
        public int TrackingAreaCode { get; set; }
    }

    public enum Param
    {
        AsuLevel,
        CQI ,
        CellId ,
        CellIdentity ,
        MobileCountryCode ,
        MobileNetworkCode ,
        RSRP ,
        RSRQ ,
        SNR ,
        SignalStrengthdBm ,
        TrackingAreaCode
    }
}
