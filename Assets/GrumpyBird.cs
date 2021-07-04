using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrumpyBird : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectilepref;
    public GameObject projectile;
    public Transform firepoint;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        firepoint = GameObject.FindGameObjectWithTag("GBFirePoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
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

        if(timeBtwShots <= 0)   {
            // Shoot - aim at player and shoot - don't follow.
            Vector3 dir = (target.position - firepoint.position).normalized;
            // Instantiate(WhatDoWeSpawn, Position, Rotation)
            Instantiate(projectilepref, firepoint.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        } else {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
