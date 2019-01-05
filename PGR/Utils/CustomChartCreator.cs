using PGRForms.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGRForms.Utils
{
    public static class CustomChartCreator
    {
        public static CustomChart Create(AvgParam chartType)
        {
            var titleOY = "";
            var titleOX = "sample";
            switch (chartType)
            {
                case AvgParam.AsuLevel:
                    titleOY = "[dB]";
                    break;
                case AvgParam.CQI:
                    break;
                case AvgParam.RSRP:
                    titleOY = "[dBm]";
                    break;
                case AvgParam.RSRQ:
                    titleOY = "[dBm]";
                    break;
                case AvgParam.SNR:
                    titleOY = "[dB]";
                    break;
                default:
                    break;
            }

            return new CustomChart
            {
                Name = chartType,
                TitleOX = titleOX,
                TitleOY = titleOY
            };
        }
    }
}
