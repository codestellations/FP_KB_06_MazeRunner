using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public TextMeshProUGUI textDisplay;

    void Update(){
        if(pausePanel.activeSelf){
            if(Input.GetMouseButtonDown(0)){
                ResumeGame();
            }
        }
        textDisplay.text = "Health : " + PlayerStats.health;
    }

    public void PauseGame(){
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame(){
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
