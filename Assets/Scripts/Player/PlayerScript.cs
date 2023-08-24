using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.IO;
 
public class PlayerScript : MonoBehaviour
{
    public GameManagerScript game_manager_script;
 
    public CharacterController con;
    Animator anim;

    public bool IsWarpUsed = false;

    public Vector3 LookDirection = new Vector3(0, 0, 1);
 
    float normalSpeed = 5f; // 通常時の移動速度
    float sprintSpeed = 5f; // ダッシュ時の移動速度
    float jump = 5f;        // ジャンプ力
    public float gravity = 16f;    // 重力の大きさ
    Vector3 moveDirection = Vector3.zero;
    private int jumpcount = 0; // 2段ジャンプのうち、何段目かを数える
    private int jumpcountmax = 2; // 最大２段ジャンプまで可能（この数値は可変、無限ジャンプに対応できるか)
 
    Vector3 startPos;

    CharacterStatusScript status;

    public AudioSource audiosource;
    public AudioClip jump_se;

    // シリアル通信でジャンプボタンが押されたか
    bool serialJumpCommanded = false;
    public void setSerialJumpCommanded(bool flag) {
        serialJumpCommanded = flag;
    }
    // シリアル通信でジョイスティックが動かされたか
    float serialMoveCommanded = 0;
    public void setSerialMoveCommanded(float value) {
        serialMoveCommanded = value;
    }
    float serialcoefficient = 0.0f;
 
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        con = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        status = GetComponent<CharacterStatusScript>();
 
        // マウスカーソルを非表示にし、位置を固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       
       // ワープを使ってステージを読み込んだときは、原点からスタート
       // 一度死んでリスタートするときは座標をセーブデータから読み込む
        if (!game_manager_script.IsWarpUsed) {
            // セーブデータをテキストファイルから読み込み、プレイヤーの初期座標とする
            string startPosString = File.ReadAllText("PlayerPositionSave.txt");
            string[] startPosString_split = startPosString.Split(',');
            startPos.x = float.Parse(startPosString_split[1]);
            startPos.y = float.Parse(startPosString_split[2]);
            startPos.z = float.Parse(startPosString_split[3]);
            transform.position = startPos;
        } else {
            transform.position = Vector3.zero;
            game_manager_script.IsWarpUsed = !game_manager_script.IsWarpUsed;
            Debug.Log(transform.position);
        }
    }
 
    void Update()
    {
        
        // 死んだり、クリアしたら制御不能にする
        if(status.life <= 0 || GameManagerScript.status == GameManagerScript.GAME_STATUS.Clear)
        {
            return;
        }
        
        // 移動速度を取得
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;
         
        // カメラの向きを基準にした正面方向のベクトル
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward,new Vector3(1, 0, 1)).normalized;
 
        // 前後左右の入力（WASDキー）から、移動のためのベクトルを計算
        // Input.GetAxis("Vertical") は前後（WSキー）の入力値
        // Input.GetAxis("Horizontal") は左右（ADキー）の入力値

        float IsMove = Input.GetAxis("Horizontal");
        float MoveValue = IsMove + serialMoveCommanded * serialcoefficient;

        if (serialcoefficient < 1.0f && serialcoefficient >= 0.0f) serialcoefficient += 0.1f;
        else if (serialcoefficient <= 0.0f && serialcoefficient > -1.0f) serialcoefficient -= 0.1f;
        if (serialMoveCommanded == 0.0f) serialcoefficient = 0.0f;
        
        //float MoveValue = IsMove;

        //Vector3 moveZ = cameraForward * MoveValue * speed;  //　前後（カメラ基準）　 
        Vector3 moveZ = Vector3.zero;
        Vector3 moveX = Camera.main.transform.right * MoveValue * speed; // 左右（カメラ基準）

        if (MoveValue > 0) {LookDirection.z = 1;}
        else if (MoveValue < 0) {LookDirection.z = -1;}


        // con.IsGrounded は地面にいるかどうかを判定します
        if (con.isGrounded)
        {
            //moveDirection = moveZ + moveX;
            moveDirection = moveX;
            jumpcount = 0;
        } else {
            // 重力を効かせる
            //moveDirection = moveZ + moveX + new Vector3(0, moveDirection.y, 0);
            if (GameManagerScript.status == GameManagerScript.GAME_STATUS.Play) {
                moveDirection = moveX + new Vector3(0, moveDirection.y, 0);
                moveDirection.y -= gravity * Time.deltaTime;
            }
        }
        // ジャンプを行う
        bool CanJump = con.isGrounded || (!con.isGrounded && (jumpcount < jumpcountmax));

        bool IsJumpCommanded = Input.GetKeyDown(KeyCode.Space) || serialJumpCommanded;

        if(IsJumpCommanded && CanJump)
        {
            audiosource.PlayOneShot(jump_se);
            moveDirection.y = jump;
            ++jumpcount;
            Debug.Log(jumpcount);
            Debug.Log("yes");

            serialJumpCommanded = false;
        }
 
        // 移動のアニメーション
        //anim.SetFloat("MoveSpeed", (moveZ + moveX).magnitude);
        anim.SetFloat("MoveSpeed", (moveZ + moveX).magnitude);
 
        // プレイヤーの向きを入力の向きに変更　
        transform.LookAt(transform.position + moveZ + moveX);
 
        // Move は指定したベクトルだけ移動させる命令
        con.Move(moveDirection * Time.deltaTime);
        //Debug.Log(transform.position);
    }

    // 指定地点に強制移動する
   public void MoveStartPos()
   {
       con.enabled = false;
 
       moveDirection = Vector3.zero;
       transform.position = startPos + Vector3.up * 10f;
       transform.rotation = Quaternion.identity;
 
       con.enabled = true;
   }

    float minimum = 0.0f;
    float t = 0.0f;
    float CalcJoystickValue(float value) {
        t += 0.1f * Time.deltaTime;
        float returnvalue = Mathf.Lerp(minimum, serialMoveCommanded, t);

        if (t > 1.0f) {
            t = 0.0f;
            //minimum = returnvalue;
        }
        return returnvalue;
    }
}