using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {
    public int AliveTime = 60; // 弾の生存フレーム数
    int currentTime; // 生まれてからの経過時間

	public Quaternion firstRotation;
	public float speedZ;
    Vector3 movementSpeed;

	void Start ()
    {
        currentTime = 0;

        var pos = transform.position;
		//firstRotation = transform.rotation;	
		
		//movementSpeed = firstRotation + movementSpeed;
		
        //pos += movementSpeed;
        //transform.position = pos;
	}

	void Update ()
    {
        movementSpeed = new Vector3(0, 0, speedZ);
        ++currentTime;

        if(currentTime < AliveTime) {
            var pos = transform.position;
            pos += movementSpeed;
            transform.position = pos;
        } else {
            Destroy(this.gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var characterstatus = other.GetComponent<CharacterStatusScript>();
            characterstatus.Damage(10);
        }
    }
}