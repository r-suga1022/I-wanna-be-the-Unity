using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGeneratorScript : MonoBehaviour
{
    public PlayerScript playerscript;
    public GameObject PlayerBullet;

    public AudioClip sound1;
    AudioSource audiosource;

    public Vector3 bulletlocalpos; // 弾を作るローカル座標
    public float BulletVelocity; // 弾の速度

    private bool serialShootCommanded = false;
    public void setSerialShootCommanded(bool flag) {
        serialShootCommanded = flag;
    }

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z) || serialShootCommanded)
        {   
            Shoot();

            serialShootCommanded = false;
        }
    }

        // 弾を撃つ関数
    private void Shoot()
    {
        audiosource.PlayOneShot(sound1);

        var pos = transform.position + bulletlocalpos;
        var rot = transform.rotation;

        var velocity = BulletVelocity * playerscript.LookDirection;
        GameObject pb = Instantiate(PlayerBullet, pos, rot);
        var pbscript = pb.GetComponent<PlayerBulletScript>();
        pbscript.speedZ = velocity.z;
    }
}
