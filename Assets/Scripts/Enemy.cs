using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
