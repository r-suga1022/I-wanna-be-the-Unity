using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTriggerScript : MonoBehaviour
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
        if(other.CompareTag("Player"))
        {
            GameObject parent = transform.parent.gameObject;
            var spikescript = parent.GetComponent<SpikeScript>();
            spikescript.SetIsTriggered(true);

            // トリガー自体は消す
            Destroy(this.gameObject);
        }
    }
}
