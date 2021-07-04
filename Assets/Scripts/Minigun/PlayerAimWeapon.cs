using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Transform gunComponent;
    private Animator anim;
    private float horizontalInput;
    private bool facingRight = true;

    private string currentAnimaton;
    const string GUN_IDLE = "Idle";
    const string GUN_SHOOT = "Shoot";

    private void Awake()    {
        aimTransform = transform.Find("Aim");
        // gunComponent = transform.Find("Gun");
        // anim = gunComponent.GetComponent<Animator>();
        anim = aimTransform.GetComponent<Animator>();
    }

    private void Update() {
        horizontalInput = Input.GetAxis("Horizontal");
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming() {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
            
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        // aimTransform.eulerAngles = new Vector3(0, 0, angle);
        
        if(horizontalInput < 0) {
            aimTransform.eulerAngles = new Vector3(180, 180, angle);
            facingRight = false;
        }
        else if(horizontalInput > 0) {
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
            facingRight = true;
        }

        if(facingRight) {
            aimTransform.eulerAngles = new Vector3(0, 0, angle);
        } else {
            aimTransform.eulerAngles = new Vector3(180, 180, angle);
        }
        
        // print(mousePosition);
    }

    private void HandleShooting()   {
        if(Input.GetMouseButton(0)) {
            // print("SHOOTING");
            ChangeAnimationState(GUN_SHOOT);
        } else {
            // print("NOT SHOOTING");
            ChangeAnimationState(GUN_IDLE);
        }
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;
        // print(newAnimation);

        anim.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
