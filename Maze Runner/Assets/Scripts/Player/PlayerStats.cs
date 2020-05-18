using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static bool foundKey;
    public static int coinCount;
    public static int health;
    void Awake()
    {
        foundKey = false;
        coinCount = 0;
        health = 3;
    }

    public static void healthDecrease(){
        if(health > 0){
            health -= 1;
            Debug.Log("ouch");
        }
        else{
            Debug.Log("game over");
        }
    }

}
