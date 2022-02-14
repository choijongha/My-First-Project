using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : Unit
{
    private GameManager gameManager;
    public bool onObjectUsed;
    private Vector3 targetPos;
    private Rigidbody2D playerRb;
    private Vector3 playerPos;
    Vector2 move;
    [SerializeField] float speed = 10f;
    void Awake()
    {
        if (GameObject.Find("GameManager")) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        move.x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        move.y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(move.x, move.y, transform.position.z);
        if (onObjectUsed)
        {           
            if (GetComponent<Collider2D>().OverlapPoint(targetPos))
            {
                onObjectUsed = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }
    public void ObjectUse()
    {
        onObjectUsed = true;
        targetPos = GameObject.Find($"{gameManager.selectedName}").transform.position;
        playerPos = playerRb.position;
        
    }


}
