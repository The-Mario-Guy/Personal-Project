using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSystem : MonoBehaviour
{
    bool player_detection = false;  

    // Update is called once per frame
    void Update()
    {
        if(player_detection && Input.GetMouseButtonDown(0))
        {
            print("The Talker");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            player_detection = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
    }
}
