using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed =5;
    public float RunningSpeed = 7;
    public float Gravity = 9.8f;
    private float velocity = 0;

    private Camera cam;
    public bool isFading;
    private Animator _Fade;
 
    private void Start()
    {
        cam = Camera.main;
        characterController = GetComponent<CharacterController>();
        _Fade = GetComponent<Animator>();
    }
 
    void Update()
    {
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);
 
        // Gravity
        if(characterController.isGrounded)
        {
            velocity = 0;
        }
        else
        {
            velocity -= Gravity * Time.deltaTime;
            characterController.Move(new Vector3(0, velocity, 0));
        }
       //Relodes scene 
       //comment out for full release
        if(Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MovementSpeed = RunningSpeed;
        }
        else
        {
            MovementSpeed = 5;
        }
    }
   /* private void OnTriggerEnter(Collider collision) 
    {
       
        
    }  */

}
