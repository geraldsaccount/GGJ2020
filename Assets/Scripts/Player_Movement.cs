using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float XAxis;
    public float ZAxis;
    public float Speed = 12f;
    public float JumpHeight = 3f;
    public float Gravity = -9.81f;

    public bool IsGrounded;

    public Vector3 Move;
    public Vector3 Velocity;

    [Header ("Ground_Check")]
    public Transform GroundCheck;
    public float GroundDistance;
    public LayerMask GroundMask;

    public CharacterController Controller;

    void Update()
    {
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        if (IsGrounded  && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        XAxis = Input.GetAxis("Horizontal");
        ZAxis = Input.GetAxis("Vertical");

        Move = transform.right * XAxis + transform.forward * ZAxis;
        Controller.Move(Move * Speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Velocity.y = Mathf.Sqrt(JumpHeight * -2 * Gravity);
        }

        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);
    }
}
