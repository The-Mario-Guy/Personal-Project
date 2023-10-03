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
    public float currentStamina;
    public bool _inTrigger = false;
    public GameObject defaultCursor;
    public GameObject interactCursor;
    public GameObject grabCursor;


    private Camera cam;
    [SerializeField] private Slider _stamina;
 
    private void Start()
    {
        cam = Camera.main;
        characterController = GetComponent<CharacterController>();
        _stamina = GameObject.Find("Stamina Bar").GetComponent<Slider>();
        normalCursor();
    }

    void Update()
    {
        // player movement - forward, backward, left, right
        float horizontal = Input.GetAxis("Horizontal") * MovementSpeed;
        float vertical = Input.GetAxis("Vertical") * MovementSpeed;
        characterController.Move((cam.transform.right * horizontal + cam.transform.forward * vertical) * Time.deltaTime);
        _stamina.value = _totalStamina;
        // Gravity
        if (characterController.isGrounded)
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
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning();
            
            updateStaminaBar(-1);

        }
        else
        {
            stopRunning();
            //Fix the (+1) as it seems to keep adding even after the bar is full
            updateStaminaBar(+1);
        }
        StaminaRegenRoutine();

    }

    private void isRunning()
    {
       _isRunning = true;
        MovementSpeed = 5;
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

   //This should be put in seperate scripts for each interactable thing, not built into the player controller, we don't want to be Yandre Dev 2 :<
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Scott")
        {
            Interactable();
            _inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        _inTrigger = false;
        normalCursor();
    }
    
    private void Interactable()
    {
        Debug.Log("You have entered the trigger for Scott");
        defaultCursor.gameObject.SetActive(false);
        interactCursor.gameObject.SetActive(true);
    }
    private void normalCursor()
    {

        defaultCursor.gameObject.SetActive(true);
        interactCursor.gameObject.SetActive(false);
        grabCursor.gameObject.SetActive(false);
    } 
}
