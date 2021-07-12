using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrumpyBirdProjectile : MonoBehaviour
{

    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float delay = 0f;
    [SerializeField] private float damage;

    private float moveSpeed = 7f;
    private Rigidbody2D rb;
    private Player target;
    private Vector2 moveDirection;

    void Start()    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindObjectOfType<Player>();
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D collision)  {
        if(collision.gameObject.name.Contains("Fireball")) return;
        // Start explosion
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy explosion effect after it's finished.
        Destroy(gameObject);
        if(collision.tag == "Player")   {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
    
}
