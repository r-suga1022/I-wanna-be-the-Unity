using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrapScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var status = other.GetComponent<CharacterStatusScript>();
            status.Damage(10);
        }
    }
}
