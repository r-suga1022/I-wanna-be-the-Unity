using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour {
    public int AliveTime = 60; // 弾の生存フレーム数
    int currentTime; // 生まれてからの経過時間

	public Quaternion firstRotation;
    public Vector3 movementSpeed;
	public float speedZ = 0.3f;

	void Start ()
    {
        currentTime = 0;

        var pos = transform.position;
		//firstRotation = transform.rotation;	
		movementSpeed = new Vector3(0, 0, speedZ);
		//movementSpeed = firstRotation + movementSpeed;
		
        pos += movementSpeed;
        transform.position = pos;
	}

	void Update ()
    {
        ++currentTime;

        if(currentTime < AliveTime) {
            var pos = transform.position;
            pos += movementSpeed;
            transform.position = pos;
        } else {
            Destroy(this.gameObject);
        }
	}
}