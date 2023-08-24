using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharacterStatusScript : MonoBehaviour
{
    Animator anim;
    public UnityEvent onDieCallback = new UnityEvent();


    public AudioSource audiosource; // 死亡の効果音
    public AudioClip damage_se;
    public AudioClip death_se;

    public int life = 100;

    private bool IsDamaged = false;
    public void setIsDamaged(bool flag) {
        IsDamaged = flag;
    }
    public bool getIsDamaged() {
        return IsDamaged;
    }

    public Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        audiosource = GetComponent<AudioSource>();

        if(hpBar != null)
        {
            hpBar.value = life;
        }
    }

    // ダメージを受ける時の処理
    public void Damage(int damage)
    {
        if(life <= 0) return;

        life -= damage;
        IsDamaged = true;
        if(hpBar != null)
        {
            hpBar.value = life;
            audiosource.PlayOneShot(death_se);

        }
        if(life <= 0)
        {
            OnDie();
        }
    }

    // ゲームクリア時の処理
    public void Clear()
    {
        GameManagerScript.status = GameManagerScript.GAME_STATUS.Clear;

        // カメラの向きを基準にした正面方向のベクトル
        /*
        Vector3 cameraPos = Camera.main.transform.position;
        transform.LookAt(-cameraPos);
        anim.SetBool("Clear", true);
        */
    }

    // 死亡するときの処理
    void OnDie()
    {
        GameManagerScript.status = GameManagerScript.GAME_STATUS.GameOver;

        // 死亡するときの効果音を流す
        audiosource.PlayOneShot(death_se);
        
        anim.SetBool("Die", true);
        onDieCallback.Invoke();
    }
}
