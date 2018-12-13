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
            var titleOX = "czas";
            switch (chartType)
            {
                case AvgParam.AsuLevel:
                    break;
                case AvgParam.CQI:
                    break;
                case AvgParam.RSRP:
                    break;
                case AvgParam.RSRQ:
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
