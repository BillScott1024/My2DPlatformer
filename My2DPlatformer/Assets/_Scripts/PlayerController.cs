﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    private Transform groundCheck;
    private bool grounded = false;
    private Animator anim;
    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;


    // Start is called before the first frame update
    void Start()
    {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Ground"));
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(h));
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (h * rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector2.right * h * moveForce);

        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if(h <0 && facingRight) {

            Flip();
        }
        if (jump)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
}
