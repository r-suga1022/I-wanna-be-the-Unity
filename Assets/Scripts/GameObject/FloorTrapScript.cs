using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrapScript : MonoBehaviour
{
    public AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioSource.PlayOneShot(sound1);
            //Destroy(gameObject);
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
