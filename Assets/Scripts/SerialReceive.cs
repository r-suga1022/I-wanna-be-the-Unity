using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerialReceive : MonoBehaviour
{
    public PlayerScript playerscript;
    public BulletGeneratorScript bulletgenerator;

    //https://qiita.com/yjiro0403/items/54e9518b5624c0030531
    //上記URLのSerialHandler.cのクラス
    public SerialHandler serialHandler;

    float ax, ay, az;
    float rSpeed = 50;
    private float accele_value = 0.0f;
    public float getAcceleValue() {
        return accele_value;
    }

    void Start()
    {
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
    }

    void Update()
    {
        Quaternion rotation = Quaternion.Euler(ax, az, ay);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, .25f);
    }

    //受信した信号(message)に対する処理
    void OnDataReceived(string message)
    {
        var data = message.Split(
                new string[] { "\n" }, System.StringSplitOptions.None);

        Debug.Log(data[0]);
        string[] data_split = data[0].Split(",");

        // ボタンの入力を受け付けたとき
        string[] jumpsegment = data_split[0].Split("-");
        int jumpvalue = int.Parse(jumpsegment[0]);
        int shootvalue = int.Parse(jumpsegment[1]);
        bool jumpflag = (jumpvalue == 1);
        bool shootflag = (shootvalue == 1);
        //Debug.Log(jumpflag);
        playerscript.setSerialJumpCommanded(jumpflag);
        bulletgenerator.setSerialShootCommanded(shootflag);

        // ジョイスティック入力を受け付けたとき        
        string horizontal_str = data_split[1];
        float horizontal_value = float.Parse(horizontal_str);
        playerscript.setSerialMoveCommanded(horizontal_value);
        
        // 加速度センサ入力を受け付けたとき
        string accele_str = data_split[2];
        accele_value = float.Parse(accele_str);
        Debug.Log(accele_value);
        
        try
        {
            //Debug.Log(data[0]);//Unityのコンソールに受信データを表示
            //Debug.Log(ax_str);
            //Debug.Log(ay_str);
            //Debug.Log(az_str);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);//エラーを表示
        }
    }
}