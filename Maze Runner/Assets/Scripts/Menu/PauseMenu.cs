using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;

    void Update(){
        if(Input.GetMouseButtonDown(0)){
            ResumeGame();
        }
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
