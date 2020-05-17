using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static bool foundKey;
    public int coinCount;
    public int health;
    // Start is called before the first frame update
    void Awake()
    {
        foundKey = false;
        coinCount = 0;
        health = 3;
    }

}
