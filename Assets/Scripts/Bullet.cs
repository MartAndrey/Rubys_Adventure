using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ======================POWERSHOT=====================
    //[SerializeField] int health = 3;
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

    void OnCollisionEnter2D(Collision2D other)
    {
        Robot enemy = other.collider.GetComponent<Robot>();

        if (enemy != null)
        {
            enemy.Fixed();
        }
        
        Destroy(gameObject);
    }
}
