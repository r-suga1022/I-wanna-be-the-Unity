using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    Animator anim;
    
    public GameObject enemybullet;

    public int ShootIntvl; // 弾を撃つ間隔（フレーム数）
    public int SinceShoot; // 弾を撃ってから何フレーム経過したか
    public Vector3 bulletlocalpos; // 弾を作るローカル座標
    public float BulletVelocity; // 弾の速度

    public bool CanShoot = false; // 撃つか否かのフラグ
    public void SetCanShoot(bool flag) { CanShoot = flag; }


    // 効果音関連
    public AudioClip sound1;
    AudioSource audiosource;

    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        // 弾を撃つ
        if (CanShoot) Shoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManagerScript.status != GameManagerScript.GAME_STATUS.Play)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            transform.LookAt(other.gameObject.transform);
            other.gameObject.transform.LookAt(transform);
            anim.SetBool("walk", false);
            anim.SetTrigger("atack01");

            var con = other.GetComponent<CharacterStatusScript>();
            con.Damage(100);
        }
    }


    // 弾を撃つ関数
    private void Shoot()
    {
        ++SinceShoot;

        if (SinceShoot >= ShootIntvl)
        {
            audiosource.PlayOneShot(sound1);

            SinceShoot = 0;

            var pos = transform.position + bulletlocalpos;
            var rot = transform.rotation;
            GameObject eb = Instantiate(enemybullet, pos, rot);
            var ebscript = eb.GetComponent<EnemyBulletScript>();
            ebscript.speedZ = BulletVelocity;
        }
    }
}
