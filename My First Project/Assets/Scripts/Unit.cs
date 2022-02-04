using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Unit : MonoBehaviour
{
    [SerializeField] protected Transform target;
    //[SerializeField] protected float moveSpeed;   
    [SerializeField] float hp;
    [SerializeField] float mana;
    [SerializeField] float damage;
    [SerializeField] float critical;
    
    protected Rigidbody2D rb;
    protected Vector2 movement;
    [SerializeField] protected Animator anim;

    protected bool isAction;  // �ൿ ������ �ƴ��� �Ǻ�
    protected bool isWalking; // �ȴ���, �� �ȴ��� �Ǻ�
    protected bool isRunning; // �޸����� �Ǻ�
    protected bool isDead;   // �׾����� �Ǻ�

    [SerializeField] protected float walkSpeed;
    [SerializeField] protected float runSpeed;
    [SerializeField] protected float turnSpeed;
    protected float applySpeed;
    protected Vector3 direction;
    [SerializeField] protected float walkTime;
    [SerializeField] protected float waitTime;
    [SerializeField] protected float runTime;
    protected float currentTime;

    private void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        currentTime = waitTime;
        isAction = true;
    }
    private void Update()
    {
        if (!isDead)
        {
            Move();
            Rotation();
            ElapseTime();
        }
    }
    protected void Move()
    {
        if(isWalking || isRunning)
        {
            rb.MovePosition(transform.position + transform.forward * applySpeed * Time.deltaTime);
        }
    }

    protected void Rotation()
    {
        if(isWalking || isRunning)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), turnSpeed);
            rb.MoveRotation(Quaternion.Euler(rotation));
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

        direction.Set(0f, Random.Range(0f, 360f), 0f);

    }

    protected void TryWalk()
    {
        currentTime = walkTime;
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        applySpeed = walkSpeed;
        Debug.Log("�ȱ�");
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
