using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Permissions;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D player;

    public Animator animator;
    private float buttonCooler = .5f;
    private int[] buttonCount = {0, 0, 0, 0};
    Vector2 movement;
    Vector2 defaultPosition;
    public DialogueManager dialogue;
    bool once = true;

    void Update()
    {
        // x < 0
        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            moveSpeed = 5f;
            GetComponent<SpriteRenderer>().flipX = true;
            runCheck(0);

            if(once){
                dialogue.RequestSentence(3, 5);
                once = false;
            }
            
        }
        // x > 0
        else if(Input.GetKeyDown(KeyCode.RightArrow)){
            moveSpeed = 5f;
            GetComponent<SpriteRenderer>().flipX = false;
            runCheck(1);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow)){
            moveSpeed = 5f;
            runCheck(2);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)){
            moveSpeed = 5f;
            runCheck(3);
        }

        if(buttonCooler > 0){
            buttonCooler -= 1 * Time.deltaTime;
        }
        else{
            Array.Clear(buttonCount, 0, buttonCount.Length);
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
        
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate()
    {
        //Movement
        player.MovePosition(player.position + movement * moveSpeed * Time.fixedDeltaTime);
    }    

    void runCheck(int i){
        if(buttonCooler > 0 && buttonCount[i] != 0){
            moveSpeed *= 2f;
        }
        else{
            buttonCooler = .75f;
            buttonCount[i] += 1;
        }
    }
}
