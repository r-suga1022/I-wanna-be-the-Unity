using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーは初期位置にワープさせる
        // それ以外のオブジェクトは破壊する
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.gameObject.GetComponent<PlayerScript>();
            //player.MoveStartPos();
            CharacterStatusScript c_status = other.gameObject.GetComponent<CharacterStatusScript>();
            c_status.Damage(100);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}