using PGRForms.Measurement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PGRForms.Utils
{
    public static class DataUtils
    {
        public static void ValidateData(this Dictionary<string, BaseMeasurement> data)
        {
            foreach (var item in data)
            {
                if (item.Value.CQI > 100)
                {
                    item.Value.CQI = 0;
                }
            }
        }
    }
}
