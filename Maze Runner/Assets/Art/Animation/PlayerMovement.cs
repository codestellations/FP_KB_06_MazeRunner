using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rigidbody2D;

    public Animator animator;

    Vector2 vector2;

    // Update is called once per frame
    void Update()
    {
        //Input

        vector2.x = Input.GetAxisRaw("Horizontal");
        vector2.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", vector2.x);
        animator.SetFloat("Vertical", vector2.y);
        animator.SetFloat("Speed", vector2.sqrMagnitude);

    }

    void FixedUpdate()
    {
        //Movement

        rigidbody2D.MovePosition(rigidbody2D.position + vector2 * moveSpeed * Time.fixedDeltaTime);
    }    
}
