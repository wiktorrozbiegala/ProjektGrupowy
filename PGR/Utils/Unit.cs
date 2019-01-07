using PGRForms.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGRForms.Utils
{
    public static class Unit
    {
        public static string GetUnit(Param param)
        {
            switch (param)
            {
                case Param.AsuLevel:
                    return "[dBm]";
                case Param.CQI:
                    return "[dBm]";
                case Param.CellId:
                    return "";
                case Param.CellIdentity:
                    return "";
                case Param.MobileCountryCode:
                    return "";
                case Param.MobileNetworkCode:
                    return "";
                case Param.RSRP:
                    return "[dBm]";
                case Param.RSRQ:
                    return "[dBm]";
                case Param.SNR:
                    return "[dB]";
                case Param.TrackingAreaCode:
                    return "";
                default:
                    return "";
            }
        }
    }
}
