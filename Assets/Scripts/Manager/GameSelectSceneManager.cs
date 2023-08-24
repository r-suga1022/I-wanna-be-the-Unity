using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameSelectSceneManager : MonoBehaviour
{
    public GameManagerScript game_manager_script;
    public GameManagerScript.SCENES NextScene;

    public enum GAME_TYPE {NewGame, LoadGame};
    //private int whichgame_index = 1;
    public GAME_TYPE whichgame = GAME_TYPE.LoadGame;

    public Text[] gametype_text = new Text[2];

    private bool isSelectChanged = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (whichgame == GAME_TYPE.NewGame)
            {
                // セーブデータをリセットし、ゲームを新しくする
                Vector3 player_pos = Vector3.zero;
                string playerpos_string = ((int)NextScene).ToString() + "," + player_pos.x + "," + player_pos.y + "," + player_pos.z;
                File.WriteAllText(@"PlayerPositionSave.txt", playerpos_string);

                game_manager_script.MoveNextStage((int)NextScene);
            }
            else if (whichgame == GAME_TYPE.LoadGame)
            {
                // セーブデータを読み込んで、ゲームをロードする
                string startPosString = File.ReadAllText("PlayerPositionSave.txt");
                string[] startPosString_split = startPosString.Split(',');
                int NextSceneNumber = int.Parse(startPosString_split[0]);

                game_manager_script.MoveNextStage(NextSceneNumber);
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if((int)whichgame <= 0) 
            {
                whichgame += 1;
                isSelectChanged = true;
            }
                
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if((int)whichgame >= 1)
            {
                whichgame -= 1;
                isSelectChanged = true;
            }
        }

        if (isSelectChanged)
        {
            Text selected_text = gametype_text[(int)whichgame];
            selected_text.GetComponent<Shadow>().enabled = true;

            Text unselected_text = gametype_text[((int)whichgame+1)%2];
            unselected_text.GetComponent<Shadow>().enabled = false;

            isSelectChanged = false;
        }
    }

    
}
