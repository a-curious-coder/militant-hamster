using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    public float delay = 0f;

    void OnCollisionEnter2D(Collision2D collision)  {
        // Instantiate explosion effect
        if(!collision.gameObject.tag.Contains("Bullet") || !collision.gameObject.tag.Contains("Player"))   {
            // Instantiate explosion animation
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            // Destroy it after 5 seconds
            Destroy(effect, 5f);
            // Destroy the bullet object upon collision
            Destroy(gameObject);
            // Play bullet sound effect contained in the prefab
            GetComponent<AudioSource>().Play();
        }
    }
}
