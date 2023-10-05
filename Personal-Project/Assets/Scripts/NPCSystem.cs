using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NPCSystem : MonoBehaviour
{
    public GameObject d_template;
    public GameObject canva;
    bool player_detection = false;  

    // Update is called once per frame
    void Update()
    {
        if(player_detection && Input.GetMouseButtonDown(0) && !PlayerController.dialouge)
        {
            canva.SetActive(true);
            PlayerController.dialouge = true;
            NewDialouge("Yo yo yo!");
            NewDialouge("It's me, mr Scott!");
            NewDialouge("This is just a test.");
            NewDialouge("Bazinga!");
            canva.transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    void NewDialouge(string text)
    {
        //Continue from 1:32 on video vv
        /* https://www.youtube.com/watch?v=a9bhK-P7r-0&ab_channel=KaiJPR */
        GameObject template_clone = Instantiate(d_template, d_template.transform);
        template_clone.transform.parent = canva.transform;
        template_clone.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = text;
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
