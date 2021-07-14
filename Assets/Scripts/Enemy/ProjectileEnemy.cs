using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemy : Enemy
{
    // Follow player variables
    public float stoppingDistance;
    public float retreatDistance;

    // Patrol variables
    public Transform[] moveSpots;
    protected int randomSpot;
    protected float waitTime;
    public float startWaitTime;
    
    // Projectile and firing variables
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileForce = 2;
    [SerializeField] private float fireRate;
    protected float nextFire;

    // Movement variables
    public float speed;
    protected float horizontalInput;

    // Firepoint and player positions
    protected Transform firePoint;
    protected Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void ShootAtPlayerLocation()   {
        if(Time.time > nextFire)    {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            // Maybe need to put the logic here?
            nextFire = Time.time + fireRate;
        }
    }

    protected void FlipEnemy()    {
        if(moveSpots[randomSpot].position.x < 0)
            // If the movespot location is to the right of the enemy, face the enemy right
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        else
            transform.localScale = new Vector2(+transform.localScale.x, transform.localScale.y);
    }

    protected void Patrol()    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);
        // If enemy has reached "random spot", wait a few seconds and move to next random spot
        if(Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f) {
            if(waitTime <= 0)   {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
                FlipEnemy();
            } else {
                waitTime -= Time.deltaTime;
            }
        }
    }

    protected void FollowPlayer() {
        // Check how far away player is away from enemy
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance)    {
            // Move towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); 
        } else if(Vector2.Distance(transform.position, player.position) < stoppingDistance 
                && Vector2.Distance(transform.position, player.position) > retreatDistance) { 
                // Is enemy near enough to stop moving toward target
            transform.position = this.transform.position;
        } else if(Vector2.Distance(transform.position, player.position) > retreatDistance) {// If enemy should back away
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }
}
