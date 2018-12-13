using PGRForms.Measurement;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System;

namespace PGRForms.Utils
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Convert Measurement class to Collection for populating list view
        /// </summary>
        /// <param name="obj">Class holding values</param>
        /// <returns>Collection of Param and its Value</returns>
        public static ObservableCollection<X> ToParams(this object obj)
        {
            PropertyInfo[] properties = obj.GetType().GetProperties();
            var list = new ObservableCollection<X>();
            foreach (PropertyInfo property in properties)
            {
                list.Add(new X
                {
                    Param = property.Name,
                    Value = property.GetValue(obj, null).ToString()
                });
            }
            return list;              
        }

        public static AvarageMeasurement CalculateAvg(this List<BaseMeasurement> listOfMeasurements)
        {
            var avarageValues = new AvarageMeasurement();
            var dc = new DataCollector(listOfMeasurements);

            avarageValues.AsuLevel = Math.Round(dc.RetrieveData(AvgParam.AsuLevel).Average(), 2);
            avarageValues.CQI = Math.Round(dc.RetrieveData(AvgParam.CQI).Average(), 2);
            avarageValues.RSRP = Math.Round(dc.RetrieveData(AvgParam.RSRP).Average(), 2);
            avarageValues.RSRQ = Math.Round(dc.RetrieveData(AvgParam.RSRQ).Average(), 2);
            avarageValues.SNR = Math.Round(dc.RetrieveData(AvgParam.SNR).Average(), 2);

            return avarageValues;
        }

    }

    public class X
    {
        public string Param { get; set; }
        public string Value { get; set; }
    }
    //private void GetParamsNames()
    //{
    //    PropertyInfo[] properties = typeof(Measurement).GetProperties();
    //    List<string> names = new List<string>();
    //    foreach (PropertyInfo property in properties)
    //    {
    //        names.Add(property.Name);
    //        object propertyValue = property.GetValue(obj, null);
    //    }
    //}
}
