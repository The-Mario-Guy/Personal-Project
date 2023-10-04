using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPCSystem : MonoBehaviour
{
    bool player_detection = false;  

    // Update is called once per frame
    void Update()
    {
        if(player_detection && Input.GetMouseButtonDown(0) && !PlayerController.dialouge)
        {
            PlayerController.dialouge = true;
        }
    }
    void NewDialouge(string text)
    {
        //Continue from 1:32 on video vv
        /* https://www.youtube.com/watch?v=a9bhK-P7r-0&ab_channel=KaiJPR */
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player" && !PlayerController.dialouge)
        {
            player_detection = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        player_detection = false;
    }
}
