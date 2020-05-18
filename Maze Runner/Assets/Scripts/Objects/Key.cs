using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isPickUp;
    public Animator animator;

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
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag.Equals("Player")){
            isPickUp = false;
        }
    }

    void pickUp(){
        Debug.Log("found key yeay");
        PlayerStats.foundKey = true;
        animator.SetBool("isPickingUp", true);
        animator.SetBool("isUsed", true);
    }
}
