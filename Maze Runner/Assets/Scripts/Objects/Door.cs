using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private bool isPickUp;
    public Animator animator;

    // PlayerStats player;
    void Start()
    {
        // player = GetComponent<PlayerStats>();
        isPickUp = false;
        animator.SetBool("isUnlocked", false);
    }

    void Update()
    {
        if(isPickUp && Input.GetKeyDown(KeyCode.Space)){
            if(PlayerStats.foundKey){
                openLock();
            }

            else{
                // put error message here
                Debug.Log("no key found yet");
            }
        }
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

    void openLock(){
        animator.SetBool("isUnlocked", true);     
    }
}
