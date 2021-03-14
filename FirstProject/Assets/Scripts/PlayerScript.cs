using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;

    public float runSpeed = 40f;
    float travel;
    float currentheight;
    float previousheight;
    float horisontalMove = 0f;
    bool jump = false;
    bool jumping = false;
    bool crouch = false;
    bool isHoldingJump = false;
    bool fire = false;
    bool attacking = false;
    
    
    void Start()
    {
        currentheight = transform.position.y;
        previousheight = currentheight;
    }

    void Update()
    {
        currentheight = transform.position.y;

        travel = currentheight - previousheight;

        
        if (travel < 0 && jumping)
        {
            animator.SetBool("isFalling", true);
        }

        if (!attacking && Input.GetButtonDown("Fire") && controller.isGrounded() && !controller.isCrouched())
        {   
            animator.SetBool("Fire",true);
            fire = true;
            attacking = true;
            StartCoroutine(resetFire(.5f));
            controller.stopMooving();
                
        }
        if (!attacking && Input.GetButtonDown("Jump"))
        {
            jump = true;
            jumping = true;
            animator.SetBool("IsJumping", true);
        }
            

        if (!attacking && Input.GetButtonUp("Jump")){
            isHoldingJump = false;
        }
        if (!attacking && Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            
        }
        else if (!attacking && Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }

        if (!attacking)
        {
            horisontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            animator.SetFloat("Speed", Mathf.Abs(horisontalMove));
        }
        
        animator.SetBool("isCrouched", controller.isCrouched());
        previousheight = currentheight;
    }


    public void OnLanding()
    {   
        animator.SetBool("IsJumping", false);
        animator.SetBool("isFalling", false);
        jumping = false;
        if (isHoldingJump)
        {
            jump = true;
            jumping = true;
            animator.SetBool("IsJumping", true);
        }

    }

    public void OnAirEvent()
    {   
        animator.SetBool("IsJumping", true);
        jumping = true;
    }

    void FixedUpdate()
    {
        if (!attacking)
        {
            controller.Move(horisontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }
        
        
    }

    IEnumerator resetFire(float timer)
    {
        yield return new WaitForSeconds(timer);
        fire = false;
        attacking = false;
        controller.allowMooving();
        animator.SetBool("Fire", false);
    }

}
