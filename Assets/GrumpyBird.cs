using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrumpyBird : MonoBehaviour
{
    // Follow player variables
    public float stoppingDistance;
    public float retreatDistance;

    // Patrol variables
    public Transform[] moveSpots;
    private int randomSpot;
    private float waitTime;
    public float startWaitTime;
    

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileForce = 2;

    private Transform firePoint;
    private Transform player;

    // Movement variables
    public float speed;
    private float horizontalInput;
    

    float fireRate;
    float nextFire;

    // Health
    private float maxHealth = 100;
    private float currentHealth;

    // Start is called before the first frame update
    void Start()    {
        fireRate = 1f;
        nextFire = Time.time;

        moveSpots[0] = GameObject.Find("MovePoint").transform;
        moveSpots[1] = GameObject.Find("MovePoint1").transform;
        moveSpots[2] = GameObject.Find("MovePoint2").transform;
        moveSpots[3] = GameObject.Find("MovePoint3").transform;
        moveSpots[4] = GameObject.Find("MovePoint4").transform;
        moveSpots[5] = GameObject.Find("MovePoint5").transform;
        randomSpot = Random.Range(0, moveSpots.Length);

        player = gameObject.transform.Find("Player");
        firePoint = gameObject.transform.Find("GBFirePoint");
        
        waitTime = startWaitTime;
        // Initialise Health
        currentHealth = maxHealth;
    }


    // Update is called once per frame
    void Update()   {
        // FollowPlayer();
        Patrol();
        HandleShooting();
    }

    void HandleShooting()   {
        if(Time.time > nextFire)    {
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
            // Debug.Log("Enemy projectile fired");
        }
    }

    void OnCollisionEnter2D(Collision2D col)    {
        if(col.gameObject.tag.Equals("Bullet")) {
            Destroy(col.gameObject);
            Destroy(gameObject);
            Debug.Log("Hit");
        }
    }

    public void TakeDamage(float _damage)   {
        currentHealth -= _damage;
        if(currentHealth <= 0)  {
            Destroy(gameObject);
        }
        Debug.Log("Enemy has taken damage");
    }

    void FlipEnemy()    {
        // If the movespot location is to the right of the enemy, face the enemy right
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
    }

    void Patrol()    {
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

    void FollowPlayer() {
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
