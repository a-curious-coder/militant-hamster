using UnityEngine;

public class MinigunShooting : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject bulletPrefab;
    // [SerializeField] private GameObject[] bullets;

    [SerializeField] public float bulletForce = 20f;

    private float cooldownTimer = Mathf.Infinity;

    // Update is called once per frame
    // void Update()
    // {
        
        
    // }

    public void Shoot()    {
        // if(cooldownTimer > attackCooldown)    {
            // cooldownTimer = 0;
            
            // Create a bullet at firePoint
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            // transform.Rotate(new Vector3(0, 0, 270));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            // Apply force to bullet so it flies outward from the gun
            rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        // }
        // cooldownTimer += Time.deltaTime;
    }

    //================================================================
    // Retrieves fireball index from prefab List
    //================================================================
    // private int FindBullet()  {
    //     for (int i = 0; i < bullets.Length; i++)
    //     {
    //         if (!bullets[i].activeInHierarchy)
    //             return i;
    //     }
    //     return 0;
    // }
}
