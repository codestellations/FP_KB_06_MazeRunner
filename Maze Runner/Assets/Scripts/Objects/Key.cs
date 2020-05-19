using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isPickUp;
    public Animator animator;
    public DialogueManager dialogue;

    void Start()
    {
        isPickUp = false;
        animator.SetBool("isUsed", false);
    }

    void Update()
    {
        if(isPickUp && Input.GetKeyDown(KeyCode.Space))
        pickUp();
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision.gameObject.tag.Equals("Player")){
            isPickUp = true;
            dialogue.RequestSentence(5, 6);
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag.Equals("Player")){
            isPickUp = false;
        }
    }

    void pickUp(){
        if(PlayerStats.foundKey == false){
            dialogue.RequestSentence(6, 7);
            PlayerStats.foundKey = true;
            animator.SetBool("isPickingUp", true);
            animator.SetBool("isUsed", true);
        }
    }
}
