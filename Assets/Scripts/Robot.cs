using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    int direction = 1;
    [SerializeField] bool vertical;

    [SerializeField] float changeTimer = 3;
    float timer;

    float speed = 1;

    void Start()
    {
        timer = changeTimer;
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
        }
        else
        {
            position.x += speed * direction * Time.deltaTime;
        }

        transform.position = position;
    }
}
