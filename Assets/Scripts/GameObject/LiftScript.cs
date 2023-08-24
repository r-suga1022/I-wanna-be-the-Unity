using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    // 動く範囲
    public Vector3 Movepos1;
    public Vector3 Movepos2;
    public Vector3 deltaMovepos;
    public float BackTriggerDistance; // 戻り始めるときのposとの距離

    private Vector3 Velocity; // 速度
    public int MoveFrame; // 片道行くときのフレーム数

    private bool IsBack; // 復路中か

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Activated\n");
        Movepos1 = transform.localPosition + deltaMovepos/2;
        Movepos2 = Movepos1 - deltaMovepos;
        Velocity = deltaMovepos / MoveFrame;
        IsBack = false;
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.localPosition;

        // 切り返すときの処理
        if (!IsBack && Vector3.Distance(Movepos1, pos) < BackTriggerDistance) {
            Velocity = -Velocity;
            IsBack = !IsBack;
        } else if(IsBack && Vector3.Distance(Movepos2, pos) < BackTriggerDistance) {
            Velocity = -Velocity;
            IsBack = !IsBack;
        }
        pos += Velocity;
        transform.localPosition = pos;
    }
}