using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : MonoBehaviour
{
    LayerMask boundaryMask;
    [SerializeField] protected Transform target;
    //[SerializeField] protected float moveSpeed;   
    [SerializeField] float hp;
    [SerializeField] float mana;
    [SerializeField] float damage;
    [SerializeField] float critical;
    
    protected Rigidbody2D rb;
    protected Vector2 movement;
    [SerializeField] protected Animator anim;

    protected bool isAction;  // 행동 중인지 아닌지 판별
    protected bool isWalking; // 걷는지, 안 걷는지 판별
    protected bool isRunning; // 달리는지 판별
    protected bool isDead;   // 죽었는지 판별

    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float turnSpeed;
    protected float applySpeed;
    protected Vector3 direction;
    [SerializeField] protected float walkTime;
    [SerializeField] protected float waitTime;
    [SerializeField] protected float runTime;
    protected float currentTime;

    private Vector3 randomDirec;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
        boundaryMask = LayerMask.GetMask("boundary");
    }
    private void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (!isDead)
        {
            Move();
            ElapseTime();
        }

        if (Physics2D.Raycast(transform.position, randomDirec, 1.0f, boundaryMask))
        {
            Reset();
        }
    }
    protected void Move()
    {
        if(isWalking || isRunning)
        {
            rb.MovePosition(transform.position + randomDirec * applySpeed * Time.deltaTime);
        }
    }
    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0) Reset();
        }
    }
    protected virtual void Reset()
    {
        isAction = true;
        isWalking = false;
        anim.SetBool("Walking", isWalking);
        isRunning = false;
        anim.SetBool("Running", isRunning);
        applySpeed = walkSpeed;

        int randomDirection = Random.Range(0, 4);

        if(randomDirection == 0)
        {
            randomDirec = transform.up;
        }else if(randomDirection == 1)
        {
            randomDirec = transform.right;
        }else if(randomDirection == 2)
        {
            randomDirec = -transform.right;
        }else if(randomDirection == 3)
        {
            randomDirec = -transform.up;
        }
        //direction.Set(0f, Random.Range(0f, 360f), 0f);
    }

    protected void TryWalk()
    {
        currentTime = walkTime;
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        applySpeed = walkSpeed;
        Debug.Log("걷기");
    }

    public virtual void Damage(int dmg, Vector3 targetPos)
    {
        if (!isDead)
        {
            hp -= dmg;

            if(hp <= 0)
            {
                Dead();
                return;
            }
            anim.SetTrigger("Hurt");
        }
    }
    protected void Dead()
    {
        isWalking = false;
        isRunning = false;
        isDead = true;
        anim.SetTrigger("Dead");
    }
}
