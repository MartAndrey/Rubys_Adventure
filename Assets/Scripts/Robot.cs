using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    int direction = 1;
    [SerializeField] bool vertical;

    [SerializeField] float changeTimer = 3;
    float timer;

    Animator animator;

    Rigidbody2D rb;

    float speed = 1;
    int damage = -1;

    bool robotFixed = false;

    [SerializeField] ParticleSystem smokeEffect;

    void Start()
    {
        timer = changeTimer;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

    public void Fixed()
    {
        robotFixed = true;
        rb.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            player.ChangeHealth(damage);
        }
    }
}
