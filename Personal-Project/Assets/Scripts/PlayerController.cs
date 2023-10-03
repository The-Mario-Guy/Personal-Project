using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    public float MovementSpeed =3;
    public float RunningSpeed = 5;
    public float Gravity = 9.8f;
    private float velocity = 0;
    private float _totalStamina = 100;
    public bool _isRunning = false;

    private Camera cam;
    public bool isFading;
    private Animator _Fade;
    [SerializeField] private Slider _stamina;
 
    private void Start()
    {
        cam = Camera.main;
        characterController = GetComponent<CharacterController>();
        _Fade = GetComponent<Animator>();
        _stamina = GameObject.Find("Stamina Bar").GetComponent<Slider>();
    }
 
    void Update()
    {
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);
        _stamina.value = _totalStamina;
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
            isRunning();
            updateStaminaBar(-2);
        }
        else
        {
            stopRunning();
            
        }
        StaminaRegenRoutine();
    }

    private void isRunning()
    {
       _isRunning = true;
        MovementSpeed = RunningSpeed;
    }
    private void stopRunning()
    {
        _isRunning = false;
        MovementSpeed = 3;
       
    }
    public void updateStaminaBar(float currentStamina)
    {
        if (_totalStamina - currentStamina <= 0)
        {
            stopRunning();
        }
        _totalStamina += currentStamina;
    }
    //Decreases Stamina Bar
    IEnumerator StaminaRoutine()
    {
        while(_isRunning == true)
        {
            updateStaminaBar(-20);
            yield return new WaitForSeconds(1.0f);
        }
    }
    //Increases Stamina Bar
    IEnumerator StaminaRegenRoutine()
    {
        while (_isRunning == false) 
        {
            updateStaminaBar(+20);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
