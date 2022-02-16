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
    Vector2 move;
    [SerializeField] float speed = 10f;
    [SerializeField] GameObject forwardPoint;
    void Awake()
    {
        if (GameObject.Find("GameManager")) gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        move.x = Input.GetAxis("Horizontal") ;
        move.y = Input.GetAxis("Vertical");

        forwardPoint.transform.localPosition = new Vector3(move.x, move.y);
        
        float moveSpeedX = move.x * speed * Time.deltaTime;
        float moveSpeedY = move.y *speed * Time.deltaTime;

        transform.Translate(moveSpeedX, moveSpeedY, transform.position.z);
        
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
