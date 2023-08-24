using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameManagerScript : MonoBehaviour
{
    public SerialHandler serialhandler_;
    public enum GAME_STATUS { Play, Clear, GameOver };
    public static GAME_STATUS status;

    public enum SCENES { TitleScene, GameSelectScene, Stage_start, Stage_sky, Stage_sunset, Stage_universe, ClearScene};
    public static SCENES current_scene;

    public GameObject ClearUI, GameOverUI;

    public bool IsWarpUsed = true;

    // Start is called before the first frame update
    void Start()
    {
        string scenename = SceneManager.GetActiveScene().name;
        current_scene = (SCENES)Enum.Parse(typeof(SCENES), scenename);
        status = GAME_STATUS.Play;
    }

    // Update is called once per frame
    void Update()
    {
        switch (status) 
        {
            case GAME_STATUS.Clear:
                //ClearUI.SetActive(true);
                break;
            case GAME_STATUS.GameOver:
                GameOverUI.SetActive(true);
                break;
            default:
                break;
        }

        // Rキーを押されたらリスタート
        if(Input.GetKey(KeyCode.R)) 
        {
            Restart();    
        }

        // クリアしたら、次のシーンに移る
        if (status == GAME_STATUS.Clear)
        {
            MoveNextStage((int)SCENES.ClearScene);
            // もしくは、タイトルに戻る
            //BackToTitle();
        }
        // Escキーを押されたら、画面を閉じる
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            serialhandler_.Close();
            Application.Quit();
        }
    }


    // ゲームをリスタートする
    void Restart() 
    {
        status = GAME_STATUS.Play;
        // セーブファイルからステージ名を読み込む
        string startPosString = File.ReadAllText("PlayerPositionSave.txt");
        string[] startPosString_split = startPosString.Split(',');
        int scene = int.Parse(startPosString_split[0]);
        Debug.Log(scene);
        current_scene = (SCENES)scene;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(scene);
    }

    // 次のシーンに移る
    // ワープを使うことでも
    public void MoveNextStage(int scenenumber)
    {
        SCENES nextscene = (SCENES)scenenumber;
        current_scene = nextscene;
        Debug.Log("current_scene = "+nextscene);
        SceneManager.LoadScene(nextscene.ToString());
    }

    // タイトルに戻る
    public void BackToTitle()
    {
        bool CanMove = (status == GAME_STATUS.Clear);
        // クリアした後でBキーを押されたら、タイトルに戻る
        if(CanMove && Input.GetKeyDown(KeyCode.B)) 
        {
            SceneManager.LoadScene(SCENES.TitleScene.ToString());
        }
    }
}
