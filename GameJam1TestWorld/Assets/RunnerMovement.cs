using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class RunnerMovement : MonoBehaviour
{
    [HideInInspector]
    public XboxController playerNumber = XboxController.First;
    [HideInInspector]
    public bool alive = true;

    public float MovementSpeed = 100.0f;
    Vector3 direction = new Vector3(0.0f,0.0f, 0.0f);
    
    Rigidbody       RunnerRigidBody;
    MeshRenderer    RunnerRenderer;
    BoxCollider     RunnerCollision;

    void Start()
    {
        RunnerRigidBody = GetComponent<Rigidbody>();
        RunnerRenderer  = GetComponent<MeshRenderer>();
        RunnerCollision = this.GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (alive)
        {
            float JoystickX = XCI.GetAxis(XboxAxis.LeftStickX, playerNumber);
            float JoystickY = XCI.GetAxis(XboxAxis.LeftStickY, playerNumber);

            direction.Set(JoystickX, 0.0f, JoystickY);

            if (direction.sqrMagnitude > 0.0f)
            {
                RunnerRigidBody.AddForce(direction * MovementSpeed * Time.deltaTime);
            }

            transform.forward = RunnerRigidBody.velocity.normalized;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "BullRider")
        {
            SetAlive(false);
        }
    }

    public void SetAlive(bool IsAlive)
    {
        alive = IsAlive;
        RunnerRenderer.enabled = IsAlive;
        RunnerCollision.enabled = IsAlive;
    }
}