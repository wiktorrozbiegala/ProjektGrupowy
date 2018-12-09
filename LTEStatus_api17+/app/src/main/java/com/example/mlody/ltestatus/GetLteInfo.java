package com.example.mlody.ltestatus;

import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.os.Build;
import android.support.v4.app.ActivityCompat;
import android.support.v4.content.ContextCompat;
import android.telephony.*;
import java.util.List;
import java.util.Date;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.time.LocalTime;

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

    public int CellIdentity = 0;
    public int MobileCountryCode = 0;
    public int MobileNetworkCode = 0;
    public int CellId = 0;
    public int TrackingAreaCode = 0;
    public int AsuLevel = 0;
    public int CQI = 0;
    public int SNR = 0;
    public int SignalStrengthdBm = 0;
    public int RSRP = 0;
    public int RSRQ = 0;
    public String timestamp;

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
            System.out.println("access ok");
        }
        counter++;
        if (listCellInfo != null) {
            for (CellInfo a_Info : listCellInfo) {
                if (CellInfoLte.class.isInstance(a_Info)) {
                    CellInfoLte cellInfoLte = (CellInfoLte) a_Info;
                    CIL = cellInfoLte.getCellIdentity();
                    CSS = cellInfoLte.getCellSignalStrength();
                    if (a_Info.isRegistered()) {
                        CellIdentity = CIL.getCi();
                        MobileCountryCode = CIL.getMcc();
                        MobileNetworkCode = CIL.getMnc();
                        CellId = CIL.getPci();
                        TrackingAreaCode = CIL.getTac();
                        AsuLevel = CSS.getAsuLevel();
                        SignalStrengthdBm = CSS.getDbm();
                        if(Build.VERSION.SDK_INT >= Build.VERSION_CODES.O) {
                            CQI = CSS.getCqi();
                            SNR = CSS.getRssnr();
                            RSRP = CSS.getRsrp();
                            RSRQ = CSS.getRsrq();
                        }
                        else{
                            String signalStr = signalStrength.toString();
                            String[] partsOfSignalStr = signalStr.split(" ");
                            CQI = Integer.parseInt(partsOfSignalStr[12]);
                            SNR = Integer.parseInt(partsOfSignalStr[11]);
                            RSRP = Integer.parseInt(partsOfSignalStr[9]);
                            RSRQ = Integer.parseInt(partsOfSignalStr[10]);
                        }
                        pushToFirebase(counter, generatedKey);
                    }
                }
            }
        }

    }

    public void pushToFirebase(int counter, String key){
        NetworkParams networkInfo = new NetworkParams(CellIdentity, MobileCountryCode, MobileNetworkCode, CellId, TrackingAreaCode, AsuLevel, CQI, SNR, SignalStrengthdBm, RSRP, RSRQ);
        myRef.child("networkInfo_api17").child(key).child(Integer.toString(counter)).setValue(networkInfo);
        myRef.child("networkInfo_api17").child(key).child(Integer.toString(counter)).orderByValue();
        System.out.println(CellIdentity + " " + MobileCountryCode + " " + MobileNetworkCode + " " + CellId + " " + TrackingAreaCode + " " + AsuLevel + " " + CQI + " " + SNR + " " + SignalStrengthdBm + " " + RSRP + " " + RSRQ);
    }

    public static String generateKey(){
        DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd'T'HH:mm:ss");
        Date date = new Date();
        String key = dateFormat.format(date);
        return key;
    }
}
