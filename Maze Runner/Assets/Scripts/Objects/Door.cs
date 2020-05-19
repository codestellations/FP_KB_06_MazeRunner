using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool isPickUp;
    public Animator animator;

    public BoxCollider2D coll;
    public BoxCollider2D next;

    void Start()
    {
        isPickUp = false;
        next.enabled = false;
        animator.SetBool("isUnlocked", false);
    }

    void Update()
    {
        if(isPickUp && Input.GetKeyDown(KeyCode.Space)){
            if(PlayerStats.foundKey){
                Debug.Log("opening the door please wait");
                openLock();
            }

            else{
                // put error message here
                Debug.Log("no key found yet");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(coll.enabled == true){
            if(collision.gameObject.tag.Equals("Player")){
                isPickUp = true;
            }     
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision.gameObject.tag.Equals("Player")){
            isPickUp = false;
        }
    }

    void openLock(){
        animator.SetBool("isUnlocked", true); 
        coll.enabled = false;
        next.enabled = true;
    }
}
