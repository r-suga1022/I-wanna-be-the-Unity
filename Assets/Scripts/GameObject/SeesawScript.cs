using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawScript : MonoBehaviour
{
    public SerialReceive serialreceive_;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z_angle = serialreceive_.getAcceleValue();
        Quaternion rot_vec = this.transform.rotation;
        transform.rotation = rot_vec * Quaternion.AngleAxis(z_angle, Vector3.left);
        Debug.Log(z_angle);
    }
}
