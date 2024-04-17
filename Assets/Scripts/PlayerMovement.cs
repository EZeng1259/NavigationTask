using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 4.5f;
    public float gravity = -9.81f;

    Vector3 velocity; 

    private void Start()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if((Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.A)))
        {
            Vector3 move = Camera.main.transform.right * x * 5/6 + Camera.main.transform.forward * z * 5/6;
            controller.Move(move * speed * Time.deltaTime);
        }
        else
        {
            Vector3 move = Camera.main.transform.right * x + Camera.main.transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }


}


