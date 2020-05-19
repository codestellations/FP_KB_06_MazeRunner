using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    public Animator animator;  

    public static bool isTalking;  

    void Update(){
        if(isTalking == true){
            animator.SetBool("isTalking", true);
        }
        else{
            animator.SetBool("isTalking", false);
        }
    }
}
