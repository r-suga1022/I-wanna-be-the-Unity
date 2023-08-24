using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDestroyAreaScript : MonoBehaviour
{
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
        if(other.CompareTag("Spikes"))
        {
            Destroy(other);
            Debug.Log("Spike was destroyed\n");
        }
    }
}
