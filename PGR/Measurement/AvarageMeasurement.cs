namespace PGRForms.Measurement
{
   public class AvarageMeasurement
    {
        public double AsuLevel { get; set; }
        public double CQI { get; set; }
        public double RSRP { get; set; }
        public double RSRQ { get; set; }
        public double SNR { get; set; }
    }

    public enum AvgParam
    {
        AsuLevel,
        CQI,
        RSRP,
        RSRQ,
        SNR
    }
}
