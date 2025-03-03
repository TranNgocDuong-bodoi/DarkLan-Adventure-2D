using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private float moveInput;
    private bool jumpInput;
    private float climbInput;
    public float MoveInput{
        get => moveInput;
        set => this.moveInput = value;
    }
    public bool JumpInput{
        get => jumpInput;
        set => this.jumpInput = value;
    }
    public float ClimbInput{
        get => climbInput;
        set => this.climbInput = value;
    }
    void Update()
    {
        MovingInput();
        JumpingInput();
        ClimbingInput();
    }

    private void MovingInput()
    {
        this.MoveInput = 0;
        if(Input.GetKey(KeyCode.D))
        {
            this.MoveInput = 1;
        }
        if(Input.GetKey(KeyCode.A))
        {
            this.MoveInput = -1;
        }
    }
    private void JumpingInput()
    {
        jumpInput = Input.GetKeyDown(KeyCode.Space);
    }
    private void ClimbingInput()
    {
        this.ClimbInput = 0;
        if(Input.GetKey(KeyCode.W))
        {
            this.ClimbInput = 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            this.ClimbInput = -1;
        }
    }
}
