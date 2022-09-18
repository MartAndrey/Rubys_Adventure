using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    // ======================MOVEMENT======================
    float timer;
    int direction = 1;
    [SerializeField] float speed = 1;
    [SerializeField] bool vertical;
    [SerializeField] float changeTimer = 3;

    // ======================DAMAGE======================
    [SerializeField] int damage = -1;

    // ====================PHYSICAL=====================
    Rigidbody2D rb;

    // ====================FIXED=====================
    bool robotFixed = false;

    // ====================ANIMATOR=====================
    Animator animator;

    // ====================AUDIO=====================
    AudioSource audioSource;
    [SerializeField] AudioClip hit;
    [SerializeField] AudioClip audioFixed;

    // ====================PARTICLE_SYSTEM=====================
    [SerializeField] ParticleSystem smokeEffect;

    int allEnemies = 13;
    public static int EnemiesDefeated { get; set; }

    void Start()
    {
        EnemiesDefeated = 0;
        rb = GetComponent<Rigidbody2D>();
        timer = changeTimer;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (robotFixed)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            direction = -direction;
            timer = changeTimer;
        }
    }

    // ======================MOVEMENT======================
    void FixedUpdate()
    {
        if (robotFixed)
        {
            return;
        }

        Vector2 position = transform.position;

        if (vertical)
        {
            position.y += speed * direction * Time.deltaTime;
            animator.SetFloat("Speed Y", direction);
            animator.SetFloat("Speed X", 0);
        }
        else
        {
            position.x += speed * direction * Time.deltaTime;
            animator.SetFloat("Speed X", direction);
            animator.SetFloat("Speed Y", 0);
        }

        transform.position = position;
    }

    // ====================FIXED=====================
    public void Fixed()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(hit);
        audioSource.PlayOneShot(audioFixed);

        robotFixed = true;
        rb.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        EnemiesDefeated += 1;
    }

    // ======================DAMAGE======================
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(damage);
        }
    }
}
