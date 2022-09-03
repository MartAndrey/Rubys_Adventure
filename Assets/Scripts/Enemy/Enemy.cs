using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int damage = -1;

    // ======================POSITION======================
    Transform player;

    // =======================HEALTH=======================
    [SerializeField] int health = 1;

    // =======================SPEED========================
    [SerializeField] float speed = 1;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    // ======================POSITION======================
    void Update()
    {
        Vector2 direction = player.position - transform.position;

        transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;
    }

    // =======================HEALTH=======================
    public void TakeDamage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
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
