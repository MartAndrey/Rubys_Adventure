using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ======================MOVEMENT======================
    [SerializeField] float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * transform.right * Time.deltaTime;
    }
}
