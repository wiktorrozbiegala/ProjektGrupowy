package com.example.mlody.ltestatus;

public class NetworkParams {
    //CellIdentityLte
    public int CellIdentity;       //getCi()
    public int MobileCountryCode;   //getMcc()
    public int MobileNetworkCode;   //getMnc()
    public int CellId;          //getPci()
    public int TrackingAreaCode;       //getTac()
    //CellSignalStrength
    public int AsuLevel;       //getAsuLevel()
    public int CQI;        //getCqi()
    public int SNR;        //getRssnr()
    public int SignalStrengthdBm;      //getDbm()
    public int RSRP;    //getRsrp()
    public int RSRQ;       //getRsrq()
    //SubscriptionInfo

    public NetworkParams(int CellIdentity, int MobileCountryCode, int MobileNetworkCode,
                         int CellId, int TrackingAreaCode, int AsuLevel, int CQI, int SNR,
                         int RSRP, int RSRQ, int SignalStrengthdBm){
        this.CellIdentity = CellIdentity;
        this.MobileCountryCode = MobileCountryCode;
        this.MobileNetworkCode = MobileNetworkCode;
        this.CellId = CellId;
        this.TrackingAreaCode = TrackingAreaCode;
        this.AsuLevel = AsuLevel;
        this.CQI = CQI;
        this.SNR = SNR;
        this.RSRP = RSRP;
        this.RSRQ = RSRQ;
        this.SignalStrengthdBm = SignalStrengthdBm;
    }
}

