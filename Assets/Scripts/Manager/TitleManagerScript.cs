using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManagerScript : MonoBehaviour
{
    public GameManagerScript.SCENES NextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Nキーを押されたら、次のシーンに遷移
        if(Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(NextScene.ToString());
        }
        // Escキーを押されたら、画面を閉じる
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
