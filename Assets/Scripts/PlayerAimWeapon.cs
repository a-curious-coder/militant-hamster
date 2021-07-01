using UnityEngine;
using CodeMonkey.Utils;

public class PlayerAimWeapon : MonoBehaviour
{
    private Transform aimTransform;
    private Animator aimAnimator;
    private void Awake()    {
        aimTransform = transform.Find("Aim");
        aimAnimator = aimTransform.GetComponent<Animator>();
    }

    private void Update() {
        HandleAiming();
        HandleShooting();
    }

    private void HandleAiming() {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
        // print(mousePosition);
    }

    private void HandleShooting()   {
        if(Input.GetMouseButton(1)) {
            aimAnimator.SetTrigger("Shoot");
        }
    }
}
