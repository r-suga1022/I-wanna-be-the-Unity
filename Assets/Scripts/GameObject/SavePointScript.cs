using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavePointScript : MonoBehaviour
{
    public bool isSaveinWarp = false;

    public GameManagerScript gms;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 player_pos;
            if (isSaveinWarp) {
                player_pos = Vector3.zero;
            } else {
                player_pos = other.transform.position;
            }

            // GameManagerScriptクラスのSCENE配列の添え字番号を入れる
            int currentscene_index = (int)GameManagerScript.current_scene;
            string playerpos_string = currentscene_index.ToString() + "," + player_pos.x + "," + player_pos.y + "," + player_pos.z;
            
            File.WriteAllText(@"PlayerPositionSave.txt", playerpos_string);

            Debug.Log(playerpos_string);
        }
    }
}
