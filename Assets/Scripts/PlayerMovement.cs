using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;

    float gravity = -9.81f;
    [SerializeField] float speed = 12f;
    [SerializeField] float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        controller.Move(HandleMovement() * Time.deltaTime);
    }

    Vector3 HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (move.magnitude > 1f) move.Normalize();
        move *= speed;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isGrounded)
        {
            velocity.y += -0.05f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        move.y += velocity.y;

        Debug.DrawLine(transform.position, move + transform.position, Color.red);

        return move;
    }
}
