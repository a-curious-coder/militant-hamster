using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect;
    public float delay = 0f;

    void OnCollisionEnter2D(Collision2D collision)  {
        // Instantiate explosion effect
        if(collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player")   {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 5f);
            Destroy(gameObject);
            GetComponent<AudioSource>().Play();
        }
    }
}
