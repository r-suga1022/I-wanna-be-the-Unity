using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialSend : MonoBehaviour
{
    //SerialHandler.cのクラス
    public SerialHandler serialHandler;

    public CharacterStatusScript characterstatus;

    int VibrationTimeCount = 0;

    void FixedUpdate() //ここは0.001秒ごとに実行される
    {
        //i = i + 1;   //iを加算していって1秒ごとに"1"のシリアル送信を実行
        bool IsDamaged = characterstatus.getIsDamaged();
        serialHandler.Write("1");
        if (IsDamaged) {
            // serialHandler.Write("1");
            // Debug.Log("sent\n");
            ++VibrationTimeCount;
            if (VibrationTimeCount == 10) {
                characterstatus.setIsDamaged(false);
                VibrationTimeCount = 0;
            }
        }
    }

    void WhenWantToSendMessage() {
        bool damageflag = characterstatus.getIsDamaged();
        if (damageflag) {

        }
    }
}