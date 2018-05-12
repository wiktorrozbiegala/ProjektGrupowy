package com.example.mlody.ltestatus;

import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.telephony.*;
import java.util.List;

public class GetLteInfo extends PhoneStateListener {

    public static boolean start = true;

    public GetLteInfo(Context context){
        tManager = (TelephonyManager) context.getSystemService(Context.TELEPHONY_SERVICE);
        startListening(start);
    }

    public static TelephonyManager tManager = null;
    public static List<CellInfo> listCellInfo = null;
    public static CellIdentityLte CIL;
    public static CellSignalStrengthLte CSS;

    public int CellIdentity;
    public int MobileCountryCode;
    public int MobileNetworkCode;
    public int CellId;
    public int TrackingAreaCode;
    public int AsuLevel;
    public int CQI;
    public int SNR;
    public int SignalStrengthdBm;
    public int RSRP;
    public int RSRQ;

    int counter = 0;

    private static FirebaseDatabase database = FirebaseDatabase.getInstance();
    public static DatabaseReference myRef = database.getReference();
    public static String generatedKey;

    public void startListening(boolean start) {
        if(start) {
            tManager.listen(this, PhoneStateListener.LISTEN_SIGNAL_STRENGTHS);
        }
    }

    @Override
    public void onSignalStrengthsChanged(SignalStrength signalStrength) {
        super.onSignalStrengthsChanged(signalStrength);

        if (ContextCompat.checkSelfPermission(MainActivity.mContext, android.Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            ActivityCompat.requestPermissions(MainActivity.mActivity, new String[]{Manifest.permission.ACCESS_COARSE_LOCATION}, MainActivity.MY_REQUEST_CODE);
        } else {
            GetLteInfo.listCellInfo = GetLteInfo.tManager.getAllCellInfo();
        }

        counter++;
        if (listCellInfo != null)
            for (CellInfo a_Info : listCellInfo) {
                if (CellInfoLte.class.isInstance(a_Info)) {
                    CellInfoLte cellInfoLte = (CellInfoLte) a_Info;
                    CIL = cellInfoLte.getCellIdentity();
                    CSS = cellInfoLte.getCellSignalStrength();
                    if(a_Info.isRegistered()) {
                        CellIdentity = CIL.getCi();
                        MobileCountryCode = CIL.getMcc();
                        MobileNetworkCode = CIL.getMnc();
                        CellId = CIL.getPci();
                        TrackingAreaCode = CIL.getTac();
                        AsuLevel = CSS.getAsuLevel();
                        CQI = CSS.getCqi();
                        SNR = CSS.getRssnr();
                        SignalStrengthdBm = CSS.getDbm();
                        RSRP = CSS.getRsrp();
                        RSRQ = CSS.getRsrq();
                        pushToFirebase(counter, generatedKey);
                    }
                }
            }
    }

    public void pushToFirebase(int counter, String key){
        NetworkParams networkInfo = new NetworkParams(CellIdentity, MobileCountryCode, MobileNetworkCode, CellId, TrackingAreaCode, AsuLevel, CQI, SNR, SignalStrengthdBm, RSRP, RSRQ);
        myRef.child("networkInfo").child(key).child(Integer.toString(counter)).setValue(networkInfo);
        myRef.child("networkInfo").child(key).child(Integer.toString(counter)).orderByValue();
    }

    public static String generateKey(){
        DatabaseReference pushedPostRef = myRef.push();
        String key = pushedPostRef.getKey();
        return key;
    }
}
