using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfoScript : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] string objectInfo;
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        ObjectInfoText();
    }
    void ObjectInfoText()
    {
        string a = objectInfo;
        gameManager.objectInfoText.text = $" {gameManager.selectedName} {a} ";
    }
}
