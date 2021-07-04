using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrumpyBirdProjectile : MonoBehaviour
{

    public float speed;

    private Transform player;
    private Vector2 target;

    void Start()
    {
        // Retrieve player game object
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Extract coordinates of the player to fire toward.
        target = new Vector2(player.position.x, player.position.y);
        // Vector3 dir = (target.position - )
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // if(transform.position.x == target.x && transform.position.y == target.y)  {
        //     DestroyProjectile();
        // }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))  {
            DestroyProjectile();
        }
    }

    void DestroyProjectile()    {
        Destroy(gameObject);
    }

    
}
