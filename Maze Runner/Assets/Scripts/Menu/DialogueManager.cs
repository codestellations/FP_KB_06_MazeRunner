using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public int index;
    public float typingSpeed;
    GameObject dialogueBox;
    public Dialogue dialogue;
    public int limit;
    // public int begin;
    void Start(){
        dialogueBox = GameObject.FindGameObjectWithTag("Dialogue");
        ActivateText(true);
        StartCoroutine(Type());
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            // if(textDisplay.text == sentences[index]){
                NextSentence();
            // }
        }
    }

    IEnumerator Type(){
        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }  
    } 

    public void NextSentence(){
        if(index < limit){
            if(index < sentences.Length - 1){
                index++;
                textDisplay.text = "";
                
                StartCoroutine(Type());
                
            }
            else{
                textDisplay.text = "";
                ActivateText(false);
            }
            sentences[index] = null;
        }
        else{
            ActivateText(false);
        }
    }

    public void ActivateText(bool status){
        dialogueBox.SetActive(status);
        Dialogue.isTalking = status;
    }

    // usage : for 1 sentence, use (index-1, index)
    // the index with start parameter will not be shown
    public void RequestSentence(int start, int end){
        index = start;
        limit = end;
        if(sentences[limit] != null){
            ActivateText(true);
            NextSentence();
        }
    }
}
