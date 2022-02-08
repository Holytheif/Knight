using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private float moveSpeed;
    private float jumpForce;
    private float dashForce;

    private bool isJumping;
    private bool isDash;
    private bool isLookingLeft;
    private bool isAttack;

    private float moveHorizontal;
    private float moveVertical;

    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();


        moveSpeed = 1.25f;
        jumpForce = 30f;
        dashForce = 30f;
        isJumping = false;
        isDash = false;
        isLookingLeft = false;
        isAttack = false;
    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        if (isLookingLeft)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        
        if (moveHorizontal!=0 && !isJumping && Input.GetButtonDown("Fire2"))
        {
            isDash = true;
            animator.SetBool("isDash", true);
        }
        animator.SetFloat("speed", Mathf.Abs(moveHorizontal * moveSpeed));
        
        if (Input.GetKey(KeyCode.A))
        {
            isLookingLeft = true;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            isLookingLeft = false;
        }

        if(moveHorizontal==0 && Input.GetButtonDown("Fire1"))
        {
            isAttack = true;
            animator.SetBool("isAttack", true);
        }

        if (isLookingLeft)
        {
            dashForce *= -1;
        }
        else
        {
            dashForce = Mathf.Abs(dashForce);
        }
        if (isLookingLeft)
        {
            if (dashForce > 0)
            {
                dashForce *= -1;
            }
        }
        else
        {
            dashForce = Mathf.Abs(dashForce);
        }


    }

    void FixedUpdate()
    {
        if(moveHorizontal != 0f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        
        if(!isJumping && moveVertical > 0f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }
        
        if(isDash)
        {
            rb2D.AddForce(new Vector2( dashForce, 0f), ForceMode2D.Impulse);
            animator.SetBool("isDash", false);
            isDash = false;
        }
        if (isAttack)
        {
            animator.SetBool("isAttack", false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }    
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }
    }
}
