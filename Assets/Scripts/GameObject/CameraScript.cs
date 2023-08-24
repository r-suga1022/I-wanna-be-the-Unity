using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject Player;
    
    // Start is called before the first frame update
    void Start()
    {
        float y = Player.transform.position.y;
        float z = Player.transform.position.z;
        Vector3 pos = transform.position;

        pos.y = y;
        pos.z = z;

        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        float y = Player.transform.position.y;
        float z = Player.transform.position.z;
        Vector3 pos = transform.position;

        pos.y = y;
        pos.z = z;

        transform.position = pos;
    }
}
