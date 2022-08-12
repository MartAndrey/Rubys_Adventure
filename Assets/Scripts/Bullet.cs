using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ======================MOVEMENT======================
    [SerializeField] float speed = 5f;

    // ======================POWERSHOT=====================
    [SerializeField] int health = 3;
    public bool powerShot;


    void Start()
    {
        Destroy(gameObject, 5);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position += speed * transform.right * Time.deltaTime;
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
