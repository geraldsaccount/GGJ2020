using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    [Header ("Input")]
    private float xAxis;
    private float zAxis;

    [Header ("Values")]
    public float Speed;
    public float JumpHeight;
    public float Gravity;

    [Header ("Vectors")]
    public Vector3 Move;
    public Vector3 Velocity;

    [Header ("Ground_Check")]
    public Transform GroundCheckPosition;
    public float GroundDistance;
    public LayerMask GroundMask;

    [Header ("Character_Controller")]
    public CharacterController Controller;

    void Update()
    {
        if (GroundCheck()  && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        xAxis = Input.GetAxis("Horizontal");
        zAxis = Input.GetAxis("Vertical");

        Move = transform.right * xAxis + transform.forward * zAxis;
        Controller.Move(Move * Speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && GroundCheck())
        {
            Velocity.y = Mathf.Sqrt(JumpHeight * -2 * Gravity);
        }

        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);
    }

    public bool GroundCheck()
    {
        return Physics.CheckSphere(GroundCheckPosition.position, GroundDistance, GroundMask);
    }
}
