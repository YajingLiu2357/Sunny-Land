using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    public Collider2D coll;
    public Collider2D DisColl;
    public Transform CellingCheck,GroundCheck;
    public AudioSource jumpAudio, hurtAudio,cherryAudio;
    public float speed;
    public float jumpForce;
    public LayerMask ground;
    public int cherry;
    public int gem;
    public Text cherryNum;
    public Text gemNum;
    private bool isHurt;
    private bool isGround;
    private int extraJump;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isHurt)
        {
            Movement();
        }
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, ground);
    }
    private void Update()
    {
        //Jump();
        Crouch();
        cherryNum.text = cherry.ToString();
        gemNum.text = gem.ToString();
        newJump();
    }
    void Movement()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float faceDirection = Input.GetAxisRaw("Horizontal");
        
      
        //the player movement
        if (horizontalMove != 0)
        {
            rb.velocity = new Vector2(horizontalMove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running",Mathf.Abs(faceDirection));
        }
        if (faceDirection != 0)
        {
            transform.localScale = new Vector3(faceDirection, 1, 1);
        }
        




    }

    //Switch animation
    void SwitchAnim()
    {
        anim.SetBool("idle", false);
        if (rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground))
        {
            anim.SetBool("dropping", true);
        } 
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("dropping", true);
            }
        }
        else if (isHurt)
        {
            anim.SetBool("hurting", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                anim.SetBool("hurting", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }
        else if (coll.IsTouchingLayers(ground))
        {
            anim.SetBool("dropping", false);
            anim.SetBool("idle", true);
        }
        
        
    }
    //Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Collect cherry
        if (collision.tag == "Collection")
        {
            cherryAudio.Play();
            collision.GetComponent<Animator>().Play("isGot");
            
        }
        // Collect gem
        if (collision.tag == "Gem")
        {
            cherryAudio.Play();
            collision.GetComponent<Animator>().Play("isGot");
            
            
        }
        if(collision.tag == "DeadLine")
        {

            GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 1.5f);
        }
       
    }
    //Hit the enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("dropping"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            //Hurt
            else if (transform.position.x < collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(10, rb.velocity.y);
                hurtAudio.Play();
                isHurt = true;
            }
        }
        
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    /*void Jump()
    {
        // the player jump
        if (Input.GetButton("Jump") && coll.IsTouchingLayers(ground))
        {
            jumpAudio.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }
    }*/
    void newJump()
    {
        if (isGround)
        {
            extraJump = 1;
        }
        if (Input.GetButtonDown("Jump") && extraJump > 0)
        {
            jumpAudio.Play();
            rb.velocity = Vector2.up * jumpForce;//"Vector2.up" is "new Vector2(0,1)"
            extraJump --;
            anim.SetBool("jumping", true);
        }
        if(Input.GetButtonDown("Jump") && extraJump == 0 && isGround)
        {
            jumpAudio.Play();
            rb.velocity = Vector2.up * jumpForce;
            anim.SetBool("jumping", true);
        }
    }

    void Crouch()
    {
        if (!Physics2D.OverlapCircle(CellingCheck.position,0.2f,ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                DisColl.enabled = false;
            }
            else 
            {
                anim.SetBool("crouching", false);
                DisColl.enabled = true;
            }
        }
        
        
        
    }
    public void CherryCount()
    {
        cherry += 1;
    }
    public void GemCount()
    {
        gem += 1;
    }
}
