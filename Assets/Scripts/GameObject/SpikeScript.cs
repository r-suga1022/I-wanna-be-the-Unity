using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public Vector3 traptrigger_pos;
    public float traptrigger_dist;
    public GameObject Player;

    // 針が飛ぶタイプの罠だったときのフィールド
    public Vector3 velocity; // 針が飛んでいくときの速度
    public Vector3 destroypos; // 針オブジェクトを消す位置

    // 針が地面を反転するタイプの罠だったときのフィールド
    public Vector3 posdelta; // 反転した後の座標
    public Vector3 rotdelta; // 反転した後の回転角

    // 罠が発動するか否か
    public bool IsTriggered = false;
    public void SetIsTriggered(bool flag)
    {
        IsTriggered = flag;
    }

    // 罠のタイプ
    public enum TRAP_TYPE {Fly, Move};
    public TRAP_TYPE thistraptype;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 針を飛ばす
        if(IsTriggered)
        {
            Trap();
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        Debug.Log(other.gameObject.CompareTag("Player"));
        if(other.gameObject.CompareTag("Player"))
        {
            var status = other.gameObject.GetComponent<CharacterStatusScript>();
            status.Damage(50);
        }
    }


    // 罠を発動する関数
    private void Trap() {
        switch(thistraptype)
        {
            case TRAP_TYPE.Fly:
                transform.position += velocity;
                break;
            case TRAP_TYPE.Move:
                transform.position += posdelta;
                transform.rotation *= Quaternion.Euler(rotdelta);
                IsTriggered = !IsTriggered;
                break;
            default:
                break;
        }
    }
}
