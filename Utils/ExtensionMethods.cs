using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace PGRForms.Utils
{
    public static class ExtensionMethods
    {
        public static ObservableCollection<X> ToParams(this Measurement obj)
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
