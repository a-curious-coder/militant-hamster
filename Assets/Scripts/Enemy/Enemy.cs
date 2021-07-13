using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    protected float health;

    // This is called if the child class has no Start function to initialise variable values.
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0) {
            Die();
        }
    }

    private void Die()  {
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)    {
        if (col.transform.tag == "Bullet")
        {
            Debug.Log("Starting health: " + health);
            // do damage here, for example:
            TakeDamage(5);
            Debug.Log("Enemy Shot");
        }
    }
}
