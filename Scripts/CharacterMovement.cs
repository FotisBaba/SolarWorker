using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 6.0f; // Movement speed

    private Vector3 moveDirection = Vector3.zero;
    private Animator animController;
    void Start()
    {
        animController = GetComponent<Animator>();
    }

    void Update()
    {
        // Get horizontal and vertical input (WASD)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, 0.05f);
            animController.SetTrigger("Walk");
        }
        else if(verticalInput < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position - transform.forward, 0.05f);
            animController.SetTrigger("Walk");
        }
        else
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            animController.SetTrigger("Idle");
        }

        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation*Quaternion.Euler(0, 1f, 0), 60f*Time.deltaTime);
        }
        else if (horizontalInput < 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, transform.rotation*Quaternion.Euler(0, 1f, 0),  -60f*Time.deltaTime);
        }
        else
        {
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }
}
