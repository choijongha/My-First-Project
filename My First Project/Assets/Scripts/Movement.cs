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
    private Vector3 directionVector;
    private bool isPlayerMoving;
    Vector2 move;
    [SerializeField] float speed = 10f;
    [SerializeField] GameObject forwardPoint;
    void Awake()
    {
        if (GameObject.Find("GameManager")) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        MoveKey();
        IsMoving();
        LookDirection();
        DestroyTile();
    }
    public void ObjectUse()
    {
        onObjectUsed = true;
        targetPos = GameObject.Find($"{gameManager.selectedName}").transform.position;
        playerPos = playerRb.position;
        
    }
    public void MoveKey()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        playerRb.velocity = new Vector2(move.x, move.y) * speed * Time.deltaTime;
    }
    public void IsMoving()
    {
        if (move.x > .1 || move.x < -.1 || move.y > .1 || move.y < -.1)
        {
            isPlayerMoving = true;

        }
        else
        {
            isPlayerMoving = false;
        }
    }
    public void LookDirection()
    {
        if (isPlayerMoving)
        {
            forwardPoint.transform.localPosition = new Vector3(move.x, move.y).normalized;
        }
    }
    public void DestroyTile()
    {
        if (onObjectUsed)
        {
            if (GetComponent<Collider2D>().OverlapPoint(targetPos))
            {
                onObjectUsed = false;
            }
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
    }

}
