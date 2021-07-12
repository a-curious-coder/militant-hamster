using UnityEngine;
using System.Collections;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Transform gunComponent;
    private Animator anim;
    private float horizontalInput;
    private bool facingRight;

    // Touch Screen control for shooting minigun
    public Joystick shootJoystick;
    public Joystick movementJoystick;

    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public float bulletForce = 20f;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;
    private bool canShoot = true;
    private bool shootButtonPressed = false;

    private string currentAnimaton;
    const string GUN_IDLE = "Idle";
    const string GUN_SHOOT = "Shoot";

    private void Awake()    {
        // Finds gun
        aimTransform = transform.Find("Aim");
        anim = aimTransform.GetComponent<Animator>();
    }

    public void SetShoot()  {
        shootButtonPressed = !shootButtonPressed;
        // Debug.Log("Pressed and can shoot:" + shootButtonPressed);
    }

    private void Update() {
        HandleAiming();
        if(shootButtonPressed && canShoot)
        // Debug.Log("Shooting");
            HandleShooting();
    }

    private void HandleAiming() {
        horizontalInput = movementJoystick.Horizontal;
        // TODO: Find a way to avoid resetting gun aim direction after releasing joystick.
        Vector3 touchPosition = new Vector3(shootJoystick.Horizontal, shootJoystick.Vertical, 0);
        // 
        Vector3 aimDirection = (touchPosition).normalized;
        // Calculates angle we're aiming the gun toward
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);

        // Vector3 aimLocalScale = Vector3.one;
        // if(angle > 90 || angle < -90){
        //     aimLocalScale.y = -1f;
        // } else {
        //     aimLocalScale.y = +1f;
        // }
        // aimTransform.localScale = aimLocalScale;
    }

    public void HandleShooting()   {
        // Instantiate a bullet
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // Get the physical bullet component
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        // Apply force to bullet so it flies outward from the gun
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        canShoot = false;
        // Applies cooldown delay between bullets.
        StartCoroutine(ShootDelay());
    }

    IEnumerator ShootDelay()    {
        yield return new WaitForSeconds(attackCooldown);
        canShoot = true;
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;
        // print(newAnimation);

        anim.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
