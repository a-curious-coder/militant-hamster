using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrumpyBird : ProjectileEnemy
{
    // Start is called before the first frame update
    void Start()    {
        // Set this enemy's health to maxHealth value
        health = maxHealth;
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
    }


    // Update is called once per frame
    void Update()   {
        // FollowPlayer();
        Patrol();
        ShootAtPlayerLocation();
    }
}
