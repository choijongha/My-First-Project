using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private GameManager gameManager;
    public bool onObjectUsed;
    private Vector3 targetPos;
    private Rigidbody2D playerRb;
    private Vector3 playerPos;
    [SerializeField] float speed = 1f;
    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (onObjectUsed)
        {           
            if (GetComponent<Collider2D>().OverlapPoint(targetPos))
            {
                onObjectUsed = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else if (onObjectUsed)
        {

        }
    }
    public void ObjectUse()
    {
        onObjectUsed = true;
        targetPos = GameObject.Find($"{gameManager.selectedName}").transform.position;
        playerPos = playerRb.position;
        
    }


}
