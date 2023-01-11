using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    //private Animator Anim;
    private Collider2D Coll;
    public LayerMask Ground;
    public Transform leftpoint, rightpoint;
    private bool FaceLeft = true;
    public float Speed,JumpForce;
    private float leftx, rightx;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        SwitchAnim();
    }

    void Movement()
    {
        if (FaceLeft)
        {
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                FaceLeft = false;
            }
            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true); 
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            
            
        }
        else
        {
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                FaceLeft = true;
            }

            if (Coll.IsTouchingLayers(Ground))
            {
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }

            

        }
    }
    void SwitchAnim()
    {
        if (Anim.GetBool("jumping"))
        {
            if(rb.velocity.y < 0.1)
            {
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if (Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling"))
        {
            Anim.SetBool("falling", false);
        }
    }
    
}
