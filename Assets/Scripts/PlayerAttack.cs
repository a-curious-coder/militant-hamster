using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;

    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    // Animation State
    const string PLAYER_ATTACK = "Attack";

    private void Awake() {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update() {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack()) {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()   {
        playerMovement.ChangeAnimationState(PLAYER_ATTACK);
        cooldownTimer = 0;
        // pool fireballs
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    
    //================================================================
    // Retrieves fireball index from prefab List
    //================================================================
    private int FindFireball()  {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
