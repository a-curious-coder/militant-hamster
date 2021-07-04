using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    private Animator anim;
    private bool dead = false;
    private PlayerMovement playerMovement;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    // Animation States
    const string PLAYER_HURT = "Hurt";
    const string PLAYER_DIE = "Die";
    
    private void Awake()    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(float _damage) {
        // Reduces to lowest whole value
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if(currentHealth > 0)   {
            // Player hurt
            // anim.SetTrigger("hurt");
            playerMovement.ChangeAnimationState(PLAYER_HURT);
            StartCoroutine(Invulnerability());
        } else {
            // Player dies
            if(!dead){
                // anim.SetTrigger("die");
                playerMovement.ChangeAnimationState(PLAYER_DIE);
                playerMovement.enabled = false;
                dead = true;
            }
        }
    }

    public void AddHealth(float _value) {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invulnerability()   {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        // Invulnerability Duration
        for(int i = 0; i < numberOfFlashes; i++)    {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes*2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes*2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }
}
