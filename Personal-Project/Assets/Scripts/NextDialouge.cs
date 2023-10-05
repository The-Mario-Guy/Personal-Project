using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDialouge : MonoBehaviour
{
    int index = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && transform.childCount >1 ) 
        {
            if(PlayerController.dialouge)
            {
                transform.GetChild(index).gameObject.SetActive(true);
                index += 1;
                if (transform.childCount == index)
                {
                    index = 2;
                    PlayerController.dialouge = false;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
