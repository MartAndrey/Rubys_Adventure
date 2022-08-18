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

    float speed = 1;

    void Start()
    {
        timer = changeTimer;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            direction = -direction;
            timer = changeTimer;
        }
    }

    void FixedUpdate()
    {
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
}
