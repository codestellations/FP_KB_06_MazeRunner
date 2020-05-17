using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isPickUp;
    public Animator animator;

    // PlayerStats player;

    // Start is called before the first frame update
    void Start()
    {
        // player = GameObject.FindObjectsOfType<PlayerStats>();
        isPickUp = false;
        animator.SetBool("isUsed", false);
    }

    // Update is called once per frame
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
        PlayerStats.foundKey = true;
        animator.SetBool("isPickingUp", true);
        animator.SetBool("isUsed", true);
    }
}
