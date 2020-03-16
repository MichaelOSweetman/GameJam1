using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class BullController : MonoBehaviour
{
    //speed the bull charges at
    public float chargeSpeed = 10;

    //speed the bull rotates
    public float rotationSpeed = 1;

    public float turnSmoothTime = 0.1f;

    //is the bull charging
    public bool isCurrentlyCharging = false;

    private Vector3 myForward;

    private Transform myTransform;

    private Rigidbody myRigidbody;

    //this determines which controller is controlling the bull, it is set from a main menu button
    //set to hide in inspector
    public int controllerNumber;

    //my controller
    [HideInInspector]
    public XboxController myController;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = gameObject.GetComponent<Transform>();
        myRigidbody = gameObject.GetComponent<Rigidbody>();
        myController = (XboxController)controllerNumber;
    }

    // Update is called once per frame
    void Update()
    {
       
        //if you are not charging
        if(!isCurrentlyCharging)
        {
            //get the inputs
            bool aButton = XCI.GetButton(XboxButton.A, myController); // XCI.GetButton(XboxButton.A)

            float axisX = XCI.GetAxis(XboxAxis.LeftStickX, myController); //left stick x axis
            float axisY = XCI.GetAxis(XboxAxis.LeftStickY, myController); //left stick y axis

            //the player can use the left stick to turn the bull
            if(axisX != 0 || axisY != 0)
            {
                //math code to get rotation
                float targetRotation = Mathf.Atan2(axisX, axisY) * Mathf.Rad2Deg;
                
                //apply the rotation
                myTransform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationSpeed, turnSmoothTime);
            }

            //the player can press the A button to peform a charge
            if (aButton) //Input.GetKeyDown(KeyCode.Space)
            {
                isCurrentlyCharging = true;
            }
        }
       
        

        //if you are charging
        if (isCurrentlyCharging)
        {
            //get the forward vector of the bull
            myForward = myTransform.forward; 

            //multiply the forward vector by the charge speed
            myForward *= chargeSpeed;

            //add force to the bull
            myRigidbody.velocity = myForward;
        }
    }

    //bull collision (code for bull hitting player is handled in RunnerMovement Script)
    void OnCollisionEnter(Collision collision)
    {
        //if the bull hits one of the walls
        if(collision.gameObject.CompareTag("Wall"))
        {
            //you are no longer charging
            isCurrentlyCharging = false;
        }
        
    }

}
