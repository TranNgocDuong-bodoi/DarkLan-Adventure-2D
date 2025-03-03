using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void LateUpdate() {

    }
    public void GetMovingAnimation(float move)
    {
        if(Mathf.Abs(move) > 0)
        {
            animator.SetBool("isRunng",true);
        }
        else{
            animator.SetBool("isRunng",false);
        }
    }
    public void GetClimbingAction(float climb)
    {
        if(Mathf.Abs(climb) > 0)
        {
            animator.SetBool("isClimbing",true);
        }
        else
        {
            animator.SetBool("isClimbing", false);
        }
    }
}
