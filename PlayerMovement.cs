using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 2f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;
    private float OgMoveSpeed = 2f;
    bool repelling;

    public Transform upPos,downPos,leftPos,rightPos;
    public GameObject attackObj;


    void Update()
    {
        //Detect if axis is being pressed
        movement.x =Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //if both axis are down reduce speed because you move faster at an angle
        if(movement.y != 0 && movement.x != 0)
        {
            movement . x = movement.x / 1.25f;
            movement . y = movement . y / 1.25f;
        }
      
        //set animator floats for directional movement
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        //Detect if space is pressed
        if(Input.GetButton("Jump"))
        {
            //Refer anims to the repel blend tree
            animator.SetBool("Repell", true);

            repelling = true;
            //Setactive hitObj
            attackObj . SetActive (true);

            //Move attack object towards position the player is facing when moving
            if(animator.GetFloat("Horizontal") >= 1)
            {
                attackObj . transform . position = rightPos . position;
            }
            else if ( animator . GetFloat ("Horizontal") <= -1 )
            {
                attackObj . transform . position = leftPos . position;
            }
            else if ( animator . GetFloat ("Vertical") >= 1 )
            {
                attackObj . transform . position = upPos . position;
            }
            else if ( animator . GetFloat ("Vertical") <= -1 )
            {
                attackObj . transform . position = downPos . position;
            }
            else
            {
                //if not moving a direction while attacking keep hitbox centered
                attackObj . transform . position = gameObject.transform. position;
            }




        }
        else
        {
            // if the jump button is not pressed down then disable all functions to attack
            animator.SetBool("Repell", false);
            repelling = false;
            attackObj . SetActive (false);
        }

        
    }


 
    void FixedUpdate()
    {
        // move players position relative to the axis key pressed and time * speed
        // if repelling reduce speed
        if(repelling == true)
        {
            rb . MovePosition (rb . position + movement * moveSpeed /3 * Time . fixedDeltaTime);
        }
        else
        {
            // if not repelling move as normal
            rb . MovePosition (rb . position + movement * moveSpeed * Time . fixedDeltaTime);
        }

    }

  
}
