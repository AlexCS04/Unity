using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public int MaxHealth =  5;
    public int health { get { return currentHealth; }}
    
    public ParticleSystem Herido;

    int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    
    public GameObject projectilePrefab;

    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    public float Speed = 18.0f;
    Rigidbody2D rigidbody2d;

    AudioSource audioSource;

    public AudioClip shoot;

    public AudioClip Daño;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;

        animator = GetComponent<Animator>();
        
        rigidbody2d= GetComponent<Rigidbody2D>();

        audioSource= GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
        
        Vector2 position = rigidbody2d.position;
        
        position = position + move * Speed * Time.deltaTime;
        
        rigidbody2d.MovePosition(position);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            
            if (isInvincible)
            {
                
                return;
            }
            else
            {
                audioSource.PlayOneShot(Daño);
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, MaxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)MaxHealth);
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        audioSource.PlayOneShot(shoot);
        
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
