using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfoScript : MonoBehaviour
{
    GameManager gameManager;
    public string objectInfoTextString;
    private void Awake()
    {
        if(GameObject.Find("GameManager")) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
}
