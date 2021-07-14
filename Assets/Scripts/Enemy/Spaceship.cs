using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : ProjectileEnemy
{   

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        ShootAtPlayerLocation();
    }
}
