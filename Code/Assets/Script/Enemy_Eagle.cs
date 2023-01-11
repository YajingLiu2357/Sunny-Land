using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy  
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    //private Animator Anim;
    //public Collider2D coll;
    public LayerMask Ground;
    public Transform top, bottom;
    private bool isUp = true;
    public float Speed;
    private float TopY, BottomY;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        TopY = top.position.x;
        BottomY = bottom.position.x;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y > TopY)
            {
                isUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < BottomY)
            {
                isUp = true;
            }
        }

    }
   


}
