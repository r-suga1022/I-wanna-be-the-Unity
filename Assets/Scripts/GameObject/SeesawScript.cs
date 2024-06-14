using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesawScript : MonoBehaviour
{
    public SerialReceive serialreceive_;
    public bool hasController;
    public float degreePerFrame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float z_angle;
        if (hasController) z_angle = serialreceive_.getAcceleValue();
        else z_angle = degreePerFrame;

        Quaternion rot_vec = this.transform.rotation;
        transform.rotation = rot_vec * Quaternion.AngleAxis(z_angle, Vector3.left);
        Debug.Log(z_angle);
    }
}
