using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] float hp;
    [SerializeField] float mana;
    [SerializeField] float damage;
    [SerializeField] float critical;
    [SerializeField] protected Transform player;

    private protected Rigidbody2D rb;
    private protected Vector2 movement;
  
}
