using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftAppearTrrigerScript : MonoBehaviour
{
    public int child_number;
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
        var appearobject = transform.parent.transform.GetChild(child_number).gameObject;
        Debug.Log(appearobject.name);
        appearobject.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
