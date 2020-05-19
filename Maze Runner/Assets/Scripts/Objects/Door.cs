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
    public DialogueManager dialogue;
    bool once = true;

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
                openLock();
            }

            else{
                dialogue.RequestSentence(11, 12);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(coll.enabled == true){
            if(collision.gameObject.tag.Equals("Player")){
                isPickUp = true;
                if(once){
                    dialogue.RequestSentence(8, 9);
                }
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
        dialogue.RequestSentence(10, 11);
        animator.SetBool("isUnlocked", true); 
        coll.enabled = false;
        next.enabled = true;
    }
}
