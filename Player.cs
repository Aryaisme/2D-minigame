using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    // speed of the player   
    public float speed = 150f;
        
    // The terminal velocity
    public Vector2 maxVelocity = new Vector2(60, 100);
    
    //speed when moving up
    public float jetSpeed = 200f;
    
    public bool standing;
    public float standingThreshold = 4f;
    public float airSpeelMultiplier = 0.3f;

    // a refrence to components on the player
    private Rigidbody2D body2D;
    private SpriteRenderer renderer2D;
    private PlayerController controller;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // getting the parts
        body2D = GetComponent<Rigidbody2D>();
        renderer2D = GetComponent<SpriteRenderer>();
        controller = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // get the velocity of the player in positive numbers
        var absVelX = Mathf.Abs(body2D.velocity.x);
        var absVelY = Mathf.Abs(body2D.velocity.y);
        
        // check to see if the player is standing or not
        if (absVelY <= standingThreshold)
        {
            standing = true;
        }
        else
        {
            standing = false;
        }
        
        //set the force to 0
        var forceX = 0f;
        var forceY = 0f;

        // if the player is moving
        if(controller.moving.x != 0)
        {
            // if we are moving at less than max velocity
            if (absVelX < maxVelocity.x)
            {
                // set the new speed
                var newSpeed = speed * controller.moving.x;

                forceX = standing ? newSpeed : (newSpeed * airSpeelMultiplier);
                
                //flipping the charater if they are moving in another direction
                renderer2D.flipX = forceX < 0;
            }
            
            //set the animation
            animator.SetInteger("AnimState", 1);
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }


        // repeat with y axis
        if (controller.moving.y > 0)
        {
            if (absVelY < maxVelocity.y)
            {
                forceY = jetSpeed*controller.moving.y;
            }
            animator.SetInteger("AnimState", 2);
        }
        else if (absVelY > 0 && !standing)
        {
            animator.SetInteger("AnimState", 3);
        }
        
        // apply the force to move
        body2D.AddForce(new Vector2(forceX, forceY));

    }
}
