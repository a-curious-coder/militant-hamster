using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    private float damage = 20;

    void OnCollisionEnter2D(Collision2D collision)  {
        // Instantiate explosion effect
        // if(!collision.gameObject.tag.Contains("Bullet") || !collision.gameObject.tag.Contains("Player"))   {
        if(collision.gameObject.tag.Equals("Bullet")) return;
        // Instantiate explosion animation
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        // Destroy it after 5 seconds
        Destroy(effect, 5f);
        // Destroy the bullet object upon collision
        Destroy(gameObject);
        // Play bullet sound effect contained in the prefab
        // GetComponent<AudioSource>().Play();

    }
    // void OnTriggerEnter2D(Collider2D collision)  {
    //     // if(collision.gameObject.name.Contains("Bullet")) return;
    //     // Start explosion
    //     GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
    //     if(collision.tag == "Enemy")   {
    //         Debug.Log("Bullet hit Enemy");
    //         collision.GetComponent<GrumpyBird>().TakeDamage(damage);
    //     }
    //     Destroy(effect, 5f);
    //     // Destroy explosion effect after it's finished.
    //     Destroy(gameObject);
    // }
}
