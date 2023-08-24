using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchScript : MonoBehaviour
{
    public enum Switchtype {HeavyGravity, LightGravity}; // スイッチ効果のタイプ
    public Switchtype thistype; // このスイッチのタイプ

    GameObject triggeredthing; // トリガーを起こしたオブジェクト

    public GameObject heavygravitytext;
    public GameObject lightgravitytext;

    GameObject ActiveText; // アクティブになっているテキスト

    public int g_heavy;
    public int g_light;

    float bottomY = -0.15f;
    float speed = 0.5f;
    public float deltaY = 2.0f;
    float defaultY;
    float currentY;

    bool active;
    
    public DoorScript door;

    void Start()
    {
        defaultY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        currentY = transform.position.y;
        float differenceY = defaultY - currentY;
        if(active && differenceY <= deltaY) {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        } else if (active && differenceY >= deltaY) {
            // 効果を起こす
            make_effect();
            active = !active;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            active = true;
            triggeredthing = other.gameObject;
        }
    }

    private void make_effect()
    {
        if(ActiveText != null) {
            Debug.Log("not null");
            ActiveText.gameObject.SetActive(false);
        }

        if(thistype == Switchtype.HeavyGravity) {
            var pscript = triggeredthing.GetComponent<PlayerScript>();   
            pscript.gravity = g_heavy;

            heavygravitytext.gameObject.SetActive(true);
            ActiveText = heavygravitytext;
        } else if(thistype == Switchtype.LightGravity) {
            var pscript = triggeredthing.GetComponent<PlayerScript>();   
            pscript.gravity = g_light;

            lightgravitytext.gameObject.SetActive(true);
            ActiveText = lightgravitytext;
        }
    }
}
