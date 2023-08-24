using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpScript : MonoBehaviour
{
    public GameManagerScript game_manager_script;
    public GameManagerScript.SCENES destination;
    public Vector3 afterwarppos; // ワープした後のプレイヤーの座標

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            game_manager_script.IsWarpUsed = true;
            game_manager_script.MoveNextStage((int)destination);
            //other.transform.position = afterwarppos;
            //Debug.Log("move\n");
        }
    }
}
