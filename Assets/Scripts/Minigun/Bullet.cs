using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    private float damage = 20;

    void OnCollisionEnter2D(Collision2D collision)  {
        if(collision.gameObject.tag == "Bullet") {
            return;
        }
        // Instantiate explosion effect        
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy it after 5 seconds
        Destroy(gameObject);
        Destroy(effect, 5f);
        // Destroy the bullet object upon collision
        // Play bullet sound effect contained in the prefab
        // GetComponent<AudioSource>().Play();
        
    }
}
