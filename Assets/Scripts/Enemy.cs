using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;

    [SerializeField] int health = 1;
    [SerializeField] float speed = 1;

    void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    void Update()
    {
        Vector2 direction = player.position - transform.position;

        transform.position += (Vector3)direction.normalized * Time.deltaTime * speed;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        health--;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage();
        }
    }
}
