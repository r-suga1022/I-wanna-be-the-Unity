using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBeginTriggerScript : MonoBehaviour
{
    BombScript bombscript;
    // Start is called before the first frame update
    void Start()
    {
        bombscript = this.transform.parent.GetComponent<BombScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            bombscript.SetCanShoot(true);
        }
    }
}
