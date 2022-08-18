using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ======================POWERSHOT=====================
    [SerializeField] int health = 3;
    public bool powerShot;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, 5);
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage();

            if (!powerShot) Destroy(gameObject);

            health--;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
