using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ======================MOVEMENT======================
    [SerializeField] float speed = 5f;

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
            Destroy(gameObject);
        }
    }
}
